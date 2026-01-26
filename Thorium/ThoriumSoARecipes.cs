using gcsep.Core;
using SacredTools.Content.Items.Materials;
using gcsep.Thorium.Souls;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.SacredTools.Name)]
    public class TorSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ThoriumSoul>() && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }
            }
        }
    }
}
