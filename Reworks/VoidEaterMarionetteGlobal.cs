using CalamityMod.Projectiles.Summon;
using System;
using Terraria;
using Terraria.ModLoader;

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

                
                int dynamicSegments = 5 + player.maxMinions;

                
                int finalSegmentCount = Math.Clamp(dynamicSegments, 5, 15);

                
                projectile.minionSlots = finalSegmentCount;
            }
        }
    }
}
