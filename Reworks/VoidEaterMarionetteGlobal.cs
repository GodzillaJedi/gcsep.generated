using CalamityMod;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using System;
using Terraria;
using Terraria.ModLoader;
using static gcsep.Calamity.Enchantments.GodSlayerEnchant;

namespace gcsep.Reworks
{
    public class VoidEaterMarionetteGlobal : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, Terraria.DataStructures.IEntitySource source)
        {
            if (ModLoader.HasMod("CalamityMod") &&
                projectile.type == ModContent.ProjectileType<VoidEaterMarionetteProjectile>())
            {
                projectile.minionSlots = 5f;
            }
        }

        public override void PostAI(Projectile projectile)
        {
            if (ModLoader.HasMod("CalamityMod") &&
                projectile.type == ModContent.ProjectileType<VoidEaterMarionetteProjectile>())
            {
                Player player = Main.player[projectile.owner];

                if (player.HasEffect<MechwormEffect>() || player.Calamity().hasVoidEaterMarionette)
                {
                    int dynamicSegments = 5 + player.maxMinions;

                    int finalSegmentCount = Math.Clamp(dynamicSegments, 5, 15);

                    projectile.minionSlots = finalSegmentCount;
                }
            }
        }
    }
}
