using Luminance.Common.Utilities;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NoxusBoss.Core.BaseEntities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles
{
    public class PlayerSuperCosmicBeam : BasePrimitiveLaserbeam
    {
        public override bool UseStandardDrawing => true;

        public override int LaserPointCount => 45;

        public override float LaserExtendSpeedInterpolant => 0.15f;

        public override float MaxLaserLength => 7000f;

        public override ManagedShader LaserShader => ShaderManager.GetShader("NoxusBoss.NamelessDeityCosmicLaserShader");

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 20000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 450; 
            Projectile.height = 450;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.timeLeft = 600;
            Projectile.Opacity = 0f;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead || !player.channel)
            {
                Projectile.Kill();
                return false;
            }

            Projectile.timeLeft = 2;

            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 targetVelocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
                if (targetVelocity != Projectile.velocity)
                {
                    Projectile.velocity = targetVelocity;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.Center = player.Center + Projectile.velocity * 50f;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = Projectile.velocity.ToRotation();

            if ((int)Time % 20 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item122 with { Volume = 0.9f, Pitch = -0.4f }, Projectile.Center);
            }

            Projectile.scale = Utilities.InverseLerp(0f, 15f, Time);
            Projectile.Opacity = Utilities.Saturate(Projectile.Opacity + 0.1f);

            return true;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Time <= 5f)
                return false;

            return base.Colliding(projHitbox, targetHitbox);
        }

        public override float LaserWidthFunction(float completionRatio) => Projectile.scale * Projectile.width;

        public override Color LaserColorFunction(float completionRatio) => Color.White * (float)Math.Pow(Projectile.scale, 1.8f) * Projectile.Opacity;

        public override void PrepareLaserShader(ManagedShader laserShader)
        {
            laserShader.TrySetParameter("scrollTime", Main.GlobalTimeWrappedHourly * 1.1f);
            laserShader.TrySetParameter("uStretchReverseFactor", 0.15f);
            laserShader.TrySetParameter("scrollSpeedFactor", 1.3f);
            laserShader.TrySetParameter("lightSmashWidthFactor", 0.05f);
            laserShader.TrySetParameter("lightSmashLengthFactor", 0.9f);
            laserShader.TrySetParameter("lightSmashLengthOffset", -0.005f);
            laserShader.TrySetParameter("startingLightBrightness", 1f);
            laserShader.TrySetParameter("maxLightTexturingDarkness", 0.6f);
            laserShader.TrySetParameter("lightSmashEdgeNoisePower", 0.5f);
            laserShader.TrySetParameter("lightSmashOpacity", 1f);
            laserShader.TrySetParameter("laserDirection", Projectile.velocity);

            laserShader.SetTexture(ModContent.Request<Texture2D>("NoxusBoss/Textures/Extra/Cosmos"), 1, SamplerState.AnisotropicWrap);
            laserShader.SetTexture(ModContent.Request<Texture2D>("NoxusBoss/Textures/Extra/FireNoiseA"), 2, SamplerState.AnisotropicWrap);
            laserShader.SetTexture(ModContent.Request<Texture2D>("NoxusBoss/Textures/Extra/CrackedNoiseA"), 3, SamplerState.AnisotropicWrap);
            laserShader.SetTexture(ModContent.Request<Texture2D>("NoxusBoss/Textures/Extra/Void"), 4, SamplerState.AnisotropicWrap);
            laserShader.SetTexture(ModContent.Request<Texture2D>("NoxusBoss/Textures/Extra/SharpNoise"), 5, SamplerState.AnisotropicWrap);
            laserShader.SetTexture(ModContent.Request<Texture2D>("NoxusBoss/Textures/Extra/FireNoiseA"), 6, SamplerState.AnisotropicWrap);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            ManagedShader beamShader = LaserShader;
            if (beamShader is not null)
            {
                PrepareLaserShader(beamShader);
            }

            return base.PreDraw(ref lightColor);
        }
    }
}
