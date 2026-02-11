using CalamityMod.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Reworks
{
    public class VoidEaterMarionetteGlobal : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ProjectileType<VoidEaterMarionetteProjectile>();
        }

        public override void SetDefaults(Projectile projectile)
        {
            projectile.minionSlots = 1f; // ⭐ REQUIRED
        }
    }
}
