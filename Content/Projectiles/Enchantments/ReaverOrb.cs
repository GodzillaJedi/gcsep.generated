using CalamityMod;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles.Enchantments
{
    public class ReaverOrb : ModProjectile, ILocalizedModType, IModType
    {
        public int dust = 3;

        public new string LocalizationCategory => "Projectiles.Typeless";

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 50;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 18000 * 5;
            Projectile.alpha = 50;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.minion = true; 
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var cal = player.Calamity();
            var cse = player.GetModPlayer<GCSEPlayer>(); 

            if ((!cal.reaverExplore && !cse.rOrb) || player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Projectile.timeLeft = 2;

            dust--;
            if (dust >= 0)
            {
                for (int i = 0; i < 50; i++)
                {
                    int d = Dust.NewDust(
                        new Vector2(Projectile.position.X, Projectile.position.Y + 16f),
                        Projectile.width,
                        Projectile.height - 16,
                        157
                    );
                    Main.dust[d].velocity *= 2f;
                    Main.dust[d].scale *= 1.15f;
                }
            }

            Lighting.AddLight(Projectile.Center, 0.5f, 2f, 0.5f);

            Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY - 60f);

            if (player.gravDir == -1f)
            {
                Projectile.position.Y += 120f;
                Projectile.rotation = MathF.PI;
            }
            else
            {
                Projectile.rotation = 0f;
            }

            Projectile.position.X = (int)Projectile.position.X;
            Projectile.position.Y = (int)Projectile.position.Y;
        }
        public override bool? CanDamage() => false;
    }
}
