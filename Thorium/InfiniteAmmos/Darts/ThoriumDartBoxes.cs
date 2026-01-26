using Fargowiltas.Items.Ammos;
using gcsep.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.Coral;
using ThoriumMod.Items.RangedItems;

namespace gcsep.Thorium.InfiniteAmmos.Darts
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CoralDartBox : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<CoralDart>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DrillDartBox : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<DrillDart>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PhaseDartBox : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<PhaseDart>();
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TetherDartBox : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<TetherDart>();
    }

    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FlareDartBox : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<FlareDart>();
    }
}