using CalamityMod;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles
{
    public class ThanatosMinion : ModProjectile
    {
        private enum Phase
        {
            Charge = 0,
            UndergroundLaserBarrage = 1,
            Deathray = 2
        }
        public ref float AttackPattern => ref Projectile.ai[0];
        public ref float AttackTimer => ref Projectile.ai[1];
        public ref float NextIsDeathray => ref Projectile.localAI[0];
        public ref float DeathrayPulse => ref Projectile.localAI[1];
        private ref float TargetIndexPlusOne => ref Projectile.localAI[2]; 
        public override string Texture => "CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosHead";
        private const int TotalBodySegments = 30; 
        private const float Body1Gap = 88f;
        private const float Body2Gap = 90f;
        private const float TailGap = 76f;

        private const int HeadFrameCount = 5;
        private const int BodyFrameCount = 5;
        private const int TailFrameCount = 5;

        private List<Vector2> segmentPositions;
        private List<float> segmentRotations;

        private const float SpeedUpTime = 300f;
        private const float SlowDownTime = 50f;
        private const float ChargePhaseDuration = SpeedUpTime + SlowDownTime;
        private const float LaserBarrageDuration = 360f;
        private const float VelocityAdjustTime = 20f;
        private const float DeathrayTelegraphDuration = 180f;
        private const float DeathrayDuration = 180f;
        private const float VentDuration = 180f;

        private const float BaseVelocity = 18f;
        private const float BaseTurnDistance = 160f;
        private const float BarrageOffset = 600f;
        private const float SlowdownRadius = 800f;

        private const float BeamLength = 2400f;
        private const float BeamWidth = 60f;
        private const float BeamDamageMultiplier = 2f;

        private const int BarrageFireRate = 12;

        private float chargeVelocityScalar;
        private float phaseSubTimer;
        private float ventTimer;
        private int barrageSide;

        public ThanatosSmokeParticleSet SmokeDrawer;

        private bool Vulnerable => (int)AttackPattern == (int)Phase.Deathray;
        private bool BeamActive => Vulnerable && DeathrayPulse >= 1f && AttackTimer >= DeathrayTelegraphDuration && AttackTimer < DeathrayTelegraphDuration + DeathrayDuration;
        private Vector2 HeadForward => (Projectile.rotation - MathHelper.PiOver2).ToRotationVector2();

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = HeadFrameCount;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 3000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 164;
            Projectile.height = 164;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft = 18000;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;
            Projectile.DamageType = DamageClass.Summon;

            SmokeDrawer = new ThanatosSmokeParticleSet(-1, 3, 0f, 16f, 1.5f);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(chargeVelocityScalar);
            writer.Write(phaseSubTimer);
            writer.Write(barrageSide);
            writer.Write(NextIsDeathray);
            writer.Write(DeathrayPulse);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            chargeVelocityScalar = reader.ReadSingle();
            phaseSubTimer = reader.ReadSingle();
            barrageSide = reader.ReadInt32();
            NextIsDeathray = reader.ReadSingle();
            DeathrayPulse = reader.ReadSingle();
        }

        // ------------------------------ segments ------------------------------

        /// <summary>Distance from segment k-1 to segment k (k = 1 is the first body piece, k = TotalBodySegments is the tail).</summary>
        private float GapFor(int k)
        {
            float gap = k == TotalBodySegments ? TailGap : (k % 2 == 1 ? Body1Gap : Body2Gap);
            return gap * Projectile.scale;
        }

        private void EnsureSegments()
        {
            if (segmentPositions != null && segmentPositions.Count == TotalBodySegments + 1)
                return;

            // Lay the worm out behind the head (opposite its heading)
            Vector2 heading = Projectile.velocity.SafeNormalize(Vector2.Zero);
            Vector2 back = heading == Vector2.Zero ? Vector2.UnitY : -heading;

            segmentPositions = new List<Vector2>();
            segmentRotations = new List<float>();
            Vector2 pos = Projectile.Center;
            for (int i = 0; i <= TotalBodySegments; i++)
            {
                if (i > 0)
                    pos += back * GapFor(i);
                segmentPositions.Add(pos);
                segmentRotations.Add((-back).ToRotation() + MathHelper.PiOver2); // heading angle + 90 degrees, same as the head
            }
        }

        private void UpdateSegments()
        {
            segmentPositions[0] = Projectile.Center;
            segmentRotations[0] = Projectile.rotation;

            for (int k = 1; k <= TotalBodySegments; k++)
            {
                Vector2 leader = segmentPositions[k - 1];
                Vector2 offset = segmentPositions[k] - leader;
                if (offset == Vector2.Zero)
                    offset = Vector2.UnitY;
                offset.Normalize();

                segmentPositions[k] = leader + offset * GapFor(k);
                // Same convention as the boss: face the segment in front of you, +90 degrees
                segmentRotations[k] = (-offset).ToRotation() + MathHelper.PiOver2;
            }
        }

        private void ResetPhase(Phase newPhase)
        {
            AttackPattern = (float)newPhase;
            AttackTimer = 0f;
            phaseSubTimer = 0f;
            chargeVelocityScalar = 0f;
            DeathrayPulse = 0f;
            Projectile.netUpdate = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead || !player.GetModPlayer<GCSEPlayer>().ThanatosSummon)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;

            EnsureSegments();

            if (Vector2.Distance(player.Center, Projectile.Center) > 3000f)
            {
                Projectile.Center = player.Center;
                for (int i = 0; i <= TotalBodySegments; i++)
                    segmentPositions[i] = player.Center;
                Projectile.netUpdate = true;
            }

            NPC target = Projectile.Center.MinionHoming(2400f, player);
            TargetIndexPlusOne = target != null ? target.whoAmI + 1 : 0f;

            if (target != null)
                AttackAI(player, target);
            else
                IdleAI(player);

            if (Projectile.velocity == Vector2.Zero)
                Projectile.velocity = -Vector2.UnitY;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            UpdateFrames();
            UpdateVent();
            UpdateSegments();

            Lighting.AddLight(Projectile.Center, Vulnerable ? new Vector3(1.2f, 0.3f, 0.3f) : new Vector3(0.3f, 0.8f, 1.2f));
        }

        private void AttackAI(Player player, NPC target)
        {
            float velocity = BaseVelocity;
            float turnSpeed = MathHelper.ToRadians(velocity * 0.11f);
            float turnDistance = BaseTurnDistance;
            Vector2 destination = target.Center;
            float distanceFromTarget = Vector2.Distance(Projectile.Center, target.Center);

            if (distanceFromTarget > 600f)
                velocity *= MathHelper.Lerp(1f, 2.5f, MathHelper.Clamp((distanceFromTarget - 600f) / 2000f, 0f, 1f));

            switch ((Phase)(int)AttackPattern)
            {
                case Phase.Charge:
                    {
                        if (phaseSubTimer == 0f)
                        {
                            chargeVelocityScalar += 1f / SpeedUpTime;
                            if (chargeVelocityScalar >= 1f)
                            {
                                chargeVelocityScalar = 1f;
                                phaseSubTimer = 1f;
                            }
                        }
                        else
                            chargeVelocityScalar = Math.Max(chargeVelocityScalar - 1f / SlowDownTime, 0f);

                        velocity *= MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
                        turnSpeed *= MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
                        turnDistance *= 0.2f;

                        if (distanceFromTarget < 240f)
                            turnSpeed *= distanceFromTarget / 240f;

                        AttackTimer++;
                        if (AttackTimer >= ChargePhaseDuration)
                            ResetPhase(NextIsDeathray == 1f ? Phase.Deathray : Phase.UndergroundLaserBarrage);
                        break;
                    }

                case Phase.UndergroundLaserBarrage:
                    {
                        destination += new Vector2(0f, barrageSide == 0 ? BarrageOffset : -BarrageOffset);
                        turnDistance *= 3f;

                        if (phaseSubTimer == 0f)
                            chargeVelocityScalar = Math.Min(chargeVelocityScalar + 0.02f, 1f);

                        velocity *= MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
                        turnSpeed *= MathHelper.Lerp(1f, 3f, chargeVelocityScalar);

                        if ((destination - Projectile.Center).Length() < turnDistance || AttackTimer > 0f)
                        {
                            AttackTimer++;

                            if (AttackTimer % BarrageFireRate == 0f && Main.myPlayer == Projectile.owner)
                            {
                                int segment = Main.rand.Next(1, TotalBodySegments);
                                FireLaser(segmentPositions[segment], target);
                            }

                            if (AttackTimer >= LaserBarrageDuration)
                            {
                                chargeVelocityScalar = Math.Max(chargeVelocityScalar - 1f / VelocityAdjustTime, 0f);
                                phaseSubTimer++;
                                if (phaseSubTimer >= VelocityAdjustTime)
                                {
                                    barrageSide ^= 1;
                                    NextIsDeathray = 1f;
                                    ResetPhase(Phase.Charge);
                                }
                            }
                        }
                        break;
                    }

                case Phase.Deathray:
                    {
                        bool readyToFire = distanceFromTarget < SlowdownRadius;
                        if (readyToFire && DeathrayPulse == 0f)
                            DeathrayPulse = 1f;

                        if (phaseSubTimer == 0f)
                        {
                            chargeVelocityScalar += 1f / DeathrayDuration;
                            if (chargeVelocityScalar >= 1f)
                            {
                                chargeVelocityScalar = 1f;
                                if (DeathrayPulse >= 1f)
                                    phaseSubTimer = 1f;
                            }
                        }
                        else
                            chargeVelocityScalar = Math.Max(chargeVelocityScalar - 5f / DeathrayDuration, 0f);

                        velocity *= MathHelper.Lerp(0.5f, 3f, chargeVelocityScalar);
                        turnSpeed *= MathHelper.Lerp(0.5f, 3f, chargeVelocityScalar);
                        turnDistance *= 0.2f;

                        if (DeathrayPulse >= 1f)
                        {
                            float velocityScale = distanceFromTarget / SlowdownRadius;
                            if (velocityScale < 1f)
                                velocityScale *= velocityScale;
                            velocity *= MathHelper.Max(velocityScale, 0.15f);
                            turnSpeed *= MathHelper.Max(velocityScale, 0.25f);

                            DeathrayPulse += 1f / 60f * (distanceFromTarget < SlowdownRadius * 0.5f ? 0.75f : 1.5f);

                            AttackTimer++;
                            if (AttackTimer == 1f)
                                SoundEngine.PlaySound(ThanatosHead.LaserSound, Projectile.Center);

                            if (AttackTimer == DeathrayTelegraphDuration)
                                SoundEngine.PlaySound(CommonCalamitySounds.LaserCannonSound, Projectile.Center);

                            if (AttackTimer >= DeathrayTelegraphDuration + DeathrayDuration)
                            {
                                NextIsDeathray = 0f;
                                ResetPhase(Phase.Charge);
                            }
                        }
                        break;
                    }
            }

            if (!((destination - Projectile.Center).Length() < turnDistance))
            {
                float targetAngle = Projectile.AngleTo(destination);
                float newAngle = Projectile.velocity.ToRotation().AngleTowards(targetAngle, turnSpeed);
                Projectile.velocity = newAngle.ToRotationVector2() * velocity;
            }

            if (Projectile.velocity.Length() > velocity)
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * velocity;
        }

        private void IdleAI(Player player)
        {
            if (AttackPattern != 0f || AttackTimer != 0f)
            {
                NextIsDeathray = 0f;
                ResetPhase(Phase.Charge);
            }

            Vector2 idleSpot = player.Center + new Vector2(player.direction * -180f, -140f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2.5f) * 40f);
            Vector2 toIdle = idleSpot - Projectile.Center;
            float speed = MathHelper.Clamp(toIdle.Length() * 0.05f, 6f, 20f);

            if (toIdle.Length() > 40f)
            {
                float newAngle = Projectile.velocity.ToRotation().AngleTowards(toIdle.ToRotation(), MathHelper.ToRadians(4f));
                Projectile.velocity = newAngle.ToRotationVector2() * speed;
            }
        }

        private void FireLaser(Vector2 source, NPC target)
        {
            SoundEngine.PlaySound(CommonCalamitySounds.ExoLaserShootSound with { Volume = 0.25f * CommonCalamitySounds.ExoLaserShootSound.Volume }, source);

            int laser = Projectile.NewProjectile(Projectile.GetSource_FromThis(), source, target.Center,
                ModContent.ProjectileType<ThanatosBeamTelegraph>(), Projectile.damage, 1f, Projectile.owner, 0f, target.whoAmI);

            if (laser >= 0 && laser < Main.maxProjectiles)
            {
                Projectile p = Main.projectile[laser];
                p.hostile = false;
                p.friendly = true;
                p.DamageType = DamageClass.Summon;
                p.netUpdate = true;
            }
        }

        private void UpdateFrames()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter < 6)
                return;

            Projectile.frameCounter = 0;
            if (Vulnerable)
                Projectile.frame = Math.Min(Projectile.frame + 1, HeadFrameCount - 1);
            else
                Projectile.frame = Math.Max(Projectile.frame - 1, 0);
        }

        private void UpdateVent()
        {
            SmokeDrawer.ParticleSpawnRate = 9999999;
            if (Vulnerable)
            {
                if (ventTimer == 0f)
                    SoundEngine.PlaySound(ThanatosHead.VentSound, Projectile.Center);

                ventTimer++;
                if (ventTimer < VentDuration)
                {
                    SmokeDrawer.BaseMoveRotation = Projectile.rotation - MathHelper.PiOver2;
                    SmokeDrawer.ParticleSpawnRate = ThanatosHead.ventCloudSpawnRate;
                }
            }
            else
                ventTimer = 0f;

            SmokeDrawer.Update();
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;

            if (BeamActive)
            {
                Vector2 start = Projectile.Center;
                Vector2 end = start + HeadForward * BeamLength;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, BeamWidth, ref collisionPoint))
                    return true;
            }

            if (projHitbox.Intersects(targetHitbox))
                return true;

            if (segmentPositions != null)
            {
                for (int i = 1; i <= TotalBodySegments; i++)
                {
                    Rectangle segmentBox = Utils.CenteredRectangle(segmentPositions[i], new Vector2(80f, 80f) * Projectile.scale);
                    if (segmentBox.Intersects(targetHitbox))
                        return true;
                }
            }

            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (BeamActive)
                modifiers.SourceDamage *= BeamDamageMultiplier;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            EnsureSegments();

            SpriteBatch spriteBatch = Main.spriteBatch;
            Vector2 headDrawPos = Projectile.Center - Main.screenPosition;

            Texture2D headTex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D body1Tex = TextureAssets.Npc[ModContent.NPCType<ThanatosBody1>()].Value;
            Texture2D body2Tex = TextureAssets.Npc[ModContent.NPCType<ThanatosBody2>()].Value;
            Texture2D tailTex = TextureAssets.Npc[ModContent.NPCType<ThanatosTail>()].Value;
            Texture2D headGlow = ThanatosHead.GlowTexture.Value;

            for (int i = TotalBodySegments; i > 0; i--)
            {
                Vector2 drawPos = segmentPositions[i] - Main.screenPosition;
                Color segmentLight = Lighting.GetColor((int)(segmentPositions[i].X / 16f), (int)(segmentPositions[i].Y / 16f));

                Texture2D tex;
                Texture2D glow;
                int frameCount;
                if (i == TotalBodySegments)
                {
                    tex = tailTex;
                    glow = ThanatosTail.GlowTexture.Value;
                    frameCount = TailFrameCount;
                }
                else if (i % 2 == 1)    
                {
                    tex = body1Tex;
                    glow = ThanatosBody1.GlowTexture.Value;
                    frameCount = BodyFrameCount;
                }
                else
                {
                    tex = body2Tex;
                    glow = ThanatosBody2.GlowTexture.Value;
                    frameCount = BodyFrameCount;
                }

                Rectangle frame = tex.Frame(1, frameCount, 0, Math.Min(Projectile.frame, frameCount - 1));
                Vector2 origin = frame.Size() * 0.5f;

                Main.EntitySpriteDraw(tex, drawPos, frame, segmentLight * Projectile.Opacity, segmentRotations[i], origin, Projectile.scale, SpriteEffects.None, 0f);
                Main.EntitySpriteDraw(glow, drawPos, frame, Color.White * Projectile.Opacity, segmentRotations[i], origin, Projectile.scale, SpriteEffects.None, 0f);
            }

            Rectangle headFrame = headTex.Frame(1, HeadFrameCount, 0, Projectile.frame);
            Vector2 headOrigin = headFrame.Size() * 0.5f;
            Main.EntitySpriteDraw(headTex, headDrawPos, headFrame, lightColor * Projectile.Opacity, Projectile.rotation, headOrigin, Projectile.scale, SpriteEffects.None, 0f);

            Color glowmaskColor = Vulnerable
                ? Color.Lerp(new Color(255, 64, 64), Color.CornflowerBlue, MathHelper.Clamp(AttackTimer / DeathrayTelegraphDuration, 0f, 1f))
                : Color.White;
            Main.EntitySpriteDraw(headGlow, headDrawPos, headFrame, glowmaskColor * Projectile.Opacity, Projectile.rotation, headOrigin, Projectile.scale, SpriteEffects.None, 0f);

            SmokeDrawer.DrawSet(Projectile.Center);

            if (Vulnerable && DeathrayPulse >= 1f)
            {
                DrawTelegraphAndBeam(spriteBatch);
                DrawAuraAndReticle(spriteBatch, headDrawPos);
            }

            return false;
        }

        private void DrawTelegraphAndBeam(SpriteBatch spriteBatch)
        {
            spriteBatch.SetBlendState(BlendState.Additive);

            float baseAngle = HeadForward.ToRotation();

            if (AttackTimer < DeathrayTelegraphDuration)
            {
                Texture2D lineTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLine").Value;
                float progress = AttackTimer / DeathrayTelegraphDuration;
                float spreadFactor = 1f - MathHelper.SmoothStep(0f, 1f, progress);
                Color lineColor = Color.Lerp(Color.Red, Color.White, progress) * 0.7f;

                for (int b = 0; b <= 6; b++)
                {
                    float squished = (float)Math.Pow(Math.Sin(MathHelper.Pi * b / 6f), 2D);
                    float offsetAngle = b == 6 ? 0f : MathHelper.Lerp(-0.74f, 0.74f, MathHelper.SmoothStep(0f, 1f, squished)) * spreadFactor;
                    float angle = baseAngle + offsetAngle;

                    spriteBatch.Draw(lineTex, Projectile.Center - Main.screenPosition, null, lineColor, angle + MathHelper.PiOver2,
                        new Vector2(lineTex.Width * 0.5f, lineTex.Height), new Vector2(0.5f + progress, BeamLength / lineTex.Height), SpriteEffects.None, 0f);
                }
            }
            else if (BeamActive)
            {
                Texture2D pixel = TextureAssets.MagicPixel.Value;
                Rectangle src = new Rectangle(0, 0, 1, 1);
                float beamProgress = (AttackTimer - DeathrayTelegraphDuration) / DeathrayDuration;
                float widthScale = (float)Math.Sin(MathHelper.Pi * MathHelper.Clamp(beamProgress, 0f, 1f)) * 0.5f + 0.5f;
                Vector2 start = Projectile.Center + HeadForward * 60f - Main.screenPosition;

                spriteBatch.Draw(pixel, start, src, new Color(255, 40, 40) * 0.6f, baseAngle, new Vector2(0f, 0.5f), new Vector2(BeamLength, BeamWidth * 1.6f * widthScale), SpriteEffects.None, 0f);
                spriteBatch.Draw(pixel, start, src, new Color(255, 120, 120) * 0.8f, baseAngle, new Vector2(0f, 0.5f), new Vector2(BeamLength, BeamWidth * widthScale), SpriteEffects.None, 0f);
                spriteBatch.Draw(pixel, start, src, Color.White, baseAngle, new Vector2(0f, 0.5f), new Vector2(BeamLength, BeamWidth * 0.35f * widthScale), SpriteEffects.None, 0f);
            }

            spriteBatch.SetBlendState(BlendState.AlphaBlend);
        }

        private void DrawAuraAndReticle(SpriteBatch spriteBatch, Vector2 headDrawPos)
        {
            Texture2D auraTexture = ThanatosHead.AuraTexture.Value;

            float auraPower = Utils.GetLerpValue(0f, DeathrayTelegraphDuration * 0.333f, AttackTimer, true);
            auraPower *= Utils.GetLerpValue(DeathrayTelegraphDuration + DeathrayDuration, DeathrayTelegraphDuration + DeathrayDuration - 20f, AttackTimer, true);

            float pulse = DeathrayPulse % 1f;
            float auraRadius = SlowdownRadius * auraPower * 1.25f;
            Vector2 outerScale = Vector2.One * auraRadius / auraTexture.Size();
            Vector2 innerScale = outerScale * (1f - pulse) * 1.2f;
            Color outerColor = Color.White * auraPower * 0.65f;
            Color innerColor = Color.Red * auraPower * 0.65f * (float)Math.Sqrt(pulse);

            spriteBatch.SetBlendState(BlendState.Additive);
            spriteBatch.Draw(auraTexture, headDrawPos, null, outerColor, 0f, auraTexture.Size() * 0.5f, outerScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(auraTexture, headDrawPos, null, innerColor, 0f, auraTexture.Size() * 0.5f, innerScale, SpriteEffects.None, 0f);
            spriteBatch.SetBlendState(BlendState.AlphaBlend);

            int targetIndex = (int)TargetIndexPlusOne - 1;
            if (targetIndex < 0 || targetIndex >= Main.maxNPCs || !Main.npc[targetIndex].active)
                return;
            NPC target = Main.npc[targetIndex];

            Texture2D left = ThanatosHead.ReticleLeftTexture.Value;
            Texture2D right = ThanatosHead.ReticleRightTexture.Value;
            Texture2D top = ThanatosHead.ReticleTopTexture.Value;
            Texture2D bottom = ThanatosHead.ReticleBottomTexture.Value;
            Texture2D prongLeft = ThanatosHead.ReticleProngLeftTexture.Value;
            Texture2D prongRight = ThanatosHead.ReticleProngRightTexture.Value;

            float lockProgress = MathHelper.Clamp(AttackTimer / DeathrayTelegraphDuration, 0f, 1f) * auraPower;
            float offset = MathHelper.SmoothStep(300f, 0f, lockProgress);
            float fadeToWhite = ((float)Math.Cos(Main.GlobalTimeWrappedHourly * 6.8f) * 0.5f + 0.5f) * lockProgress * 0.67f;
            Color baseColor = new Color(255, 0, 0, 127) * lockProgress;
            Color flashColor = Color.Lerp(baseColor, new Color(255, 255, 255, 0), fadeToWhite) * lockProgress;
            Vector2 origin = left.Size() * 0.5f;
            Vector2 targetPos = target.Center - Main.screenPosition;

            spriteBatch.Draw(left, targetPos - Vector2.UnitX * offset, null, baseColor, 0f, origin, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(right, targetPos + Vector2.UnitX * offset, null, baseColor, 0f, origin, 1f, SpriteEffects.None, 0f);
            for (int i = 0; i < 3; i++)
            {
                float scale = 1f + i * 0.125f;
                spriteBatch.Draw(prongLeft, targetPos - Vector2.UnitX * offset, null, flashColor, 0f, origin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(prongRight, targetPos + Vector2.UnitX * offset, null, flashColor, 0f, origin, scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(bottom, targetPos + Vector2.UnitY * offset, null, flashColor, 0f, origin, scale, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(top, targetPos - Vector2.UnitY * offset, null, baseColor, 0f, origin, 1f, SpriteEffects.None, 0f);
        }
    }
}