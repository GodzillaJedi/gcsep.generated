using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Sounds;
using gcsep.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles
{
    public abstract class ExoMinionBeamBase : ModProjectile
    {
        private const float GrowTime = 20f;
        private const float ShrinkTime = 20f;

        protected abstract int Duration { get; }
        protected abstract float MaxLength { get; }
        protected abstract Texture2D BeginTexture { get; }
        protected abstract Texture2D MiddleTexture { get; }
        protected abstract Texture2D EndTexture { get; }
        protected abstract int DustType { get; }

        protected virtual Color DustColor => default;
        protected virtual Color OverlayColor => new Color(250, 250, 250, 100);
        protected virtual Color LightColor => Color.White;
        protected virtual float BodyStartBackOffset => 0f;
        protected virtual int HitCooldown => 12;

        protected abstract bool UpdateFromMaster(Projectile master, float elapsed);

        protected virtual void BeamTick(float elapsed) { }

        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 10000;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.timeLeft = Duration;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = HitCooldown;
        }

        public override void AI()
        {
            int index = Projectile.GetByUUID(Projectile.owner, (int)Projectile.ai[1]);
            if (index < 0 || index >= Main.maxProjectiles || !Main.projectile[index].active)
            {
                Projectile.Kill();
                return;
            }

            float elapsed = Duration - Projectile.timeLeft;
            if (!UpdateFromMaster(Main.projectile[index], elapsed))
            {
                Projectile.Kill();
                return;
            }

            float grow = MathHelper.Clamp(elapsed / GrowTime, 0f, 1f);
            float shrink = MathHelper.Clamp(Projectile.timeLeft / ShrinkTime, 0f, 1f);
            Projectile.scale = Math.Min(grow, shrink);

            DelegateMethods.v3_1 = LightColor.ToVector3() * 0.5f;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * MaxLength, Projectile.width * Projectile.scale, DelegateMethods.CastLight);

            Projectile.frameCounter++;
            if (Projectile.frameCounter % 5 == 0)
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Type];

            if (!Main.dedServ && Projectile.scale > 0.3f)
            {
                Vector2 tip = Projectile.Center + Projectile.velocity * (MaxLength - 14f);
                if (Vector2.Distance(tip, Main.LocalPlayer.Center) < 2500f)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        float dustDirection = Projectile.velocity.ToRotation() + Main.rand.NextBool().ToDirectionInt() * MathHelper.PiOver2;
                        Vector2 dustVelocity = dustDirection.ToRotationVector2() * Main.rand.NextFloat(2f, 4f);
                        Dust dust = Dust.NewDustDirect(tip, 0, 0, DustType, dustVelocity.X, dustVelocity.Y, 0, DustColor, 1f);
                        dust.noGravity = true;
                        dust.scale = 1.7f;
                    }
                }
            }

            BeamTick(elapsed);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.scale < 0.3f || Projectile.velocity == Vector2.Zero)
                return false;

            float width = MiddleTexture.Width * Projectile.scale * 0.7f;
            Vector2 end = Projectile.Center + Projectile.velocity * MaxLength;
            float unused = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, end, width, ref unused);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
                return false;

            if (Projectile.scale < 0.001f)
                return false;

            Texture2D begin = BeginTexture;
            Texture2D middle = MiddleTexture;
            Texture2D end = EndTexture;
            int frames = Main.projFrames[Type];

            Color beamColor = OverlayColor;
            Rectangle startFrameArea = begin.Frame(1, frames, 0, Projectile.frame);
            Rectangle middleFrameArea = middle.Frame(1, frames, 0, Projectile.frame);
            Rectangle endFrameArea = end.Frame(1, frames, 0, Projectile.frame);

            Main.EntitySpriteDraw(begin, Projectile.Center - Main.screenPosition, startFrameArea, beamColor, Projectile.rotation,
                begin.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);

            float laserBodyLength = MaxLength + middleFrameArea.Height;
            Vector2 centerOnLaser = Projectile.Center + Projectile.velocity * -BodyStartBackOffset;
            if (laserBodyLength > 0f)
            {
                float laserOffset = middleFrameArea.Height * Projectile.scale;
                float incrementalBodyLength = 0f;
                while (incrementalBodyLength + 1f < laserBodyLength)
                {
                    Main.EntitySpriteDraw(middle, centerOnLaser - Main.screenPosition, middleFrameArea, beamColor, Projectile.rotation,
                        middle.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0);
                    incrementalBodyLength += laserOffset;
                    centerOnLaser += Projectile.velocity * laserOffset;
                    middleFrameArea.Y += middle.Height / frames;
                    if (middleFrameArea.Y + middleFrameArea.Height > middle.Height)
                        middleFrameArea.Y = 0;
                }
            }

            Main.EntitySpriteDraw(end, centerOnLaser - Main.screenPosition, endFrameArea, beamColor, Projectile.rotation,
                end.Size() * 0.5f, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }

    public class AresMinionBeam : ExoMinionBeamBase
    {
        private const float SpinDivisor = 340f;

        private static Asset<Texture2D> beginAsset;
        private static Asset<Texture2D> middleAsset;
        private static Asset<Texture2D> endAsset;

        protected override int Duration => AresMinion.BeamDuration;
        protected override float MaxLength => AresMinion.BeamLength;
        protected override int DustType => 107;
        protected override Color DustColor => new Color(0, 255, 255);

        protected override Texture2D BeginTexture => (beginAsset ??= ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/AresDeathBeamStart", AssetRequestMode.ImmediateLoad)).Value;
        protected override Texture2D MiddleTexture => (middleAsset ??= ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresDeathBeamMiddle", AssetRequestMode.ImmediateLoad)).Value;
        protected override Texture2D EndTexture => (endAsset ??= ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresDeathBeamEnd", AssetRequestMode.ImmediateLoad)).Value;

        protected override bool UpdateFromMaster(Projectile master, float elapsed)
        {
            if (!(master.ModProjectile is AresMinion ares))
                return false;

            if (elapsed > 5f && (int)ares.AttackPattern != AresMinion.StateBeams)
                return false;

            float angle = Projectile.ai[0] + MathHelper.Pi * elapsed * elapsed / (AresMinion.BeamDuration * SpinDivisor);
            Vector2 direction = angle.ToRotationVector2();

            Projectile.velocity = direction;
            Projectile.rotation = angle - MathHelper.PiOver2;

            Projectile.Center = ares.CoreWorldPos + direction * MathHelper.Lerp(35f, 127f, Projectile.scale * Projectile.scale);
            return true;
        }
    }

    public class ThanatosMinionBeam : ExoMinionBeamBase
    {
        public const int BeamDuration = 180;           
        private const float SideLaserInterval = 80f;   
        private const float SideLaserSpacing = 320f;
        private const float SideLaserDamageMult = 0.25f;

        private static Asset<Texture2D> beginAsset;
        private static Asset<Texture2D> middleAsset;
        private static Asset<Texture2D> endAsset;

        protected override int Duration => BeamDuration;
        protected override float MaxLength => 3600f;
        protected override int DustType => (int)CalamityDusts.Brimstone;
        protected override float BodyStartBackOffset => 5f;

        protected override Texture2D BeginTexture => (beginAsset ??= ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/ThanatosBeamStart", AssetRequestMode.ImmediateLoad)).Value;
        protected override Texture2D MiddleTexture => (middleAsset ??= ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ThanatosBeamMiddle", AssetRequestMode.ImmediateLoad)).Value;
        protected override Texture2D EndTexture => (endAsset ??= ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/ThanatosBeamEnd", AssetRequestMode.ImmediateLoad)).Value;

        protected override bool UpdateFromMaster(Projectile master, float elapsed)
        {
            if (!(master.ModProjectile is ThanatosMinion head))
                return false;

            if (elapsed > 5f && !head.IsDeathrayPhase)
                return false;

            Vector2 direction = head.BeamDirection;
            Projectile.velocity = direction;
            Projectile.rotation = direction.ToRotation() - MathHelper.PiOver2;
            Projectile.Center = master.Center + direction * Projectile.scale * 174f;
            return true;
        }

        protected override void BeamTick(float elapsed)
        {
            if (elapsed < 20f || elapsed % SideLaserInterval != 0f)
                return;

            SoundEngine.PlaySound(CommonCalamitySounds.LaserCannonSound, Projectile.Center);

            if (Main.myPlayer != Projectile.owner)
                return;

            Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.UnitY);
            Vector2 firePosition = Projectile.Center + direction * SideLaserSpacing;
            int count = (int)(MaxLength / SideLaserSpacing);
            int damage = (int)(Projectile.damage * SideLaserDamageMult);
            int type = ModContent.ProjectileType<ExoMinionLaser>();

            for (int i = 0; i < count; i++)
            {
                for (int side = 0; side < 2; side++)
                {
                    Vector2 velocity = direction.RotatedBy(MathHelper.Pi * side + MathHelper.PiOver2) * 3f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), firePosition, velocity, type, damage, 0f, Projectile.owner);
                }
                firePosition += direction * SideLaserSpacing;
            }
        }
    }
}
