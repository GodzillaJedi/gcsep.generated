using CalamityMod.Items.Accessories;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using gcsep.Core;
using gcsep.CrossMod.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.FargoCrossmod
{
    [ExtendsFromMod(ModCompatibility.FargoCrossmod.Name, ModCompatibility.Thorium.Name)]
    public class DlcTorOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()) && recipe.HasIngredient(ModContent.ItemType<Nanotech>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<Nanotech>());
                    recipe.AddIngredient<GtTETFinal>(1);
                }
            }
        }
    }
}
