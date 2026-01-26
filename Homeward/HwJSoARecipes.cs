using ContinentOfJourney.Items.Accessories;
using gcsep.Core;
using SacredTools.Content.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Homeward
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name, ModCompatibility.Homeward.Name)]
    public class HwjSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<VoidSpurs>() && !recipe.HasIngredient<Horizon>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<RoyalRunners>());
                    recipe.AddIngredient<Horizon>();
                }
            }
        }
    }
}
