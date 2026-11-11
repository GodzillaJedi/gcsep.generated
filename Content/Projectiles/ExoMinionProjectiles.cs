using CalamityMod;
using CalamityMod.Graphics.Primitives;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

public class ExoMinionLaser : ModProjectile
{
    public override string Texture => "CalamityMod/Projectiles/Boss/ThanatosLaser";

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
        Projectile.timeLeft = 600;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1; 
    }

    public override void AI()
    {
        Projectile.frameCounter++;
        if (Projectile.frameCounter > 12)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame > 3)
            Projectile.frame = 0;

        if (Projectile.alpha > 0)
            Projectile.alpha = Math.Max(Projectile.alpha - 25, 0);

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
public class ExoMinionPlasmaBolt : ModProjectile
{
    private const int LifeTime = 360;
    private const float MaxVelocity = 10f;

    public override string Texture => "CalamityMod/Projectiles/Boss/AresPlasmaBolt";

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
        ProjectileID.Sets.TrailCacheLength[Type] = 4;
        ProjectileID.Sets.TrailingMode[Type] = 0;
    }

    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.Opacity = 0f;
        Projectile.penetrate = 2;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = LifeTime;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
    }

    public override void AI()
    {
        if (Projectile.velocity.Length() < MaxVelocity)
        {
            Projectile.velocity *= 1.025f;
            if (Projectile.velocity.Length() > MaxVelocity)
                Projectile.velocity = Projectile.velocity.SafeNormalize(Vector2.UnitY) * MaxVelocity;
        }

        const int fadeOutTime = 15;
        const int fadeInTime = 3;
        if (Projectile.timeLeft < fadeOutTime)
            Projectile.Opacity = MathHelper.Clamp(Projectile.timeLeft / (float)fadeOutTime, 0f, 1f);
        else
            Projectile.Opacity = MathHelper.Clamp(1f - ((Projectile.timeLeft - (LifeTime - fadeInTime)) / (float)fadeInTime), 0f, 1f);

        Lighting.AddLight(Projectile.Center, 0f, 0.4f * Projectile.Opacity, 0f);

        Projectile.frameCounter++;
        if (Projectile.frameCounter > 8)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame > 3)
            Projectile.frame = 0;

        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - MathHelper.PiOver2;
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
}
public class ExoMinionTeslaOrb : ModProjectile
{
    private const int LifeTime = 480;
    private const float DetachDistance = 1200f;

    public ref float Identity => ref Projectile.ai[0];

    public override string Texture => "CalamityMod/Projectiles/Boss/AresTeslaOrb";

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
        ProjectileID.Sets.TrailCacheLength[Type] = 4;
        ProjectileID.Sets.TrailingMode[Type] = 0;
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 3000;
    }

    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.Opacity = 0f;
        Projectile.timeLeft = LifeTime;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 20;
    }

    public override void AI()
    {
        if (Projectile.velocity.Length() < 20.8f)
            Projectile.velocity *= 1.01f;

        const int fadeOutTime = 15;
        const int fadeInTime = 3;
        if (Projectile.timeLeft < fadeOutTime)
            Projectile.Opacity = MathHelper.Clamp(Projectile.timeLeft / (float)fadeOutTime, 0f, 1f);
        else
            Projectile.Opacity = MathHelper.Clamp(1f - ((Projectile.timeLeft - (LifeTime - fadeInTime)) / (float)fadeInTime), 0f, 1f);

        Lighting.AddLight(Projectile.Center, 0.1f * Projectile.Opacity, 0.25f * Projectile.Opacity, 0.25f * Projectile.Opacity);

        Projectile.frameCounter++;
        if (Projectile.frameCounter > 4)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame > 3)
            Projectile.frame = 0;

        if (Projectile.localAI[0] == 0f)
        {
            Projectile.localAI[0] = 1f;
            if (!Main.dedServ)
            {
                for (int i = 0; i < 30; i++)
                {
                    int dustType = Main.rand.NextBool() ? 206 : 229;
                    Vector2 dustVel = new Vector2(Main.rand.NextFloat(1.8f, 2.8f), 0f).RotatedBy(Projectile.velocity.ToRotation()).RotatedByRandom(0.35f);
                    Dust dust = Dust.NewDustPerfect(Projectile.Center + Main.rand.NextVector2Circular(Projectile.width / 2f, Projectile.height / 2f), dustType, dustVel * 2f, 100, default, 2f);
                    dust.noGravity = true;
                }
            }
        }
    }

    public override bool? CanHitNPC(NPC target) => Projectile.Opacity == 1f ? null : false;

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Electrified, 240);
    }
    private Projectile GetOrbToAttachTo()
    {
        foreach (Projectile p in Main.ActiveProjectiles)
        {
            if (p.type != Projectile.type || p.owner != Projectile.owner || p.whoAmI == Projectile.whoAmI)
                continue;
            if (p.ai[0] != Identity + 1f)
                continue;
            if (Vector2.Distance(Projectile.Center, p.Center) > DetachDistance)
                continue;
            return p;
        }
        return null;
    }

    private float WidthFunction(float completionRatio, Vector2 vertexPos)
        => MathHelper.Lerp(0.75f, 1.85f, (float)Math.Sin(MathHelper.Pi * completionRatio)) * Projectile.scale;

    private Color ColorFunction(float completionRatio, Vector2 vertexPos)
    {
        float fadeToWhite = MathHelper.Lerp(0f, 0.65f, (float)Math.Sin(MathHelper.TwoPi * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
        Color baseColor = Color.Lerp(Color.Cyan, Color.White, fadeToWhite);
        return Color.Lerp(baseColor, Color.LightBlue, ((float)Math.Sin(MathHelper.Pi * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f) * 0.8f) * Projectile.Opacity;
    }

    private float BackgroundWidthFunction(float completionRatio, Vector2 vertexPos) => WidthFunction(completionRatio, vertexPos) * 4f;

    private Color BackgroundColorFunction(float completionRatio, Vector2 vertexPos) => Color.CornflowerBlue * Projectile.Opacity * 0.4f;

    public override bool PreDraw(ref Color lightColor)
    {
        Projectile orbToAttachTo = GetOrbToAttachTo();
        if (orbToAttachTo != null)
        {
            List<Vector2> arcPoints = AresTeslaOrb.DetermineElectricArcPoints(Projectile.Center, orbToAttachTo.Center, 117);
            PrimitiveRenderer.RenderTrail(arcPoints, new(BackgroundWidthFunction, BackgroundColorFunction, smoothen: false), 90);
            PrimitiveRenderer.RenderTrail(arcPoints, new(WidthFunction, ColorFunction, smoothen: false), 90);
        }

        lightColor.R = (byte)(255 * Projectile.Opacity);
        lightColor.G = (byte)(255 * Projectile.Opacity);
        lightColor.B = (byte)(255 * Projectile.Opacity);
        CalamityMod.CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Type], lightColor, 1);
        return false;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        if (projHitbox.Intersects(targetHitbox))
            return true;

        float unused = 0f;
        Projectile orbToAttachTo = GetOrbToAttachTo();
        return orbToAttachTo != null && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, orbToAttachTo.Center, 8f, ref unused);
    }
}
public class ExoMinionGaussNuke : ModProjectile
{
    private const int LifeTime = 180;
    private const float StopHomingDistance = 300f;
    private const float Inertia = 10f;

    public override string Texture => "CalamityMod/Projectiles/Boss/AresGaussNukeProjectile";

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 12;
        ProjectileID.Sets.TrailCacheLength[Type] = 4;
        ProjectileID.Sets.TrailingMode[Type] = 0;
    }

    public override void SetDefaults()
    {
        Projectile.width = 100;
        Projectile.height = 100;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.timeLeft = LifeTime;
    }
    public override bool? CanDamage() => false;
    private NPC FindTarget()
    {
        NPC best = null;
        float bestDistance = 2400f;
        foreach (NPC npc in Main.ActiveNPCs)
        {
            if (!npc.CanBeChasedBy(Projectile))
                continue;
            float distance = Vector2.Distance(Projectile.Center, npc.Center);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                best = npc;
            }
        }
        return best;
    }
    public override void AI()
    {
        Projectile.frameCounter++;
        if (Projectile.frameCounter >= 6)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if (Projectile.frame >= 12)
            Projectile.frame = 0;

        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - MathHelper.PiOver2;
        Lighting.AddLight(Projectile.Center, 0.2f, 0.25f, 0.05f);

        if (Projectile.localAI[0] == 0f)
        {
            Projectile.localAI[0] = 1f;
            if (!Main.dedServ)
            {
                for (int i = 0; i < 25; i++)
                {
                    Vector2 dustVel = new Vector2(Main.rand.NextFloat(3f, 13f), 0f).RotatedBy(Projectile.velocity.ToRotation()).RotatedByRandom(0.12f);
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Terra, -dustVel, 0, default, Main.rand.NextFloat(0.5f, 1.6f));
                    dust.noGravity = true;
                }
            }
        }

        if (Projectile.ai[0] == 1f)
        {
            if (Projectile.velocity.Length() < 24f)
                Projectile.velocity *= 1.025f;
        }

        NPC target = FindTarget();
        if (target == null)
            return;

        if (CalamityMod.CalamityUtils.CircularHitboxCollision(Projectile.Center, 45f, target.Hitbox))
        {
            Projectile.Kill();
            return;
        }

        Vector2 toTarget = target.Center - Projectile.Center;
        if (Projectile.ai[0] != 1f && toTarget.Length() < StopHomingDistance)
            Projectile.ai[0] = 1f;

        if (Projectile.ai[0] != 1f)
        {
            float speed = Projectile.velocity.Length();
            Vector2 desired = toTarget.SafeNormalize(Vector2.UnitY) * speed;
            Projectile.velocity = ((Projectile.velocity * Inertia + desired) / (Inertia + 1f)).SafeNormalize(Vector2.UnitY) * speed;
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        CalamityMod.CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Type], lightColor, 1);
        return false;
    }

    public override void PostDraw(Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Type].Value;
        int height = texture.Height / Main.projFrames[Type];
        Rectangle frame = new Rectangle(0, height * Projectile.frame, texture.Width, height);
        Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/AresGaussNukeProjectileGlow").Value;
        Main.EntitySpriteDraw(glow, Projectile.Center - Main.screenPosition, frame, Color.White, Projectile.rotation, Projectile.Size / 2f, Projectile.scale, SpriteEffects.None, 0);
    }

    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(AresGaussNuke.NukeExplosionSound, Projectile.Center);

        if (Main.myPlayer == Projectile.owner)
        {
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero,
                ModContent.ProjectileType<ExoMinionNukeBlast>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
    }
}

public class ExoMinionNukeBlast : ModProjectile
{
    private const int Duration = 60; 
    private const float MaxRadius = 650f; 
    public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.DrawScreenCheckFluff[Type] = 1500;
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Summon;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.timeLeft = Duration;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1; 
    }

    private float CurrentRadius()
    {
        float t = MathHelper.Clamp(1f - Projectile.timeLeft / (float)Duration, 0f, 1f);
        return MathHelper.SmoothStep(0f, MaxRadius, t);
    }

    public override void AI()
    {
        Lighting.AddLight(Projectile.Center, 0.9f, 0.8f, 0.2f);

        if (Main.dedServ)
            return;

        float radius = CurrentRadius();
        int count = 24;
        for (int i = 0; i < count; i++)
        {
            Vector2 offset = (MathHelper.TwoPi * i / count + Main.rand.NextFloat(0.2f)).ToRotationVector2() * radius;
            Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, Main.rand.NextBool(3) ? DustID.Smoke : DustID.Terra, offset.SafeNormalize(Vector2.UnitY) * 4f, 0, default, Main.rand.NextFloat(1f, 2f));
            dust.noGravity = true;
        }
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        => CalamityMod.CalamityUtils.CircularHitboxCollision(Projectile.Center, CurrentRadius(), targetHitbox);

    public override bool PreDraw(ref Color lightColor) => false;
}
