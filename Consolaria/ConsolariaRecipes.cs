using gcsep.Core;
using Terraria.ModLoader;

namespace gcsep.Consolaria
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class ConsolariaRecipes : ModSystem
    {
        //public override void PostAddRecipes()
        //{
        //    for (int i = 0; i < Recipe.numRecipes; i++)
        //    {
        //        Recipe recipe = Main.recipe[i];

        //        if (recipe.HasResult<TerrariaSoul>() && !recipe.HasIngredient<HeroForce>())
        //        {
        //            recipe.AddIngredient<HeroForce>(1);
        //        }
        //    }
        //}
    }
}
