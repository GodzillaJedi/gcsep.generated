using gcsep.Core;
using Terraria.ModLoader;

namespace gcsep.Crossmod.Difficulties
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    internal class DifficultiesRegister : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.ExperimentalContent;
        }
        public override void PostSetupContent()
        {
            Mod cal = ModCompatibility.Calamity.Mod;
            cal.Call("AddDifficultyToUI", new TrueEternityRevDifficulty());
            cal.Call("AddDifficultyToUI", new TrueEternityDeathDifficulty());
        }
    }
}
