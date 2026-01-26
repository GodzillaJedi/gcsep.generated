using CalamityMod.Items.Materials;
using gcsep.Core;
using gcsep.Thorium.Souls;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.FargoCrossmod.Name)]
    public class TorDlcRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ThoriumSoul>() && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                }
            }
        }
    }
}


