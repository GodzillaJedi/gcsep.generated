using CalamityMod.Projectiles.Melee;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class CalProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void AI(Projectile projectile)
        {
            if (projectile.type == ModContent.ProjectileType<OrderbringerWaveProj>())
            {
                projectile.velocity *= 1.1f;
            }
        }
    }
}
