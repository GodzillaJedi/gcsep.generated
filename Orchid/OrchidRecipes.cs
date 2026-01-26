using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using OrchidMod.Content.Guardian.Misc;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Orchid
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class OrchidRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.OrchidMod;
        }

        public override void PostAddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                // ---------------------------------------------------------
                // 1. Add HorizonFragment to major Souls when BOTH Calamity AND SacredTools are missing
                // ---------------------------------------------------------
                bool noCalamity = !ModCompatibility.Calamity.Loaded;
                bool noSacredTools = !ModCompatibility.SacredTools.Loaded;

                if (noCalamity && noSacredTools)
                {
                    if ((recipe.HasResult<UniverseSoul>() ||
                         recipe.HasResult<TerrariaSoul>() ||
                         recipe.HasResult<MasochistSoul>() ||
                         recipe.HasResult<DimensionSoul>()) &&
                        !recipe.HasIngredient<HorizonFragment>())
                    {
                        recipe.AddIngredient<HorizonFragment>(5);
                    }
                }

                // ---------------------------------------------------------
                // 2. Add HorizonFragment to ALL Forces (no mod gating)
                // ---------------------------------------------------------
                if (recipe.createItem.ModItem is BaseForce &&
                    !recipe.HasIngredient<HorizonFragment>())
                {
                    recipe.AddIngredient<HorizonFragment>(4);
                }
            }
        }
    }
}