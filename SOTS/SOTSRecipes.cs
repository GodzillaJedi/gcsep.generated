using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Content.Items.Accessories;
using gcsep.Core;
using gcsep.SOTS.Forces;
using gcsep.SOTS.Souls;
using SOTS.Items.Fragments;
using SOTS.Items.Wings;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.SOTS
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name)]
    public class SOTSRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SOTS;
        }
        public override void PostAddRecipes()
        {
            
            for (int i = 0; i < Main.recipe.Length; i++)
            {
                Recipe recipe = Main.recipe[i];

                // Souls → add FragmentOfChaos
                if (!ModCompatibility.Calamity.Loaded &&
                    !ModCompatibility.SacredTools.Loaded &&
                    !recipe.HasIngredient<FragmentOfChaos>())
                {
                    if (recipe.HasResult<UniverseSoul>() ||
                        recipe.HasResult<TerrariaSoul>() ||
                        recipe.HasResult<MasochistSoul>() ||
                        recipe.HasResult<DimensionSoul>())
                    {
                        recipe.AddIngredient<FragmentOfChaos>(5);
                    }
                }

                // BaseForce items → add FragmentOfChaos
                if (recipe.createItem.ModItem is BaseForce &&
                    !recipe.HasIngredient<FragmentOfChaos>())
                {
                    recipe.AddIngredient<FragmentOfChaos>(4);
                }

                // FlightMasterySoul → GildedBladeWings
                if (recipe.HasResult<FlightMasterySoul>() &&
                    !recipe.HasIngredient<GildedBladeWings>())
                {
                    recipe.AddIngredient<GildedBladeWings>();
                }

                // ColossusSoul → VoidShield
                if (recipe.HasResult<ColossusSoul>() &&
                    !recipe.HasIngredient<VoidShield>())
                {
                    recipe.AddIngredient<VoidShield>();
                }

                // SoulOfTmod → SecretsForce
                if (recipe.HasResult<MicroverseSoul>() &&
                    !recipe.HasIngredient<SecretsForce>())
                {
                    recipe.AddIngredient<SecretsForce>();
                }

                // SoulOfTmod → ShadowsForce
                if (recipe.HasResult<MicroverseSoul>() &&
                    !recipe.HasIngredient<ShadowsForce>())
                {
                    recipe.AddIngredient<ShadowsForce>();
                }
            }
        }
    }
}
