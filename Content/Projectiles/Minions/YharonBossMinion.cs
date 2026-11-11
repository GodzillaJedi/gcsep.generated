using CalamityMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using YharonBoss = CalamityMod.NPCs.Yharon.Yharon;

namespace gcsep.Content.Projectiles.Minions
{
    public class YharonBossMinion : ModProjectile
    {
        private enum State
        {
            Bombard = 0,
            Dash = 1,
            Inferno = 2
        }

        private const float SpriteScale = 1f;            

        private const float HoverX = 450f;
        private const float HoverY = 250f;
        private const float MaxSpeed = 40f;

        private const int BombardDuration = 180;
        private const int FirstFireballTick = 20;
        private const int FireballInterval = 14;
        private const int RoarWarningTick = 150;

        private const float DashSpeed = 32f;
        private const int DashDuration = 22;
        private const int DashesPerCycle = 3;

        private const int InfernoRoarTick = 30;
        private const int InfernoFireballTick = 45;
        private const int InfernoRingTick = 60;
        private const int InfernoDuration = 90;
        private const int RingProjectiles = 16;

        private const float BombDamageMult = 0.8f;        
        private const float BigFireballDamageMult = 1.5f; 
        private const float RingDamageMult = 0.5f;        
        private const float FlarenadoDamageMult = 1f;     

        public ref float AttackState => ref Projectile.ai[0];
        public ref float AttackTimer => ref Projectile.ai[1];
        public ref float DashCounter => ref Projectile.ai[2];
        private ref float HoverSide => ref Projectile.localAI[0];   

        private float powerGlow;                                     

        private State CurrentState => (State)(int)AttackState;

        public override string Texture => "CalamityMod/NPCs/Yharon/Yharon";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;   
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;   
            ProjectileID.Sets.DrawScreenCheckFluff[Projectile.type] = 1000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
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
        }

        public override bool? CanDamage() => CurrentState == State.Dash;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead || !player.GetModPlayer<GCSEPlayer>().YharonSummon)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;

            NPC target = Projectile.Center.MinionHoming(2800f, player);

            if (target != null)
                AttackAI(target);
            else
                IdleAI(player);

            bool powered = target != null && CurrentState != State.Bombard;
            powerGlow = MathHelper.Lerp(powerGlow, powered ? 1f : 0f, 0.08f);

            if (Projectile.velocity.Length() > MaxSpeed)
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitX) * MaxSpeed;

            UpdateRotation(target != null && CurrentState == State.Dash);

            Lighting.AddLight(Projectile.Center, 1.4f, 0.5f, 0.15f);
        }

        private void SwitchState(State next)
        {
            AttackState = (float)next;
            AttackTimer = 0f;
            DashCounter = 0f;
            HoverSide = 0f;
            Projectile.netUpdate = true;
        }

        private void AttackAI(NPC target)
        {
            AttackTimer++;

            switch (CurrentState)
            {
                case State.Bombard:
                    BombardState(target);
                    break;
                case State.Dash:
                    DashState(target);
                    break;
                case State.Inferno:
                    InfernoState(target);
                    break;
                default:
                    SwitchState(State.Bombard);
                    break;
            }
        }

        private void BombardState(NPC target)
        {
            if (HoverSide == 0f)
                HoverSide = Projectile.Center.X < target.Center.X ? -1f : 1f;

            Vector2 hoverSpot = target.Center + new Vector2(HoverSide * HoverX, -HoverY);
            Projectile.velocity = (Projectile.velocity * 15f + (hoverSpot - Projectile.Center) * 0.15f) / 16f;
            FaceTarget(target);

            if (AttackTimer >= FirstFireballTick && AttackTimer < RoarWarningTick && (AttackTimer - FirstFireballTick) % FireballInterval == 0f)
                SpitFireball(target);

            if (AttackTimer == RoarWarningTick)
                SoundEngine.PlaySound(YharonBoss.ShortRoarSound, Projectile.Center);

            if (AttackTimer >= RoarWarningTick)
                Projectile.frame = 6;
            else
                Animate(0, 4, 5);

            if (AttackTimer >= BombardDuration)
            {
                SwitchState(State.Dash);
                LaunchDash(target, true);
            }
        }

        private void DashState(NPC target)
        {
            Projectile.frame = 5;
            FaceVelocity();

            if (!Main.dedServ && Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0f, 0f, 100, default, 1.8f);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }

            if (AttackTimer >= DashDuration)
            {
                AttackTimer = 0f;
                DashCounter++;

                if (DashCounter >= DashesPerCycle)
                    SwitchState(State.Inferno);
                else
                    LaunchDash(target, false);

                Projectile.netUpdate = true;
            }
        }

        private void InfernoState(NPC target)
        {
            Projectile.velocity *= 0.82f;
            FaceTarget(target);

            if (AttackTimer == InfernoRoarTick)
                SoundEngine.PlaySound(YharonBoss.ShortRoarSound, Projectile.Center);

            if (AttackTimer >= InfernoRoarTick && AttackTimer < InfernoDuration)
                Projectile.frame = 6;
            else
                Animate(0, 4, 5);

            if (!Main.dedServ && AttackTimer >= 20f)
            {
                Vector2 ringOffset = Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(50f, 100f);
                Dust dust = Dust.NewDustDirect(Projectile.Center + ringOffset, 0, 0, DustID.SolarFlare);
                dust.noGravity = true;
                dust.velocity = -ringOffset.SafeNormalize(Vector2.Zero) * 5f;
            }

            if (AttackTimer == InfernoFireballTick)
                LobFireball(target);

            if (AttackTimer == InfernoRingTick)
                FireRing();

            if (AttackTimer >= InfernoDuration)
            {
                SummonFlarenado(target);
                SwitchState(State.Bombard);
            }
        }

        private void IdleAI(Player player)
        {
            AttackState = (float)State.Bombard;
            AttackTimer = 0f;
            DashCounter = 0f;
            HoverSide = 0f;

            float phase = Main.GlobalTimeWrappedHourly * 2.8f;
            Vector2 idleSpot = player.Center + new Vector2((float)Math.Sin(phase) * 220f, -160f);
            Vector2 displacement = idleSpot - Projectile.Center;

            if (displacement.Length() > 2200f)
                Projectile.Center = player.Center;

            Projectile.velocity = (Projectile.velocity * 20f + displacement * 0.12f) / 21f;
            Animate(0, 4, 6);
            FaceVelocity();
        }
        private Vector2 MouthPosition(Vector2 toward)
        {
            Vector2 direction = (toward - Projectile.Center).SafeNormalize(Vector2.UnitX);
            int facing = Projectile.spriteDirection == -1 ? -1 : 1;
            return Projectile.Center + direction * (Projectile.width + 20f) * 0.5f + new Vector2(60f * facing, -15f);
        }

        private void SpitFireball(NPC target)
        {
            Vector2 mouth = MouthPosition(target.Center);
            SoundEngine.PlaySound(YharonBoss.FireSound, mouth);

            if (Main.myPlayer != Projectile.owner)
                return;

            // A homing flare bomb (ai[0] = 0: homes on the nearest enemy)
            Vector2 direction = (target.Center - mouth).SafeNormalize(Vector2.UnitX).RotatedByRandom(0.1f);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), mouth, direction * 11f,
                ModContent.ProjectileType<YharonMinionFlareBomb>(), (int)(Projectile.damage * BombDamageMult), 0f, Projectile.owner);
        }

        private void LobFireball(NPC target)
        {
            Vector2 mouth = MouthPosition(target.Center);
            SoundEngine.PlaySound(YharonBoss.OrbSound, mouth);

            if (Main.myPlayer != Projectile.owner)
                return;

            Vector2 direction = (target.Center - mouth).SafeNormalize(Vector2.UnitX);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), mouth, direction * 15f,
                ModContent.ProjectileType<YharonMinionFireball>(), (int)(Projectile.damage * BigFireballDamageMult), 0f, Projectile.owner);
        }

        private void LaunchDash(NPC target, bool roar)
        {
            Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);
            Projectile.velocity = direction * DashSpeed;

            if (roar)
                SoundEngine.PlaySound(YharonBoss.RoarSound, Projectile.Center);
        }

        private void FireRing()
        {
            SoundEngine.PlaySound(YharonBoss.FireSound, Projectile.Center);

            if (Main.myPlayer != Projectile.owner)
                return;

            int damage = (int)(Projectile.damage * RingDamageMult);
            for (int i = 0; i < RingProjectiles; i++)
            {
                Vector2 velocity = Vector2.UnitY.RotatedBy(MathHelper.TwoPi * i / RingProjectiles) * 9f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
                    ModContent.ProjectileType<YharonMinionFlareBomb>(), damage, 0f, Projectile.owner, 1f);   // ai[0] = 1: flies straight
            }
        }
        private void SummonFlarenado(NPC target)
        {
            SoundEngine.PlaySound(YharonBoss.OrbSound, Projectile.Center);

            if (Main.myPlayer != Projectile.owner)
                return;

            int tileX = Math.Clamp((int)(target.Center.X / 16f), 10, Main.maxTilesX - 10);
            int startY = Math.Clamp((int)(target.Center.Y / 16f), 10, Main.maxTilesY - 110);
            int groundY = startY + 25;   

            for (int y = startY; y < startY + 25; y++)
            {
                Tile tile = Main.tile[tileX, y];
                if (tile.HasTile && (Main.tileSolid[tile.TileType] || tile.LiquidAmount != 0))
                {
                    groundY = y;
                    break;
                }
            }

            Vector2 basePosition = new Vector2(tileX * 16f + 8f, groundY * 16f - 24f);
            int tornado = Projectile.NewProjectile(Projectile.GetSource_FromThis(), basePosition, Vector2.Zero,
                ModContent.ProjectileType<YharonMinionFlarenado>(), (int)(Projectile.damage * FlarenadoDamageMult), 4f, Projectile.owner, 11f, 10f);
            if (tornado >= 0 && tornado < Main.maxProjectiles)
                Main.projectile[tornado].netUpdate = true;
        }
        private void FaceTarget(NPC target)
        {
            Projectile.spriteDirection = target.Center.X >= Projectile.Center.X ? 1 : -1;
        }
        private void FaceVelocity()
        {
            if (Projectile.velocity.X > 0.6f)
                Projectile.spriteDirection = 1;
            else if (Projectile.velocity.X < -0.6f)
                Projectile.spriteDirection = -1;
        }

        private void UpdateRotation(bool dashing)
        {
            float wanted;
            if (dashing && Projectile.velocity != Vector2.Zero)
            {
                wanted = Projectile.velocity.ToRotation();
                if (Projectile.spriteDirection == -1)
                    wanted += MathHelper.Pi;
            }
            else
                wanted = Projectile.velocity.X * 0.025f;  

            Projectile.rotation = StepAngle(Projectile.rotation, wanted, 0.3f);
        }

        private static float StepAngle(float current, float target, float maxChange)
        {
            float difference = MathHelper.WrapAngle(target - current);
            return MathHelper.WrapAngle(current + MathHelper.Clamp(difference, -maxChange, maxChange));
        }

        private void Animate(int firstFrame, int lastFrame, int ticksPerFrame)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= ticksPerFrame)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
            if (Projectile.frame > lastFrame || Projectile.frame < firstFrame)
                Projectile.frame = firstFrame;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            int frameHeight = texture.Height / Main.projFrames[Type];
            Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
            Vector2 origin = frame.Size() / 2f;
            SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            float scale = Projectile.scale * SpriteScale;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);

            bool dashing = CurrentState == State.Dash;
            int afterimages = dashing ? 9 : (powerGlow > 0.3f ? 5 : 0);
            for (int i = 1; i < afterimages; i += 2)
            {
                if (Projectile.oldPos[i] == Vector2.Zero)
                    continue;

                Color trailColor = Color.Lerp(lightColor, dashing ? Color.Red : Color.Orange, 0.5f);
                trailColor = Projectile.GetAlpha(trailColor) * ((afterimages - i) / 15f);
                Vector2 trailPosition = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Main.EntitySpriteDraw(texture, trailPosition, frame, trailColor, Projectile.oldRot[i], origin, scale, effects, 0);
            }

            Main.EntitySpriteDraw(texture, drawPosition, frame, Projectile.GetAlpha(lightColor), Projectile.rotation, origin, scale, effects, 0);

            Color orange = Color.Lerp(Color.White, Color.Orange, 0.5f) * Projectile.Opacity;
            Main.EntitySpriteDraw(YharonBoss.GlowTextureOrange.Value, drawPosition, frame, orange, Projectile.rotation, origin, scale, effects, 0);

            if (powerGlow > 0.01f)
            {
                Color green = Color.Lerp(Color.White, Color.Chartreuse, 0.5f) * Projectile.Opacity * powerGlow;
                Color purple = Color.Lerp(Color.White, Color.BlueViolet, 0.5f) * Projectile.Opacity * powerGlow;
                Main.EntitySpriteDraw(YharonBoss.GlowTextureGreen.Value, drawPosition, frame, green, Projectile.rotation, origin, scale, effects, 0);
                Main.EntitySpriteDraw(YharonBoss.GlowTexturePurple.Value, drawPosition, frame, purple, Projectile.rotation, origin, scale, effects, 0);
            }

            return false;
        }
    }

}