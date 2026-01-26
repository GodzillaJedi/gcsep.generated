using gcsep.Core;
using Terraria.ModLoader;

namespace gcsep.FargoCrossmod
{
    [ExtendsFromMod(ModCompatibility.FargoCrossmod.Name, ModCompatibility.SacredTools.Name)]
    public class DlcSoAOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            //souls/mutagen overhaul

            //for (int i = 0; i < Recipe.numRecipes; i++)
            //{
            //    Recipe recipe = Main.recipe[i];

            //    if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()) && !recipe.HasIngredient(ModContent.ItemType<BindsOfVeracity>()))
            //    {
            //        recipe.AddIngredient<BindsOfVeracity>(1);
            //        recipe.AddIngredient<LanceSheathTalisman>(1);
            //    }
            //}
        }
    }
}
