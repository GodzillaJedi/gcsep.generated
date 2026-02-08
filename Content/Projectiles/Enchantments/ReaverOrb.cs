using CalamityMod;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            base.Projectile.width = 48;
            base.Projectile.height = 50;
            base.Projectile.netImportant = true;
            base.Projectile.friendly = true;
            base.Projectile.ignoreWater = true;
            base.Projectile.timeLeft = 18000;
            base.Projectile.alpha = 50;
            base.Projectile.penetrate = -1;
            base.Projectile.tileCollide = false;
            base.Projectile.timeLeft *= 5;
        }
        public override void AI()
        {
            bool flag = base.Projectile.type == ModContent.ProjectileType<ReaverOrb>();
            Player player = Main.player[base.Projectile.owner];
            CalamityPlayer calamityPlayer = player.Calamity();
            GCSEPlayer gCSEPlayer = player.CSE();
            if (!calamityPlayer.reaverExplore)
            {
                base.Projectile.active = false;
                return;
            }
            if (flag)
            {
                if (player.dead)
                {
                    player.CSE().rOrb = false;
                }
                if (player.CSE().rOrb)
                {
                    base.Projectile.timeLeft = 2;
                }
            }
            dust--;
            if (dust >= 0)
            {
                int num = 50;
                for (int i = 0; i < num; i++)
                {
                    int num2 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 157);
                    Dust obj = Main.dust[num2];
                    obj.velocity *= 2f;
                    Main.dust[num2].scale *= 1.15f;
                }
            }
            Lighting.AddLight(base.Projectile.Center, 0.5f, 2f, 0.5f);
            base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY - 60f);
            if (player.gravDir == -1f)
            {
                base.Projectile.position.Y += 120f;
                base.Projectile.rotation = (float)Math.PI;
            }
            else
            {
                base.Projectile.rotation = 0f;
            }
            base.Projectile.position.X = (int)base.Projectile.position.X;
            base.Projectile.position.Y = (int)base.Projectile.position.Y;
        }
        public override bool? CanDamage()
        {
            return false;
        }
    }
}
