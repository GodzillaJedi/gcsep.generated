using gcsep.Core;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Placeable.Furniture.Lab;
using Redemption.Items.Placeable.Furniture.Misc;
using Redemption.Items.Placeable.Furniture.SlayerShip;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace gcsep.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionCraftingStationItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<XeniumAlloy>());
            Item.createTile = TileType<RedemptionCraftingStationTile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<XeniumSmelter>());
            recipe.AddIngredient(ItemType<XeniumRefinery>());
            recipe.AddIngredient(ItemType<SlayerFabricator>());
            recipe.AddIngredient(ItemType<GathicCryoFurnace>());
            recipe.AddIngredient(ItemType<GirusCorruptor>());
            recipe.AddIngredient(ItemType<EnergyStation>());
            recipe.AddIngredient(ItemType<LifeFragment>(), 15);
            recipe.Register();
        }
    }
}
