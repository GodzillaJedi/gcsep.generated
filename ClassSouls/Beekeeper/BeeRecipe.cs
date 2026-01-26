using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using gcsep.ClassSouls.Beekeeper.Souls;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.ClassSouls.Beekeeper
{
    [ExtendsFromMod(ModCompatibility.BeekeeperClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
    public class BeeRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<UniverseSoul>())
                {
                    recipe.AddIngredient<BeekeeperSoul>();
                }
            }
        }
    }
}
