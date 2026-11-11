using CalamityMod.Graphics.Primitives;
using CalamityMod.Sounds;
using gcsep.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles
{
    internal static class ExoTwinsProjectileUtil
    {
        public static NPC FindTarget(Projectile projectile, float range)
        {
            NPC best = null;
            float bestDistance = range;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (!npc.CanBeChasedBy(projectile))
                    continue;
                float distance = Vector2.Distance(projectile.Center, npc.Center);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    best = npc;
                }
            }
            return best;
        }
        public static void AnimateFrames(Projectile projectile, int ticksPerFrame)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > ticksPerFrame)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= Main.projFrames[projectile.type])
                projectile.frame = 0;
        }
        public static void PlasmaSpawnDust(Projectile projectile, int heavy, int light)
        {
            if (Main.dedServ)
                return;

            const float speed1 = 1.8f;
            const float speed2 = 2.8f;
            const float angleRandom = 0.35f;
            float baseAngle = projectile.velocity.ToRotation();

            for (int i = 0; i < heavy; i++)
            {
                Vector2 dustVel = new Vector2(Main.rand.NextFloat(speed1, speed2), 0f).RotatedBy(baseAngle).RotatedBy(-angleRandom).RotatedByRandom(2f * angleRandom);
                int dustType = Main.rand.NextBool() ? 107 : 110;

                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, dustVel.X, dustVel.Y, 200, default, 1.7f);
                Main.dust[d].position = projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 3f;

                d = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, dustVel.X, dustVel.Y, 100, default, 0.8f);
                Main.dust[d].position = projectile.Center + Vector2.UnitY.RotatedByRandom(MathHelper.Pi) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                Main.dust[d].velocity *= 2f;
                Main.dust[d].noGravity = true;
                Main.dust[d].fadeIn = 1f;
                Main.dust[d].color = Color.Green * 0.5f;
            }

            for (int j = 0; j < light; j++)
            {
                Vector2 dustVel = new Vector2(Main.rand.NextFloat(speed1, speed2), 0f).RotatedBy(baseAngle).RotatedBy(-angleRandom).RotatedByRandom(2f * angleRandom);
                int dustType = Main.rand.NextBool() ? 107 : 110;

                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType, dustVel.X, dustVel.Y, 0, default, 2f);
                Main.dust[d].position = projectile.Center + Vector2.UnitX.RotatedByRandom(MathHelper.Pi).RotatedBy(baseAngle) * projectile.width / 3f;
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.5f;
            }
        }
    }
    public class ExoTwinsLaser : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Boss/ArtemisLaser";

        public ref float TopSpeed => ref Projectile.ai[0];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 2000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 3;
            Projectile.timeLeft = 400;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            ExoTwinsProjectileUtil.AnimateFrames(Projectile, 12);
            Lighting.AddLight(Projectile.Center, 0.6f, 0f, 0f);

            if (Projectile.alpha > 0)
                Projectile.alpha = Math.Max(Projectile.alpha - 25, 0);

            if (TopSpeed > 0f && Projectile.velocity.Length() < TopSpeed)
            {
                Projectile.velocity *= 1f + TopSpeed * 0.0016667f;
                if (Projectile.velocity.Length() > TopSpeed)
                    Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * TopSpeed;
            }

            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float)Math.Atan2(-Projectile.velocity.Y, -Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
            => CalamityMod.CalamityUtils.CircularHitboxCollision(projHitbox.Center(), Projectile.Size.Length() * 0.5f, targetHitbox);

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor.R = (byte)(255 * Projectile.Opacity);
            lightColor.G = (byte)(255 * Projectile.Opacity);
            lightColor.B = (byte)(255 * Projectile.Opacity);
            Vector2 drawOffset = Projectile.velocity.SafeNormalize(Vector2.Zero) * -30f;
            Projectile.Center += drawOffset;
            CalamityMod.CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Type], lightColor, 1);
            Projectile.Center -= drawOffset;
            return false;
        }
    }

    public class ExoTwinsFireball : ModProjectile
    {
        private const int LifeTime = 60;

        public override string Texture => "CalamityMod/Projectiles/Boss/ApolloFireball";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 6;
            ProjectileID.Sets.TrailCacheLength[Type] = 4;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.Opacity = 0f;
            Projectile.timeLeft = LifeTime;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            const int fadeInTime = 3;
            Projectile.Opacity = MathHelper.Clamp(1f - ((Projectile.timeLeft - (LifeTime - fadeInTime)) / (float)fadeInTime), 0f, 1f);
            Lighting.AddLight(Projectile.Center, 0f, 0.6f * Projectile.Opacity, 0f);

            ExoTwinsProjectileUtil.AnimateFrames(Projectile, 4);

            NPC target = ExoTwinsProjectileUtil.FindTarget(Projectile, 1400f);
            if (target != null)
            {
                float speed = Math.Max(Projectile.velocity.Length(), 10f);
                Vector2 desired = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitY) * speed;
                Projectile.velocity = (Projectile.velocity * 18f + desired) / 19f;
            }

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - MathHelper.PiOver2;

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                ExoTwinsProjectileUtil.PlasmaSpawnDust(Projectile, 40, 20);
            }
        }

        public override bool? CanHitNPC(NPC target) => Projectile.Opacity == 1f ? null : false;

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor.R = (byte)(255 * Projectile.Opacity);
            lightColor.G = (byte)(255 * Projectile.Opacity);
            lightColor.B = (byte)(255 * Projectile.Opacity);
            CalamityMod.CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Type], lightColor, 1);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Resize(90, 90);
            Projectile.Damage();

            SoundEngine.PlaySound(CommonCalamitySounds.ExoPlasmaExplosionSound, Projectile.Center);

            if (Main.myPlayer == Projectile.owner)
            {
                int type = ModContent.ProjectileType<ExoMinionPlasmaBolt>();
                for (int k = 0; k < 4; k++)
                {
                    Vector2 velocity = new Vector2(0f, -0.5f).RotatedBy(MathHelper.TwoPi / 4f * k);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, velocity, type, (int)(Projectile.damage * 0.4f), 0f, Projectile.owner);
                }
            }

            if (Main.dedServ)
                return;

            for (int i = 0; i < 120; i++)
            {
                float dustScale = i < 40 ? 4f : i < 80 ? 8f : 12f;
                float scale = i < 40 ? 1.2f : i < 80 ? 1.1f : 1f;

                int d = Dust.NewDust(Projectile.Center, 6, 6, Main.rand.NextBool() ? 107 : 110, 0f, 0f, 100, default, 1f);
                Vector2 velocity = Main.dust[d].velocity;
                if (velocity == Vector2.Zero)
                    velocity.X = 1f;
                velocity = velocity.SafeNormalize(Vector2.UnitX) * dustScale;

                Main.dust[d].velocity = Main.dust[d].velocity * 0.5f + velocity;
                Main.dust[d].scale = scale;
                Main.dust[d].noGravity = true;
            }
        }
    }

    public class ExoTwinsRocket : ModProjectile
    {
        private const float StopHomingDistance = 200f;
        private const float HomingInertia = 10f;

        public override string Texture => "CalamityMod/Projectiles/Boss/ApolloRocket";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            ProjectileID.Sets.TrailCacheLength[Type] = 4;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            ExoTwinsProjectileUtil.AnimateFrames(Projectile, 4);
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + MathHelper.PiOver2;

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                ExoTwinsProjectileUtil.PlasmaSpawnDust(Projectile, 20, 10);
            }

            Lighting.AddLight(Projectile.Center, 0.2f, 1f, 0f);

            NPC target = ExoTwinsProjectileUtil.FindTarget(Projectile, 1800f);
            float distance = target != null ? Vector2.Distance(Projectile.Center, target.Center) : float.MaxValue;

            if (distance < StopHomingDistance || Projectile.ai[0] == 1f || Projectile.timeLeft < 480)
            {
                Projectile.ai[0] = 1f;
                if (Projectile.velocity.Length() < 24f)
                    Projectile.velocity *= 1.05f;
                return;
            }

            if (target == null)
                return;

            float speed = Projectile.velocity.Length();
            Vector2 desired = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitY) * speed;
            Projectile.velocity = ((Projectile.velocity * HomingInertia + desired) / (HomingInertia + 1f)).SafeNormalize(Vector2.UnitY) * speed;

            foreach (Projectile other in Main.ActiveProjectiles)
            {
                if (other.whoAmI == Projectile.whoAmI || other.type != Projectile.type)
                    continue;
                if (Vector2.Distance(Projectile.Center, other.Center) < 80f)
                {
                    Projectile.velocity.X += Projectile.position.X < other.position.X ? -0.05f : 0.05f;
                    Projectile.velocity.Y += Projectile.position.Y < other.position.Y ? -0.05f : 0.05f;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            CalamityMod.CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Type], lightColor, 1);
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;
            int height = texture.Height / Main.projFrames[Type];
            Rectangle frame = new Rectangle(0, height * Projectile.frame, texture.Width, height);
            Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/ApolloRocketGlow").Value;
            Main.EntitySpriteDraw(glow, Projectile.Center - Main.screenPosition, frame, Color.White, Projectile.rotation, Projectile.Size / 2f, Projectile.scale, SpriteEffects.None, 0);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Resize(90, 90);
            Projectile.Damage();

            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            if (Main.dedServ)
                return;

            for (int i = 0; i < 12; i++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Main.rand.NextBool() ? 107 : 110, 0f, 0f, 100, default, 2f);
                Main.dust[d].velocity *= 3f;
                if (Main.rand.NextBool())
                {
                    Main.dust[d].scale = 0.5f;
                    Main.dust[d].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int j = 0; j < 15; j++)
            {
                int dustType = Main.rand.NextBool() ? 107 : 110;
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, default, 3f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 5f;
                d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 100, default, 2f);
                Main.dust[d].velocity *= 2f;
            }

            Vector2 source = Projectile.Center - new Vector2(24f, 24f);
            for (int g = 0; g < 4; g++)
            {
                Gore gore = Main.gore[Gore.NewGore(Projectile.GetSource_Death(), source, default, Main.rand.Next(61, 64), 1f)];
                gore.velocity *= 0.66f;
                gore.velocity += new Vector2(g % 2 == 0 ? 1f : -1f, g < 2 ? 1f : -1f);
            }
        }
    }
    public class ExoTwinsDeathray : ModProjectile
    {
        public const int BeamDuration = 180;
        public const float MaxLength = 3600f;
        private const float GrowTime = 20f;
        private const float ShrinkTime = 20f;

        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 10000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = BeamDuration;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            int npcIndex = (int)Projectile.ai[0];
            if (npcIndex < 0 || npcIndex >= Main.maxNPCs || !Main.npc[npcIndex].active || !(Main.npc[npcIndex].ModNPC is ExoTwinsArtemis))
            {
                Projectile.Kill();
                return;
            }
            NPC artemis = Main.npc[npcIndex];

            float elapsed = BeamDuration - Projectile.timeLeft;

            int masterIndex = Projectile.GetByUUID(Projectile.owner, (int)Projectile.ai[1]);
            if (masterIndex < 0 || !Main.projectile[masterIndex].active || !(Main.projectile[masterIndex].ModProjectile is ExoTwinsMinion master))
            {
                Projectile.Kill();
                return;
            }
            if (elapsed > 5f && (int)master.Phase != ExoTwinsMinion.PhaseDeathray)
            {
                Projectile.Kill();
                return;
            }

            Projectile.scale = Math.Min(MathHelper.Clamp(elapsed / GrowTime, 0f, 1f), MathHelper.Clamp(Projectile.timeLeft / ShrinkTime, 0f, 1f));

            Projectile.rotation = artemis.rotation;
            Projectile.velocity = (Projectile.rotation - MathHelper.PiOver2).ToRotationVector2();
            Projectile.Center = artemis.Center + Vector2.UnitY * artemis.gfxOffY + Projectile.velocity * 50f;

            Lighting.AddLight(Projectile.Center + Projectile.velocity * 200f, 1f, 0.5f, 0.1f * Projectile.scale);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.scale < 0.3f || Projectile.velocity == Vector2.Zero)
                return false;

            float unused = 0f;
            Vector2 end = Projectile.Center + Projectile.velocity * MaxLength;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, end, Projectile.width * Projectile.scale * 1.4f, ref unused);
        }

        public float LaserWidthFunction(float _, Vector2 vertexPos) => Projectile.scale * Projectile.width;

        public static Color LaserColorFunction(float completionRatio, Vector2 vertexPos)
        {
            float colorInterpolant = (float)Math.Sin(Main.GlobalTimeWrappedHourly * -3.2f + completionRatio * 23f) * 0.5f + 0.5f;
            return Color.Lerp(Color.Orange, Color.Red, colorInterpolant * 0.67f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero || Projectile.scale < 0.01f)
                return false;

            Vector2 laserEnd = Projectile.Center + Projectile.velocity.SafeNormalize(Vector2.UnitY) * MaxLength;
            Vector2[] baseDrawPoints = new Vector2[8];
            for (int i = 0; i < baseDrawPoints.Length; i++)
                baseDrawPoints[i] = Vector2.Lerp(Projectile.Center, laserEnd, i / (float)(baseDrawPoints.Length - 1f));

            GameShaders.Misc["CalamityMod:ArtemisLaser"].UseColor(Color.Cyan);
            GameShaders.Misc["CalamityMod:ArtemisLaser"].UseImage1("Images/Extra_189");
            GameShaders.Misc["CalamityMod:ArtemisLaser"].UseImage2("Images/Misc/Perlin");
            PrimitiveRenderer.RenderTrail(baseDrawPoints, new(LaserWidthFunction, LaserColorFunction, shader: GameShaders.Misc["CalamityMod:ArtemisLaser"]), 64);
            return false;
        }
    }

}
