using ContinentOfJourney.Items.Material;
using gcsep.Core;
using gcsep.Thorium.Souls;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.Terrarium;

namespace gcsep.Homeward
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Homeward.Name)]
    public class HwjThoriumRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<FinalBar>() && !recipe.HasIngredient<TerrariumCore>())
                {
                    recipe.DisableRecipe();
                }

                if ((recipe.HasResult<BardSoul>() ||
                    recipe.HasResult<GuardianAngelsSoul>()
                    ) && !recipe.HasIngredient<FinalBar>())
                {
                    recipe.AddIngredient<FinalBar>(5);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<FinalBar>())
                .AddIngredient<TerrariumCore>()
                .AddIngredient<FinalOre>(7)
                .AddIngredient<EternalBar>()
                .AddIngredient<LivingBar>()
                .AddIngredient<CubistBar>()
                .Register();
        }
    }
}
