using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Vitality.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Vitality.Forces
{
    public class ForceOfOre : BaseForce
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
            ModContent.GetInstance<ArcaneGoldEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GeoformedEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MetalsmithEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GeraniumEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PaladinEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SkyforgedEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ArcaneGoldEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GeoformedEnchant>());
            recipe.AddIngredient(ModContent.ItemType<MetalsmithEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GeraniumEnchant>());
            recipe.AddIngredient(ModContent.ItemType<PaladinEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
