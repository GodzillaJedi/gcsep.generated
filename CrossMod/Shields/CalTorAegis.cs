using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using gcsep.Thorium.Souls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Terrarium;

namespace gcsep.Crossmod.Shields
{
    /*
        * Progression look like this:
        * ankh shield
        * asgard's valor
        * terrarium defender
        * elysian aegis
        * asgardian aegis
    */

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class AegisShieldRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Shields;
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // valor to defender
                if (recipe.HasResult(ModContent.ItemType<TerrariumDefender>()) && recipe.HasIngredient(1613))
                {
                    recipe.RemoveIngredient(1613);
                    recipe.AddIngredient<CorruptedWarShield>(1);
                    recipe.AddIngredient<AsgardsValor>(1);
                }
                if (recipe.HasResult(ModContent.ItemType<TerrariumDefender>()) && recipe.HasIngredient(ItemID.FrozenShield) && ModCompatibility.SacredTools.Loaded)
                {
                    recipe.RemoveIngredient(ItemID.FrozenShield);
                    recipe.RemoveIngredient(ModContent.ItemType<HolyAegis>());
                }
                // defender to aegis
                if (recipe.HasResult(ModContent.ItemType<AsgardianAegis>()) && recipe.HasIngredient<AsgardsValor>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AsgardsValor>());
                    recipe.AddIngredient<TerrariumDefender>(1);
                }
                //aegis to colossus (if no cal dlc)
                if (recipe.HasResult(ModContent.ItemType<ColossusSoul>()) && recipe.HasIngredient(1613))
                {
                    recipe.RemoveIngredient(1613);
                    recipe.AddIngredient<AsgardianAegis>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class AegisShieldEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Shields;
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<TerrariumDefender>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "AsgardsValor").UpdateAccessory(player, false);
            }
            if (Item.type == ModContent.ItemType<AsgardianAegis>()
                || Item.type == ModContent.ItemType<ColossusSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "TerrariumDefender").UpdateAccessory(player, false);
            }
            if (Item.type == ModContent.ItemType<ColossusSoul>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "AsgardianAegis").UpdateAccessory(player, false);
            }
        }
    }
}

