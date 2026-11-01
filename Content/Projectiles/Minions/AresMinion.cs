using CalamityMod;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Intrinsics.X86;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles.Minions
{
    public class AresMinion : ModProjectile
    {
        public const int NumBeams = 8;
        public const int TelegraphDuration = 90;
        public const int BeamDuration = 300;
        public const float BeamLength = 2400f;
        public const float BeamSpinPerTick = 0.01f;   
        private const float BeamDamageMult = 1.5f;    
        private const float HoverHeight = 460f;       
        public const int StatePlasma = 0;   
        public const int StateBeams = 1;    
        public const int StateTesla = 2;    
        public const int StateGauss = 3;    
        public const int StateLaser = 4;    
        public const int StateBarrage = 5;  

        private const float PlasmaDamageMult = 1f;
        private const float TeslaDamageMult = 1.2f;
        private const float GaussDamageMult = 3f;
        private const float LaserDamageMult = 0.8f;
        private const int MaxFramesX = 6;
        private const int MaxFramesY = 8;

        public ref float AttackPattern => ref Projectile.ai[0]; 
        public ref float AttackTimer => ref Projectile.ai[1];

        private int bodyFrameIndex;
        private int bodyFrameTick;
        private bool lastWasBeamState;
        private int cannonFrameIndex;
        private int cannonFrameTick;
        private float[] cannonRot;
        private int[] cannonDir;
        private float[] cannonKick;   
        private int orbIdentity;      

        public static readonly SoundStyle LaserStartSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresCircleLaserStart");
        public static readonly SoundStyle LaserEndSound = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresCircleLaserEnd");

        public override string Texture => "CalamityMod/NPCs/ExoMechs/Ares/AresBody";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1; // we do our own sheet animation
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 220;
            Projectile.height = 252;

            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft = 18000;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6;
            Projectile.DamageType = DamageClass.Summon;

            
            cannonRot = new float[4];
            cannonDir = new int[] { -1, 1, -1, 1 };
            cannonKick = new float[4];
        }

        public override bool? CanDamage() => false;
        public Vector2 CoreWorldPos => Projectile.Center + new Vector2(-1f, 23f).RotatedBy(Projectile.rotation) * Projectile.scale;

        private Vector2 CannonPos(int which)
        {
            float bob = 20f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.6f + which);
            Vector2 local;
            switch (which)
            {
                case 0: local = new Vector2(-560f, bob); break;
                case 1: local = new Vector2(560f, -bob); break;
                case 2: local = new Vector2(-375f, 160f + bob); break;
                default: local = new Vector2(375f, 160f - bob); break;
            }
            return Projectile.Center + local.RotatedBy(Projectile.rotation) * Projectile.scale;
        }
        private static Rectangle SheetFrame(Texture2D tex, int cols, int rows, int index)
        {
            int w = tex.Width / cols;
            int h = tex.Height / rows;
            return new Rectangle(w * (index / rows), h * (index % rows), w, h);
        }
        private static float StepAngle(float current, float target, float maxChange)
        {
            float diff = MathHelper.WrapAngle(target - current);
            return MathHelper.WrapAngle(current + MathHelper.Clamp(diff, -maxChange, maxChange));
        }

        private static void MakeFriendly(int index)
        {
            if (index < 0 || index >= Main.maxProjectiles)
                return;
            Projectile p = Main.projectile[index];
            p.hostile = false;
            p.friendly = true;
            p.DamageType = DamageClass.Summon;
        }

        private void MoveTowards(Vector2 destination, float maxSpeed, float inertia)
        {
            Vector2 toDest = destination - Projectile.Center;
            float dist = toDest.Length();
            Vector2 desired = dist > 1f ? toDest / dist * Math.Min(maxSpeed, dist * 0.1f) : Vector2.Zero; 
            Projectile.velocity = (Projectile.velocity * inertia + desired) / (inertia + 1f);
        }

        private void AnimateBody()
        {
            bool beamState = (int)AttackPattern == StateBeams;
            if (beamState != lastWasBeamState)
            {
                lastWasBeamState = beamState;
                bodyFrameIndex = beamState ? 12 : 0; 
                bodyFrameTick = 0;
            }

            if (++bodyFrameTick < 6)
                return;
            bodyFrameTick = 0;
            bodyFrameIndex++;

            if (!beamState)
            {
                if (bodyFrameIndex > 11)
                    bodyFrameIndex = 0;      
            }
            else if (bodyFrameIndex > 47)
                bodyFrameIndex = 36;         
        }

        private void UpdateCannons(NPC target)
        {
            if (++cannonFrameTick >= 6)
            {
                cannonFrameTick = 0;
                cannonFrameIndex = (cannonFrameIndex + 1) % 12; // idle loop
            }

            for (int i = 0; i < 4; i++)
            {
                cannonKick[i] *= 0.85f;
                Vector2 pos = CannonPos(i);
                bool leftSide = i % 2 == 0;
                Vector2 aim = target != null ? target.Center : pos + new Vector2(leftSide ? -600f : 600f, 300f);

                int dir = Math.Sign(aim.X - pos.X);
                if (dir == 0)
                    dir = cannonDir[i];

                if (dir != cannonDir[i])
                {
                    cannonRot[i] += MathHelper.Pi;
                    cannonDir[i] = dir;
                }

                float desired = (aim - pos).ToRotation();
                if (dir == -1)
                    desired += MathHelper.Pi;

                cannonRot[i] = StepAngle(cannonRot[i], desired, 0.1f);
            }
        }
        private void GoToState(int next)
        {
            AttackPattern = next;
            AttackTimer = 0f;
            Projectile.netUpdate = true;
        }
        private static int NextState(int state)
        {
            switch (state)
            {
                case StatePlasma: return StateTesla;
                case StateTesla: return StateGauss;
                case StateGauss: return StateLaser;
                case StateLaser: return StateBarrage;
                case StateBarrage: return StateBeams;
                default: return StatePlasma;
            }
        }
        private bool Every(float interval, float start = 0f) => AttackTimer >= start && (AttackTimer - start) % interval == 0f;
        private void FirePlasma(NPC target)
        {
            Vector2 pos = CannonPos(3);
            Vector2 dir = (target.Center - pos).SafeNormalize(Vector2.UnitY);
            SoundEngine.PlaySound(CommonCalamitySounds.ExoPlasmaShootSound, pos);
            cannonKick[3] = 14f;
            if (Main.myPlayer != Projectile.owner)
                return;

            int fireball = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos + dir * 40f, dir * 12f,
                ModContent.ProjectileType<AresPlasmaFireball>(), (int)(Projectile.damage * PlasmaDamageMult), 0f, Projectile.owner,
                target.Center.X, target.Center.Y);
            MakeFriendly(fireball);
        }
        private void FireTesla(NPC target)
        {
            Vector2 pos = CannonPos(2);
            Vector2 dir = (target.Center - pos).SafeNormalize(Vector2.UnitY);
            SoundEngine.PlaySound(AresTeslaCannon.TeslaOrbShootSound, pos);
            cannonKick[2] = 14f;
            if (Main.myPlayer != Projectile.owner)
                return;

            for (int i = -1; i <= 1; i++)
            {
                Vector2 velocity = dir.RotatedBy(i * 0.3f) * 11f;
                int orb = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos + velocity.SafeNormalize(Vector2.UnitY) * 40f, velocity,
                    ModContent.ProjectileType<AresTeslaOrb>(), (int)(Projectile.damage * TeslaDamageMult), 0f, Projectile.owner,
                    orbIdentity++);
                MakeFriendly(orb);
            }
        }

        private void FireGauss(NPC target)
        {
            Vector2 pos = CannonPos(1);
            Vector2 dir = (target.Center - pos).SafeNormalize(Vector2.UnitY);
            SoundEngine.PlaySound(CommonCalamitySounds.LargeWeaponFireSound, pos);
            cannonKick[1] = 22f;
            if (Main.myPlayer != Projectile.owner)
                return;

            int nuke = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos + dir * 40f, dir * 14f,
                ModContent.ProjectileType<AresGaussNukeProjectile>(), (int)(Projectile.damage * GaussDamageMult), 5f, Projectile.owner,
                0f, target.Center.Y);
            MakeFriendly(nuke);
        }

        private void FireLaser(NPC target)
        {
            Vector2 pos = CannonPos(0);
            Vector2 dir = (target.Center - pos).SafeNormalize(Vector2.UnitY);
            SoundEngine.PlaySound(CommonCalamitySounds.ExoLaserShootSound with { Volume = 0.35f * CommonCalamitySounds.ExoLaserShootSound.Volume }, pos);
            cannonKick[0] = 8f;
            if (Main.myPlayer != Projectile.owner)
                return;

            int laser = Projectile.NewProjectile(Projectile.GetSource_FromThis(), pos + dir * 70f + Vector2.UnitY * 16f, target.Center,
                ModContent.ProjectileType<AresDeathBeamTelegraph>(), (int)(Projectile.damage * LaserDamageMult), 0f, Projectile.owner,
                0f, target.whoAmI);
            MakeFriendly(laser);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead || !player.GetModPlayer<GCSEPlayer>().AresSummon)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;

            NPC target = Projectile.Center.MinionHoming(2400f, player);
            int state = (int)AttackPattern;

            if (target != null)
            {
                AttackTimer++;

                float sway = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.7f) * 220f;
                Vector2 destination = state == StateBeams ? target.Center : target.Center + new Vector2(sway, -HoverHeight);
                MoveTowards(destination, state == StateBeams ? 22f : 14f, 16f);

                switch (state)
                {
                    case StatePlasma:
                        if (AttackTimer == 1f)
                            SoundEngine.PlaySound(AresPlasmaFlamethrower.TelSound, CannonPos(3));
                        if (Every(25f, 40f))
                            FirePlasma(target);
                        if (AttackTimer >= 190f)
                            GoToState(NextState(state));
                        break;

                    case StateTesla:
                        if (AttackTimer == 1f)
                            SoundEngine.PlaySound(AresTeslaCannon.TelSound, CannonPos(2));
                        if (Every(35f, 40f))
                            FireTesla(target);
                        if (AttackTimer >= 200f)
                            GoToState(NextState(state));
                        break;

                    case StateGauss:
                        if (AttackTimer == 1f)
                            SoundEngine.PlaySound(AresGaussNuke.TelSound, CannonPos(1));
                        if (AttackTimer == 60f || AttackTimer == 120f)
                            FireGauss(target);
                        if (AttackTimer >= 170f)
                            GoToState(NextState(state));
                        break;

                    case StateLaser:
                        if (AttackTimer == 1f)
                            SoundEngine.PlaySound(AresLaserCannon.TelSound, CannonPos(0));
                        if (AttackTimer < 150f && Every(10f, 30f))
                            FireLaser(target);
                        if (AttackTimer >= 170f)
                            GoToState(NextState(state));
                        break;

                    case StateBarrage:
                        if (AttackTimer == 1f)
                            SoundEngine.PlaySound(AresGaussNuke.TelSound, CannonPos(1));
                        if (Every(45f, 20f))
                            FirePlasma(target);
                        if (Every(60f, 30f))
                            FireTesla(target);
                        if (Every(16f, 10f))
                            FireLaser(target);
                        if (AttackTimer == 90f)
                            FireGauss(target);
                        if (AttackTimer >= 240f)
                            GoToState(NextState(state));
                        break;

                    case StateBeams:
                        if (AttackTimer == 1f)
                            SoundEngine.PlaySound(in CommonCalamitySounds.LaserCannonSound, Projectile.Center);

                        if (AttackTimer == TelegraphDuration)
                        {
                            SoundEngine.PlaySound(in AresBody.LaserStartSound, Projectile.Center);

                            if (Main.myPlayer == Projectile.owner)
                            {
                                int damage = (int)(Projectile.damage * BeamDamageMult);
                                for (int k = 0; k < NumBeams; k++)
                                {
                                    float angle = k * MathHelper.TwoPi / NumBeams;
                                    Projectile.NewProjectile(
                                        Projectile.GetSource_FromThis(),
                                        CoreWorldPos,
                                        Vector2.Zero,
                                        ModContent.ProjectileType<AresMinionBeam>(),
                                        damage,
                                        0f,
                                        Projectile.owner,
                                        angle,                 
                                        Projectile.identity);  
                                }
                            }
                        }

                        if (AttackTimer >= TelegraphDuration + BeamDuration)
                        {
                            SoundEngine.PlaySound(in AresBody.LaserEndSound, Projectile.Center);
                            GoToState(NextState(state));
                        }
                        break;

                    default:
                        GoToState(StatePlasma); 
                        break;
                }
            }
            else
            {
                AttackPattern = StatePlasma;
                AttackTimer = 0f;

                Vector2 idleSpot = player.Center + new Vector2(-player.direction * 140f, -220f + player.gfxOffY);
                if (Vector2.Distance(Projectile.Center, player.Center) > 2500f)
                    Projectile.Center = player.Center;
                MoveTowards(idleSpot, 14f, 20f);
            }

            Projectile.rotation = Projectile.velocity.X * 0.004f;

            AnimateBody();
            UpdateCannons(target);

            Lighting.AddLight(Projectile.Center, 0.2f, 0.9f, 1.5f);
        }
        private static void DrawLine(SpriteBatch sb, Vector2 worldStart, float angle, float length, float width, Color color)
        {
            sb.Draw(TextureAssets.MagicPixel.Value, worldStart - Main.screenPosition, new Rectangle(0, 0, 1, 1),
                color, angle, new Vector2(0f, 0.5f), new Vector2(length, width), SpriteEffects.None, 0f);
        }

        public void DrawMinionArm(SpriteBatch spriteBatch, Vector2 handPosition, Color glowmaskColor, int direction, bool backArm)
        {
            // Ported from AresBody.DrawArm (same offsets / math as the boss)
            SpriteEffects dirFx = direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            float s = Projectile.scale;
            float distanceFromHand = Vector2.Distance(Projectile.Center, handPosition);
            int animFrame = (int)(Main.GlobalTimeWrappedHourly * 0.9f % 1f * 9f);
            Vector2 drawOffset = Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Color glowAlpha = Projectile.GetAlpha(glowmaskColor);

            if (backArm)
            {
                Texture2D shoulderTex = AresBody.ArmTopShoulderTexture.Value;
                Texture2D arm1Tex = AresBody.ArmTopTexture.Value;
                Texture2D segmentTex = AresBody.ArmSegmentTexture.Value;
                Texture2D arm2Tex = AresBody.ArmTopTexture2.Value;
                Texture2D shoulderGlow = AresBody.ArmTopShoulderTexture_Glow.Value;
                Texture2D segmentGlow = AresBody.ArmSegmentTexture_Glow.Value;
                Texture2D arm2Glow = AresBody.ArmTopTexture2_Glow.Value;

                Vector2 shoulderPos = Projectile.Center + s * new Vector2(direction * 176f, -100f).RotatedBy(Projectile.rotation);
                Vector2 arm1Pos = shoulderPos + s * new Vector2(direction * (shoulderTex.Width + 16f), 10f).RotatedBy(Projectile.rotation);
                Vector2 segmentPos = arm1Pos;

                Rectangle shoulderFrame = shoulderTex.Frame(1, 9, 0, animFrame);
                Rectangle segmentFrame = segmentTex.Frame(1, 9, 0, animFrame);
                Rectangle arm2Frame = arm2Tex.Frame(1, 9, 0, animFrame);

                Vector2 arm1Origin = arm1Tex.Size() * new Vector2(direction == 1 ? 1f : 0f, 0.5f);
                Vector2 arm2Origin = arm2Frame.Size() * new Vector2(direction == 1 ? 1f : 0f, 0.5f);

                float arm1Rot = MathHelper.Clamp(distanceFromHand * direction / 1200f, -0.12f, 0.12f);
                float arm2Rot = (handPosition - segmentPos - Vector2.UnitY * 12f).ToRotation();
                if (direction == 1)
                    arm2Rot += MathHelper.Pi;
                float segmentRot = arm2Rot;

                segmentPos += arm1Rot.ToRotationVector2() * s * direction * -14f;
                segmentPos -= arm2Rot.ToRotationVector2() * s * direction * 20f;
                Vector2 arm2Pos = segmentPos;
                arm2Pos -= arm2Rot.ToRotationVector2() * direction * s * 40f;
                arm2Pos += (arm2Rot - MathHelper.PiOver2).ToRotationVector2() * s * 14f;

                Color shoulderLight = Projectile.GetAlpha(Lighting.GetColor(shoulderPos.ToTileCoordinates()));
                Color arm1Light = Projectile.GetAlpha(Lighting.GetColor(arm1Pos.ToTileCoordinates()));
                Color segmentLight = Projectile.GetAlpha(Lighting.GetColor(segmentPos.ToTileCoordinates()));
                Color arm2Light = Projectile.GetAlpha(Lighting.GetColor(arm2Pos.ToTileCoordinates()));

                if (!Main.dedServ && Projectile.Opacity > 0f && Main.rand.NextBool(4))
                {
                    Dust d = Dust.NewDustPerfect(segmentPos, DustID.Electric, Vector2.Zero, 100, Color.Cyan, 0.6f);
                    d.noGravity = true;
                    Dust d2 = Dust.NewDustPerfect(arm2Pos, DustID.Electric, Vector2.Zero, 100, Color.Cyan, 0.6f);
                    d2.noGravity = true;
                }

                Main.EntitySpriteDraw(arm1Tex, arm1Pos + drawOffset, null, arm1Light, arm1Rot, arm1Origin, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(shoulderTex, shoulderPos + drawOffset, shoulderFrame, shoulderLight, 0f, shoulderFrame.Size() * 0.5f, s, dirFx, 0f);
                Main.EntitySpriteDraw(shoulderGlow, shoulderPos + drawOffset, shoulderFrame, glowAlpha, 0f, shoulderFrame.Size() * 0.5f, s, dirFx, 0f);
                Main.EntitySpriteDraw(segmentTex, segmentPos + drawOffset, segmentFrame, segmentLight, segmentRot, segmentFrame.Size() * 0.5f, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(segmentGlow, segmentPos + drawOffset, segmentFrame, glowAlpha, segmentRot, segmentFrame.Size() * 0.5f, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(arm2Tex, arm2Pos + drawOffset, arm2Frame, arm2Light, arm2Rot, arm2Origin, s, dirFx ^ SpriteEffects.FlipVertically, 0f);
                Main.EntitySpriteDraw(arm2Glow, arm2Pos + drawOffset, arm2Frame, glowAlpha, arm2Rot, arm2Origin, s, dirFx ^ SpriteEffects.FlipVertically, 0f);
            }
            else
            {
                Texture2D shoulderTex = AresBody.ArmBottomShoulderTexture.Value;
                Texture2D connectorTex = AresBody.ArmBottomConnectorTexture.Value;
                Texture2D arm1Tex = AresBody.ArmBottomTexture.Value;
                Texture2D arm2Tex = AresBody.ArmBottomTexture2.Value;
                Texture2D shoulderGlow = AresBody.ArmBottomShoulderTexture_Glow.Value;
                Texture2D arm1Glow = AresBody.ArmBottomTexture_Glow.Value;
                Texture2D arm2Glow = AresBody.ArmBottomTexture2_Glow.Value;

                Vector2 shoulderPos = Projectile.Center + s * new Vector2(direction * 110f, -54f).RotatedBy(Projectile.rotation);
                Vector2 connectorPos = shoulderPos + s * new Vector2(direction * 20f, 32f).RotatedBy(Projectile.rotation);
                Vector2 arm1Pos = shoulderPos + s * Vector2.UnitX * direction * 20f;

                Rectangle arm1Frame = arm1Tex.Frame(1, 9, 0, animFrame);
                Rectangle shoulderFrame = shoulderTex.Frame(1, 9, 0, animFrame);
                Rectangle arm2Frame = arm2Tex.Frame(1, 9, 0, animFrame);

                Vector2 arm1Origin = arm1Frame.Size() * new Vector2(direction == 1 ? 1f : 0f, 0.5f);
                Vector2 arm2Origin = arm2Frame.Size() * new Vector2(direction == 1 ? 1f : 0f, 0.5f);

                float arm1Rot = CalamityMod.CalamityUtils.WrapAngle90Degrees((handPosition - shoulderPos).ToRotation()) * 0.5f;
                connectorPos += arm1Rot.ToRotationVector2() * s * direction * -26f;
                arm1Pos += arm1Rot.ToRotationVector2() * s * direction * (arm1Tex.Width - 14f);
                float arm2Rot = CalamityMod.CalamityUtils.WrapAngle90Degrees((handPosition - arm1Pos).ToRotation());

                Vector2 arm2Pos = arm1Pos + arm2Rot.ToRotationVector2() * s * direction * (arm2Tex.Width + 16f) - Vector2.UnitY * 16f;

                Color shoulderLight = Projectile.GetAlpha(Lighting.GetColor(shoulderPos.ToTileCoordinates()));
                Color arm1Light = Projectile.GetAlpha(Lighting.GetColor(arm1Pos.ToTileCoordinates()));
                Color arm2Light = Projectile.GetAlpha(Lighting.GetColor(arm2Pos.ToTileCoordinates()));

                if (!Main.dedServ && Projectile.Opacity > 0f && Main.rand.NextBool(4))
                {
                    Dust d = Dust.NewDustPerfect(arm1Pos, DustID.Electric, Vector2.Zero, 100, Color.Cyan, 0.6f);
                    d.noGravity = true;
                    Dust d2 = Dust.NewDustPerfect(arm2Pos, DustID.Electric, Vector2.Zero, 100, Color.Cyan, 0.6f);
                    d2.noGravity = true;
                }

                Main.EntitySpriteDraw(shoulderTex, shoulderPos + drawOffset, shoulderFrame, shoulderLight, arm1Rot, shoulderFrame.Size() * 0.5f, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(shoulderGlow, shoulderPos + drawOffset, shoulderFrame, glowAlpha, arm1Rot, shoulderFrame.Size() * 0.5f, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(connectorTex, connectorPos + drawOffset, null, shoulderLight, 0f, connectorTex.Size() * 0.5f, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(arm1Tex, arm1Pos + drawOffset, arm1Frame, arm1Light, arm1Rot, arm1Origin, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(arm1Glow, arm1Pos + drawOffset, arm1Frame, glowAlpha, arm1Rot, arm1Origin, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(arm2Tex, arm2Pos + drawOffset, arm2Frame, arm2Light, arm2Rot, arm2Origin, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
                Main.EntitySpriteDraw(arm2Glow, arm2Pos + drawOffset, arm2Frame, glowAlpha, arm2Rot, arm2Origin, s, dirFx ^ SpriteEffects.FlipHorizontally, 0f);
            }
        }

        private void DrawCannon(SpriteBatch sb, int which, Texture2D tex, Texture2D glow, int cols, int rows, Color lightColor)
        {
            Rectangle frame = SheetFrame(tex, cols, rows, cannonFrameIndex);
            Vector2 origin = frame.Size() * 0.5f;
            Vector2 facing = (cannonRot[which] - (cannonDir[which] == -1 ? MathHelper.Pi : 0f)).ToRotationVector2();
            Vector2 pos = CannonPos(which) - facing * cannonKick[which] - Main.screenPosition;
            SpriteEffects fx = cannonDir[which] == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.EntitySpriteDraw(tex, pos, frame, lightColor * Projectile.Opacity, cannonRot[which], origin, Projectile.scale, fx, 0f);
            Main.EntitySpriteDraw(glow, pos, frame, Color.White * Projectile.Opacity, cannonRot[which], origin, Projectile.scale, fx, 0f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Rectangle bodyFrame = SheetFrame(texture, MaxFramesX, MaxFramesY, bodyFrameIndex);
            Vector2 origin = bodyFrame.Size() * 0.5f;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;

            if ((int)AttackPattern == StateBeams && AttackTimer < TelegraphDuration)
            {
                float progress = AttackTimer / TelegraphDuration;
                float pulse = 0.6f + 0.4f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 14f);
                Color lineColor = new Color(90, 220, 255, 0) * (progress * pulse * 0.8f);
                for (int k = 0; k < NumBeams; k++)
                    DrawLine(sb, CoreWorldPos, k * MathHelper.TwoPi / NumBeams, BeamLength, 2f + progress * 6f, lineColor);
            }

            for (int i = 1; i < 5; i += 2)
            {
                if (Projectile.oldPos[i] == Vector2.Zero)
                    continue;
                Color trailColor = Projectile.GetAlpha(Color.Lerp(lightColor, Color.White, 0.5f)) * ((5 - i) / 15f);
                Vector2 trailPos = Projectile.oldPos[i] + new Vector2(Projectile.width, Projectile.height) * 0.5f - Main.screenPosition;
                Main.EntitySpriteDraw(texture, trailPos, bodyFrame, trailColor, Projectile.oldRot[i], origin, Projectile.scale, SpriteEffects.None, 0f);
            }

            DrawMinionArm(sb, CannonPos(0), Color.White, -1, true);   // laser
            DrawMinionArm(sb, CannonPos(1), Color.White, 1, true);    // gauss
            DrawMinionArm(sb, CannonPos(2), Color.White, -1, false);  // tesla
            DrawMinionArm(sb, CannonPos(3), Color.White, 1, false);   // plasma

            Main.EntitySpriteDraw(texture, drawPosition, bodyFrame, lightColor * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);
            Main.EntitySpriteDraw(AresBody.GlowTexture.Value, drawPosition, bodyFrame, Color.White * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);

            DrawCannon(sb, 0, TextureAssets.Npc[ModContent.NPCType<AresLaserCannon>()].Value, AresLaserCannon.GlowTexture.Value, 6, 8, lightColor);
            DrawCannon(sb, 1, TextureAssets.Npc[ModContent.NPCType<AresGaussNuke>()].Value, AresGaussNuke.GlowTexture.Value, 9, 12, lightColor);
            DrawCannon(sb, 2, TextureAssets.Npc[ModContent.NPCType<AresTeslaCannon>()].Value, AresTeslaCannon.GlowTexture.Value, 6, 8, lightColor);
            DrawCannon(sb, 3, TextureAssets.Npc[ModContent.NPCType<AresPlasmaFlamethrower>()].Value, AresPlasmaFlamethrower.GlowTexture.Value, 6, 8, lightColor);

            return false;
        }
    }
    public class AresMinionBeam : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MagicMissile; // never actually drawn

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 3000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = AresMinion.BeamDuration;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
        }

        private float WidthScale()
        {
            float elapsed = AresMinion.BeamDuration - Projectile.timeLeft;
            float grow = MathHelper.Clamp(elapsed / 15f, 0f, 1f);
            float shrink = MathHelper.Clamp(Projectile.timeLeft / 20f, 0f, 1f);
            return Math.Min(grow, shrink);
        }

        public override void AI()
        {
            int index = Projectile.GetByUUID(Projectile.owner, (int)Projectile.ai[1]);
            if (index < 0 || !Main.projectile[index].active || !(Main.projectile[index].ModProjectile is AresMinion minion))
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.timeLeft < AresMinion.BeamDuration - 5 && (int)minion.AttackPattern != AresMinion.StateBeams)
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = minion.CoreWorldPos;
            float elapsed = AresMinion.BeamDuration - Projectile.timeLeft;
            Projectile.rotation = Projectile.ai[0] + elapsed * AresMinion.BeamSpinPerTick;

            Lighting.AddLight(Projectile.Center, 0.2f, 0.8f, 1.2f);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float width = 70f * WidthScale();
            if (width <= 1f)
                return false;

            Vector2 end = Projectile.Center + Projectile.rotation.ToRotationVector2() * AresMinion.BeamLength;
            float unused = 0f;

            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, end, width, ref unused);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float width = 70f * WidthScale();
            if (width <= 0.5f)
                return false;

            SpriteBatch sb = Main.spriteBatch;
            Texture2D px = TextureAssets.MagicPixel.Value;
            Vector2 start = Projectile.Center - Main.screenPosition;
            Vector2 beamOrigin = new Vector2(0f, 0.5f);
            float length = AresMinion.BeamLength;

            sb.Draw(px, start, new Rectangle(0, 0, 1, 1), new Color(30, 140, 255, 0) * 0.30f, Projectile.rotation, beamOrigin, new Vector2(length, width * 1.7f), SpriteEffects.None, 0f);
            sb.Draw(px, start, new Rectangle(0, 0, 1, 1), new Color(80, 220, 255, 0) * 0.55f, Projectile.rotation, beamOrigin, new Vector2(length, width), SpriteEffects.None, 0f);
            sb.Draw(px, start, new Rectangle(0, 0, 1, 1), new Color(255, 255, 255, 0) * 0.85f, Projectile.rotation, beamOrigin, new Vector2(length, width * 0.35f), SpriteEffects.None, 0f);

            return false;
        }
    }
}