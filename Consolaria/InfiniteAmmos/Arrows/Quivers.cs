using Consolaria.Content.Items.Weapons.Ammo;
using Fargowiltas.Items.Ammos;
using gcsep.Core;
using Terraria.ModLoader;

namespace gcsep.Consolaria.InfiniteAmmos.Arrows
{
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    public class HeartQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<HeartArrow>();
    }

    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    public class SpectralQuiver : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SpectralArrow>();
    }
}