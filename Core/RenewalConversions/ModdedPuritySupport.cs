using Fargowiltas.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Core.RenewalConversions
{
    public class ModdedPuritySupport : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == ModContent.ProjectileType<PurityNukeProj>())
            {
                ConvertEquation.Convert(projectile, "Purity", false);
            }

            else if (projectile.type == ModContent.ProjectileType<PurityNukeSupremeProj>())
            {
                ConvertEquation.Convert(projectile, "Purity", true);
            }
        }
    }
}