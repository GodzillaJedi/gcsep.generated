using CalamityMod.Items.Accessories;
using gcsep.Core;
using SacredTools.Content.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.FargoCrossmod
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoAOther : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                //flora fist to gaunlet
                if (recipe.HasResult(ModContent.ItemType<ElementalGauntlet>()) && recipe.HasIngredient(1613))
                {
                    recipe.RemoveIngredient(1613);
                    recipe.AddIngredient<FloraFist>(1);
                }
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoAEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Shields;
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<FloraFist>())
            {
                player.GetDamage<MeleeDamageClass>() += 0.02f;
            }
        }
    }

}
