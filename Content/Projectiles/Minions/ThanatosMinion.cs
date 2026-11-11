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

namespace gcsep.Content.Projectiles.Minions
{
    public class ThanatosMinion : ModProjectile
    {
        private enum Phase
        {
            Charge = 0,
            UndergroundLaserBarrage = 1,
            Deathray = 2
        }
        private struct Steering
        {
            public float Speed;
            public float TurnSpeed;
            public float TurnDistance;
            public Vector2 Destination;
        }
        private const int TotalBodySegments = 30;
        private const float Body1Gap = 88f;   
        private const float Body2Gap = 90f;
        private const float TailGap = 76f;

        private const int HeadFrameCount = 5;
        private const int BodyFrameCount = 5;
        private const int TailFrameCount = 5;

        private const float SpeedUpTime = 300f;
        private const float SlowDownTime = 50f;
        private const float ChargePhaseDuration = SpeedUpTime + SlowDownTime;

        private const float LaserBarrageDuration = 360f;
        private const float VelocityAdjustTime = 20f;
        private const float BarrageOffset = 600f;
        private const int BarrageFireRate = 12;
        private const float BarrageLaserRange = 1800f;   
        private const float DeathrayTelegraphDuration = 180f;
        private const float DeathrayDuration = 180f;      
        private const float SlowdownRadius = 800f;
        private const float TelegraphWidth = 3600f;
        private const float BeamDamageMultiplier = 2f;
        private const float VentDuration = 180f;

        private const float BaseVelocity = 18f;
        private const float BaseTurnDistance = 160f;
        private const float LeashDistance = 3000f;        

        public ref float AttackPattern => ref Projectile.ai[0];
        public ref float AttackTimer => ref Projectile.ai[1];
        public ref float NextIsDeathray => ref Projectile.localAI[0];
        public ref float DeathrayPulse => ref Projectile.localAI[1];
        private ref float TargetIndexPlusOne => ref Projectile.localAI[2];   

        private float chargeVelocityScalar;
        private float phaseSubTimer;
        private float ventTimer;
        private int barrageSide;

        private Vector2[] segPos;
        private float[] segRot;

        public ThanatosSmokeParticleSet SmokeDrawer;

        private bool Vulnerable => (int)AttackPattern == (int)Phase.Deathray;
        private Vector2 HeadForward => (Projectile.rotation - MathHelper.PiOver2).ToRotationVector2();
        public bool IsDeathrayPhase => Vulnerable;
        public Vector2 BeamDirection => HeadForward;

        public override string Texture => "CalamityMod/NPCs/ExoMechs/Thanatos/ThanatosHead";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = HeadFrameCount;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 4000;   // long worm + long telegraph
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
        private float GapFor(int k)
        {
            float gap = k == TotalBodySegments ? TailGap : (k % 2 == 1 ? Body1Gap : Body2Gap);
            return gap * Projectile.scale;
        }

        private void EnsureSegments()
        {
            if (segPos != null)
                return;

            segPos = new Vector2[TotalBodySegments + 1];
            segRot = new float[TotalBodySegments + 1];
            LayOutSegmentsBehindHead();
        }
        private void LayOutSegmentsBehindHead()
        {
            Vector2 heading = Projectile.velocity.SafeNormalize(Vector2.Zero);
            Vector2 back = heading == Vector2.Zero ? Vector2.UnitY : -heading;

            Vector2 pos = Projectile.Center;
            for (int i = 0; i <= TotalBodySegments; i++)
            {
                if (i > 0)
                    pos += back * GapFor(i);
                segPos[i] = pos;
                segRot[i] = (-back).ToRotation() + MathHelper.PiOver2;   
            }
        }

        private void UpdateSegments()
        {
            segPos[0] = Projectile.Center;
            segRot[0] = Projectile.rotation;

            for (int k = 1; k <= TotalBodySegments; k++)
            {
                Vector2 leader = segPos[k - 1];
                Vector2 offset = segPos[k] - leader;
                if (offset == Vector2.Zero)
                    offset = Vector2.UnitY;
                offset.Normalize();

                segPos[k] = leader + offset * GapFor(k);
                segRot[k] = (-offset).ToRotation() + MathHelper.PiOver2;   
            }
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

            if (Vector2.Distance(player.Center, Projectile.Center) > LeashDistance)
            {
                Projectile.Center = player.Center;
                LayOutSegmentsBehindHead();
                Projectile.netUpdate = true;
            }

            NPC target = Projectile.Center.MinionHoming(2400f, player);
            TargetIndexPlusOne = target != null ? target.whoAmI + 1 : 0f;

            if (target != null)
                AttackAI(target);
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

        private void ResetPhase(Phase newPhase)
        {
            AttackPattern = (float)newPhase;
            AttackTimer = 0f;
            phaseSubTimer = 0f;
            chargeVelocityScalar = 0f;
            DeathrayPulse = 0f;
            Projectile.netUpdate = true;
        }

        private void AttackAI(NPC target)
        {
            float distanceFromTarget = Vector2.Distance(Projectile.Center, target.Center);

            Steering steer = new Steering
            {
                Speed = BaseVelocity,
                TurnSpeed = MathHelper.ToRadians(BaseVelocity * 0.11f),
                TurnDistance = BaseTurnDistance,
                Destination = target.Center
            };

            if (distanceFromTarget > 600f)
                steer.Speed *= MathHelper.Lerp(1f, 2.5f, MathHelper.Clamp((distanceFromTarget - 600f) / 2000f, 0f, 1f));

            switch ((Phase)(int)AttackPattern)
            {
                case Phase.Charge:
                    ChargePhase(ref steer, distanceFromTarget);
                    break;
                case Phase.UndergroundLaserBarrage:
                    BarragePhase(ref steer, target);
                    break;
                case Phase.Deathray:
                    DeathrayPhase(ref steer, distanceFromTarget);
                    break;
            }

            if (!((steer.Destination - Projectile.Center).Length() < steer.TurnDistance))
            {
                float targetAngle = Projectile.AngleTo(steer.Destination);
                float newAngle = Projectile.velocity.ToRotation().AngleTowards(targetAngle, steer.TurnSpeed);
                Projectile.velocity = newAngle.ToRotationVector2() * steer.Speed;
            }

            if (Projectile.velocity.Length() > steer.Speed)
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * steer.Speed;
        }
        private void ChargePhase(ref Steering steer, float distanceFromTarget)
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

            steer.Speed *= MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
            steer.TurnSpeed *= MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
            steer.TurnDistance *= 0.2f;

            if (distanceFromTarget < 240f)
                steer.TurnSpeed *= distanceFromTarget / 240f;

            AttackTimer++;
            if (AttackTimer >= ChargePhaseDuration)
                ResetPhase(NextIsDeathray == 1f ? Phase.Deathray : Phase.UndergroundLaserBarrage);
        }

        private void BarragePhase(ref Steering steer, NPC target)
        {
            steer.Destination += new Vector2(0f, barrageSide == 0 ? BarrageOffset : -BarrageOffset);
            steer.TurnDistance *= 3f;

            if (phaseSubTimer == 0f)
                chargeVelocityScalar = Math.Min(chargeVelocityScalar + 0.02f, 1f);

            steer.Speed *= MathHelper.Lerp(1f, 1.5f, chargeVelocityScalar);
            steer.TurnSpeed *= MathHelper.Lerp(1f, 3f, chargeVelocityScalar);

            if ((steer.Destination - Projectile.Center).Length() >= steer.TurnDistance && AttackTimer <= 0f)
                return;

            AttackTimer++;

            if (AttackTimer % BarrageFireRate == 0f && Main.myPlayer == Projectile.owner)
                FireLaser(segPos[PickBarrageSegment(target)], target);

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

        private void DeathrayPhase(ref Steering steer, float distanceFromTarget)
        {
            if (distanceFromTarget < SlowdownRadius && DeathrayPulse == 0f)
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

            steer.Speed *= MathHelper.Lerp(0.5f, 3f, chargeVelocityScalar);
            steer.TurnSpeed *= MathHelper.Lerp(0.5f, 3f, chargeVelocityScalar);
            steer.TurnDistance *= 0.2f;

            if (DeathrayPulse < 1f)
                return;

            float velocityScale = distanceFromTarget / SlowdownRadius;
            if (velocityScale < 1f)
                velocityScale *= velocityScale;
            steer.Speed *= MathHelper.Max(velocityScale, 0.15f);
            steer.TurnSpeed *= MathHelper.Max(velocityScale, 0.25f);

            DeathrayPulse += 1f / 60f * (distanceFromTarget < SlowdownRadius * 0.5f ? 0.75f : 1.5f);

            AttackTimer++;
            if (AttackTimer == 1f)
                SoundEngine.PlaySound(ThanatosHead.LaserSound, Projectile.Center);

            if (AttackTimer == DeathrayTelegraphDuration)
            {
                SoundEngine.PlaySound(CommonCalamitySounds.LaserCannonSound, Projectile.Center);

                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                        ModContent.ProjectileType<ThanatosMinionBeam>(), (int)(Projectile.damage * BeamDamageMultiplier), 0f, Projectile.owner,
                        0f, Projectile.identity);
                }
            }

            if (AttackTimer >= DeathrayTelegraphDuration + DeathrayDuration)
            {
                NextIsDeathray = 0f;
                ResetPhase(Phase.Charge);
            }
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
        private int PickBarrageSegment(NPC target)
        {
            int chosen = -1;
            int candidates = 0;
            float rangeSquared = BarrageLaserRange * BarrageLaserRange;

            for (int i = 1; i < TotalBodySegments; i++)
            {
                if (Vector2.DistanceSquared(segPos[i], target.Center) < rangeSquared && Main.rand.Next(++candidates) == 0)
                    chosen = i;
            }

            return chosen != -1 ? chosen : Main.rand.Next(1, TotalBodySegments);
        }

        private void FireLaser(Vector2 source, NPC target)
        {
            SoundEngine.PlaySound(CommonCalamitySounds.ExoLaserShootSound with { Volume = 0.25f * CommonCalamitySounds.ExoLaserShootSound.Volume }, source);

            Vector2 direction = (target.Center - source).SafeNormalize(Vector2.UnitY);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), source, direction * 7.5f,
                ModContent.ProjectileType<ExoMinionLaser>(), Projectile.damage, 1f, Projectile.owner);
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
            if (projHitbox.Intersects(targetHitbox))
                return true;

            if (segPos != null)
            {
                for (int i = 1; i <= TotalBodySegments; i++)
                {
                    Rectangle segmentBox = Utils.CenteredRectangle(segPos[i], new Vector2(80f, 80f) * Projectile.scale);
                    if (segmentBox.Intersects(targetHitbox))
                        return true;
                }
            }

            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            EnsureSegments();

            Vector2 headDrawPos = Projectile.Center - Main.screenPosition;

            Texture2D headTex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D headGlow = ThanatosHead.GlowTexture.Value;

            for (int i = TotalBodySegments; i > 0; i--)
            {
                Texture2D tex;
                Texture2D glow;
                int frameCount;
                if (i == TotalBodySegments)
                {
                    tex = TextureAssets.Npc[ModContent.NPCType<ThanatosTail>()].Value;
                    glow = ThanatosTail.GlowTexture.Value;
                    frameCount = TailFrameCount;
                }
                else if (i % 2 == 1)    
                {
                    tex = TextureAssets.Npc[ModContent.NPCType<ThanatosBody1>()].Value;
                    glow = ThanatosBody1.GlowTexture.Value;
                    frameCount = BodyFrameCount;
                }
                else
                {
                    tex = TextureAssets.Npc[ModContent.NPCType<ThanatosBody2>()].Value;
                    glow = ThanatosBody2.GlowTexture.Value;
                    frameCount = BodyFrameCount;
                }

                Rectangle frame = tex.Frame(1, frameCount, 0, Math.Min(Projectile.frame, frameCount - 1));
                Vector2 origin = frame.Size() * 0.5f;
                Vector2 drawPos = segPos[i] - Main.screenPosition;
                Color segmentLight = Lighting.GetColor((int)(segPos[i].X / 16f), (int)(segPos[i].Y / 16f));

                Main.EntitySpriteDraw(tex, drawPos, frame, segmentLight * Projectile.Opacity, segRot[i], origin, Projectile.scale, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(glow, drawPos, frame, Color.White * Projectile.Opacity, segRot[i], origin, Projectile.scale, SpriteEffects.None, 0);
            }

            // Head
            Rectangle headFrame = headTex.Frame(1, HeadFrameCount, 0, Projectile.frame);
            Vector2 headOrigin = headFrame.Size() * 0.5f;
            Main.EntitySpriteDraw(headTex, headDrawPos, headFrame, lightColor * Projectile.Opacity, Projectile.rotation, headOrigin, Projectile.scale, SpriteEffects.None, 0);

            Color glowmaskColor = Vulnerable
                ? Color.Lerp(new Color(255, 64, 64), Color.CornflowerBlue, MathHelper.Clamp(AttackTimer / DeathrayTelegraphDuration, 0f, 1f))
                : Color.White;
            Main.EntitySpriteDraw(headGlow, headDrawPos, headFrame, glowmaskColor * Projectile.Opacity, Projectile.rotation, headOrigin, Projectile.scale, SpriteEffects.None, 0);

            SmokeDrawer.DrawSet(Projectile.Center);

            if (Vulnerable && DeathrayPulse >= 1f)
            {
                DrawTelegraph();
                DrawAuraAndReticle(Main.spriteBatch, headDrawPos);
            }

            return false;
        }
        private void DrawTelegraph()
        {
            if (AttackTimer >= DeathrayTelegraphDuration)
                return;

            Texture2D telegraph = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/LaserWallTelegraphBeam").Value;

            float time = AttackTimer;
            float timeLeft = DeathrayTelegraphDuration - time;
            float convergence = MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(25f, 120f, time, true));
            float verticalScale = Utils.GetLerpValue(0f, 20f, time, true) * Utils.GetLerpValue(0f, 16f, timeLeft, true) * 4f;

            Vector2 origin = telegraph.Size() * new Vector2(0f, 0.5f);
            Vector2 scaleInner = new Vector2(TelegraphWidth / telegraph.Width, verticalScale);
            Vector2 scaleOuter = scaleInner * new Vector2(1f, 2.2f);

            Color colorOuter = Color.Lerp(Color.Red, Color.CornflowerBlue, time / DeathrayTelegraphDuration);   // red to blue, as if heating up
            colorOuter = Color.Lerp(colorOuter, Color.White, Utils.GetLerpValue(40f, 0f, timeLeft, true) * 0.8f);
            Color colorInner = Color.Lerp(colorOuter, Color.White, 0.5f);
            colorInner *= 0.85f;
            colorOuter *= 0.7f;

            float headAngle = HeadForward.ToRotation();
            Vector2 start = Projectile.Center + HeadForward * 8f - Main.screenPosition;

            for (int b = 0; b <= 6; b++)
            {
                // Beams 0-5 start fanned out ("stretched" spacing); beam 6 is the straight one
                float startOffset = 0f;
                if (b < 6)
                {
                    float squished = (float)Math.Pow(Math.Sin(MathHelper.Pi * b / 6f), 2D);
                    startOffset = MathHelper.Lerp(-0.74f, 0.74f, MathHelper.SmoothStep(0f, 1f, squished));
                }

                float rotation = startOffset.AngleLerp(0f, convergence) + headAngle;
                Main.EntitySpriteDraw(telegraph, start, null, colorOuter, rotation, origin, scaleOuter, SpriteEffects.None, 0);
                Main.EntitySpriteDraw(telegraph, start, null, colorInner, rotation, origin, scaleInner, SpriteEffects.None, 0);
            }
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