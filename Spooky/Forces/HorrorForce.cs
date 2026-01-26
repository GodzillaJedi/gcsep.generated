using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Spooky.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Forces
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class TerrorForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Spooky;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<FlowerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GildedWizardEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<RootEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SpiOpsEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SpiritHorsemenEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<FlowerEnchant>();
            recipe.AddIngredient<GildedWizardEnchant>();
            recipe.AddIngredient<RootEnchant>();
            recipe.AddIngredient<SpiOpsEnchant>();
            recipe.AddIngredient<SpiritHorsemenEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
