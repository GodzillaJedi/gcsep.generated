using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Calamity.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Forces
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class ExplorationForceEx : BaseForce
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 600000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<WulfrumEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AerospecEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DesertProwlerEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MarniteEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<VictideEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SulphurousEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<StatigelEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SnowRuffianEnchantEx>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WulfrumEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<StatigelEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<VictideEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<SulphurousEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<AerospecEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<MarniteEnchantEx>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}