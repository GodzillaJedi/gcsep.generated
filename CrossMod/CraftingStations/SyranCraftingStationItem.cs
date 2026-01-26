using gcsep.Core;
using SacredTools.Content.Items.Materials;
using SacredTools.Content.Items.Placeable.CraftingStations;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace gcsep.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SyranCraftingStationItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<OblivionBar>());
            Item.createTile = TileType<SyranCraftingStationTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<NightmareFoundry>());
            recipe.AddIngredient(ItemType<TiridiumInfuser>());
            recipe.AddIngredient(ItemType<OblivionForge>());
            recipe.AddIngredient(ItemType<FlariumAnvil>());
            recipe.AddIngredient(ItemType<FlariumForge>());
            recipe.AddIngredient(ItemType<EmberOfOmen>(), 15);
            recipe.Register();
        }
    }
}
