using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles
{
    internal static class YharonMinionUtil
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
        public static void DrawFrame(Projectile projectile, Color color, Vector2 position, float rotation)
        {
            Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
            int frameHeight = texture.Height / Main.projFrames[projectile.type];
            Rectangle frame = new Rectangle(0, frameHeight * projectile.frame, texture.Width, frameHeight);
            Main.EntitySpriteDraw(texture, position, frame, color, rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), projectile.scale, SpriteEffects.None, 0);
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

        public static void ApplyDragonfire(NPC target, int ticks)
        {
            target.AddBuff(ModContent.BuffType<Dragonfire>(), ticks);
        }
    }
    public class YharonMinionFlareBomb : ModProjectile
    {
        private const float HomingInertia = 25f;
        private const float HomingSpeed = 26f;

        public override string Texture => "CalamityMod/Projectiles/Boss/FlareBomb";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 66;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.scale = 1.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.alpha = 50;
            Projectile.timeLeft = 180;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        private bool Straight => Projectile.ai[0] == 1f;

        public override void AI()
        {
            YharonMinionUtil.AnimateFrames(Projectile, 4);
            Lighting.AddLight(Projectile.Center, 0.5f, 0.25f, 0f);

            if (Projectile.timeLeft > 30 && Projectile.alpha > 0)
                Projectile.alpha = Math.Max(Projectile.alpha - 25, 0);

            if (Straight)
            {
                if (Projectile.localAI[0] == 0f)
                {
                    Projectile.localAI[0] = 1f;
                    Projectile.timeLeft = 90;   
                }
                return;
            }

            Projectile.localAI[1]++;
            if (Projectile.localAI[1] > 8f)
            {
                NPC target = YharonMinionUtil.FindTarget(Projectile, 1800f);
                if (target != null && Vector2.Distance(Projectile.Center, target.Center) > 20f)
                {
                    Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.UnitY);
                    Projectile.velocity = (Projectile.velocity * (HomingInertia - 1f) + direction * HomingSpeed) / HomingInertia;
                }
            }

            foreach (Projectile other in Main.ActiveProjectiles)
            {
                if (other.whoAmI == Projectile.whoAmI || other.type != Projectile.type)
                    continue;
                if (Vector2.Distance(Projectile.Center, other.Center) < 24f)
                {
                    Projectile.velocity.X += Projectile.position.X < other.position.X ? -0.5f : 0.5f;
                    Projectile.velocity.Y += Projectile.position.Y < other.position.Y ? -0.5f : 0.5f;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor) => new Color(200, 200, 200, Projectile.alpha);

        public override bool PreDraw(ref Color lightColor)
        {
            YharonMinionUtil.DrawFrame(Projectile, Projectile.GetAlpha(lightColor), Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), Projectile.rotation);
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
            => CalamityMod.CalamityUtils.CircularHitboxCollision(Projectile.Center, Projectile.width * 0.5f - 8f, targetHitbox);

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            YharonMinionUtil.ApplyDragonfire(target, 120);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            Projectile.ExpandHitboxBy(96);   // explosion radius

            if (!Main.dedServ)
            {
                for (int d = 0; d < 2; d++)
                {
                    int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CopperCoin, 0f, 0f, 100, default, 1f);
                    Main.dust[idx].velocity *= 3f;
                }
                for (int d = 0; d < 4; d++)
                {
                    int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CopperCoin, 0f, 0f, 100, default, 2f);
                    Main.dust[idx].noGravity = true;
                    Main.dust[idx].velocity *= 5f;
                    idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CopperCoin, 0f, 0f, 100, default, 1f);
                    Main.dust[idx].velocity *= 2f;
                }

                Vector2 source = Projectile.Center - new Vector2(24f, 24f);
                for (int g = 0; g < 4; g++)
                {
                    Gore gore = Main.gore[Gore.NewGore(Projectile.GetSource_Death(), source, default, Main.rand.Next(61, 64), 1f)];
                    gore.velocity *= 0.66f;
                    gore.velocity += new Vector2(g % 2 == 0 ? 1f : -1f, g < 2 ? 1f : -1f);
                }
            }

            Projectile.Damage();   
        }
    }

    public class YharonMinionFireball : ModProjectile
    {
        private const float Speed = 15f;

        public override string Texture => "CalamityMod/Projectiles/Boss/YharonFireball";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            ProjectileID.Sets.TrailCacheLength[Type] = 20;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 120;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            YharonMinionUtil.AnimateFrames(Projectile, 4);

            Vector2 heading = Projectile.velocity.SafeNormalize(Vector2.UnitX);
            NPC target = YharonMinionUtil.FindTarget(Projectile, 1800f);
            if (target != null)
                heading = (heading * 14f + (target.Center - Projectile.Center).SafeNormalize(heading)).SafeNormalize(heading);
            Projectile.velocity = heading * Speed;

            // The boss' fireball art points down at rotation 0
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            Lighting.AddLight(Projectile.Center, 1f, 0.6f, 0.2f);
        }

        public override Color? GetAlpha(Color lightColor) => new Color(200, 200, 200, Projectile.alpha);

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            int frameHeight = texture.Height / Main.projFrames[Type];
            Rectangle frame = new Rectangle(0, frameHeight * Projectile.frame, texture.Width, frameHeight);
            Vector2 origin = frame.Size() / 2f;

            // Long trail, like the boss'
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero)
                    continue;
                Vector2 trailPosition = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Color trailColor = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, trailPosition, frame, trailColor, Projectile.oldRot[i], origin, Projectile.scale, SpriteEffects.None, 0);
            }

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), frame, Projectile.GetAlpha(lightColor),
                Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            YharonMinionUtil.ApplyDragonfire(target, 180);
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                int rainType = ModContent.ProjectileType<YharonMinionRainFireball>();
                for (int i = 0; i < 3; i++)
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, new Vector2(-3f + 3f * i, -50f), rainType, Projectile.damage, 0f, Projectile.owner);
                for (int i = 0; i < 2; i++)
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, new Vector2(-5f + 10f * i, -75f), rainType, Projectile.damage, 0f, Projectile.owner);
            }

            SoundEngine.PlaySound(SoundID.Item14 with { Volume = SoundID.Item14.Volume * 0.5f }, Projectile.Center);
            Projectile.ExpandHitboxBy(120);

            if (!Main.dedServ)
            {
                for (int d = 0; d < 2; d++)
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0f, 0f, 50, default, 1.5f);
                for (int d = 0; d < 20; d++)
                {
                    int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0f, 0f, 0, default, 2.5f);
                    Main.dust[idx].noGravity = true;
                    Main.dust[idx].velocity *= 3f;
                    idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0f, 0f, 50, default, 1.5f);
                    Main.dust[idx].velocity *= 2f;
                    Main.dust[idx].noGravity = true;
                }
            }

            Projectile.Damage();
        }
    }

    public class YharonMinionRainFireball : ModProjectile
    {
        public override string Texture => "CalamityMod/Projectiles/Boss/YharonFireball";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            ProjectileID.Sets.TrailCacheLength[Type] = 4;
            ProjectileID.Sets.TrailingMode[Type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.Opacity = 0.25f;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 420;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            YharonMinionUtil.AnimateFrames(Projectile, 4);

            if (Projectile.velocity.Y >= -16f && Projectile.Opacity < 1f)
            {
                Projectile.Opacity = 1f;
                SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);

                if (!Main.dedServ)
                {
                    int dustAmount = 36;
                    for (int i = 0; i < dustAmount; i++)
                    {
                        Vector2 dustSpawn = Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width / 2f, Projectile.height) * 0.5f;
                        dustSpawn = dustSpawn.RotatedBy((i - (dustAmount / 2 - 1)) * MathHelper.TwoPi / dustAmount) + Projectile.Center;
                        Vector2 dustVelocity = dustSpawn - Projectile.Center;
                        int dust = Dust.NewDust(dustSpawn + dustVelocity, 0, 0, DustID.Pixie, dustVelocity.X, dustVelocity.Y);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                        Main.dust[dust].velocity = dustVelocity;
                    }
                }
            }

            if (Projectile.velocity.Y < -1f)
                Projectile.velocity.Y *= 0.97f;      
            else
            {
                Projectile.velocity.Y += 0.2f;       
                if (Projectile.velocity.Y > 16f)
                    Projectile.velocity.Y = 16f;
            }
            Projectile.velocity.X *= 0.995f;

            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonFireball", 3), Projectile.Center);
            }

            Lighting.AddLight(Projectile.Center, 0.8f, 0.45f, 0.15f);

            if (!Main.dedServ && Main.rand.NextBool(16))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0f, 0f, 200, default, 1f);
                dust.scale *= 0.7f;
                dust.velocity += Projectile.velocity * 0.25f;
            }
        }

        public override bool? CanHitNPC(NPC target) => Projectile.velocity.Y >= -16f ? null : false;

        public override Color? GetAlpha(Color lightColor) => new Color(200, 200, 200, Projectile.alpha);

        public override bool PreDraw(ref Color lightColor)
        {
            CalamityMod.CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Type], lightColor, 1);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            YharonMinionUtil.ApplyDragonfire(target, 120);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14 with { Volume = SoundID.Item14.Volume * 0.5f }, Projectile.Center);
            Projectile.ExpandHitboxBy(120);

            if (!Main.dedServ)
            {
                for (int d = 0; d < 2; d++)
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0f, 0f, 100, default, 1.5f);
                for (int d = 0; d < 20; d++)
                {
                    int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0f, 0f, 0, default, 2.5f);
                    Main.dust[idx].noGravity = true;
                    Main.dust[idx].velocity *= 3f;
                    idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0f, 0f, 100, default, 1.5f);
                    Main.dust[idx].velocity *= 2f;
                    Main.dust[idx].noGravity = true;
                }
            }

            Projectile.Damage();
        }
    }

    public class YharonMinionFlarenado : ModProjectile
    {
        private const float ScaleBase = 24f;
        private const float ScaleMult = 1f;
        private const float BaseWidth = 320f;
        private const float BaseHeight = 88f;

        public override string Texture => "CalamityMod/Projectiles/Boss/Flarenado";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 12;
        }

        public override void SetDefaults()
        {
            Projectile.width = 320;
            Projectile.height = 88;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        public override void AI()
        {
            if (Projectile.velocity.X != 0f)
                Projectile.direction = Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X);

            YharonMinionUtil.AnimateFrames(Projectile, 2);

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                Projectile.scale = (ScaleBase - Projectile.ai[1]) * ScaleMult / ScaleBase;
                Projectile.ExpandHitboxBy((int)(BaseWidth * Projectile.scale), (int)(BaseHeight * Projectile.scale));
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] != -1f)
            {
                Projectile.scale = (ScaleBase - Projectile.ai[1]) * ScaleMult / ScaleBase;
                Projectile.width = (int)(BaseWidth * Projectile.scale);
                Projectile.height = (int)(BaseHeight * Projectile.scale);
            }

            if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                Projectile.alpha = Math.Max(Projectile.alpha - 30, 60);
            else
                Projectile.alpha = Math.Min(Projectile.alpha + 30, 150);

            if (Projectile.ai[0] > 0f)
                Projectile.ai[0] -= 1f;

            if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
                Vector2 center = Projectile.Center;
                center.Y -= BaseHeight * Projectile.scale / 2f;
                float nextScale = (ScaleBase - Projectile.ai[1] + 1f) * ScaleMult / ScaleBase;
                center.Y -= BaseHeight * nextScale / 2f;
                center.Y += 2f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), center, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner,
                    11f, Projectile.ai[1] - 1f);
            }

            // Once built, every segment sways side to side
            if (Projectile.ai[0] <= 0f)
            {
                float swaySize = MathHelper.Pi / 30f;
                float smallWidth = Projectile.width / 5f * 2f;
                float change = (float)(Math.Cos(swaySize * -Projectile.ai[0]) - 0.5) * smallWidth;
                Projectile.position.X -= change * -Projectile.direction;
                Projectile.ai[0] -= 1f;
                change = (float)(Math.Cos(swaySize * -Projectile.ai[0]) - 0.5) * smallWidth;
                Projectile.position.X += change * -Projectile.direction;
            }

            Lighting.AddLight(Projectile.Center, 1f, 0.5f, 0.1f);
        }

        public override bool? CanHitNPC(NPC target) => Projectile.timeLeft <= 480 ? null : false;

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 53, Projectile.alpha);

        public override bool PreDraw(ref Color lightColor)
        {
            YharonMinionUtil.DrawFrame(Projectile, Projectile.GetAlpha(lightColor), Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), Projectile.rotation);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            YharonMinionUtil.ApplyDragonfire(target, 120);
        }
    }

}
