using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Materials;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Material;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Homeward
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Homeward.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Homeward.Name, ModCompatibility.FargoCrossmod.Name)]
    public class HwjCalRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<ShadowspecBar>() && !recipe.HasIngredient<EssenceofBright>())
                {
                    recipe.AddIngredient<EssenceofBright>();
                }
                if (recipe.HasResult<VagabondsSoul>() && !recipe.HasIngredient<FinalBar>())
                {
                    recipe.AddIngredient<FinalBar>(5);
                }
                if (recipe.HasResult<VoidStriders>() && !recipe.HasIngredient<Horizon>() && !ModCompatibility.SacredTools.Loaded)
                {
                    recipe.RemoveIngredient(ModContent.ItemType<MoonWalkers>());
                    recipe.AddIngredient<Horizon>();
                }
            }
        }
    }
}
