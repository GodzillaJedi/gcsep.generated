using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Furniture.CraftingStations;
using CalamityMod.Items.Placeables.FurnitureAncient;
using CalamityMod.Items.Placeables.FurnitureBotanic;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;
using CalamityMod.Items.Placeables.FurnitureSilva;
using CalamityMod.Items.Placeables.FurnitureStatigel;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace gcsep.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeWorkbenchItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemType<ShadowspecBar>());
            Item.createTile = TileType<DemonshadeWorkbenchTile>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<DraedonsForge>());
            recipe.AddIngredient(ItemType<StaticRefiner>());
            recipe.AddIngredient(ItemType<ProfanedCrucible>());
            recipe.AddIngredient(ItemType<PlagueInfuser>());
            recipe.AddIngredient(ItemType<MonolithAmalgam>());
            recipe.AddIngredient(ItemType<EutrophicShelf>());
            recipe.AddIngredient(ItemType<SilvaBasin>());
            recipe.AddIngredient(ItemType<AncientAltar>());
            recipe.AddIngredient(ItemType<AshenAltar>());
            recipe.AddIngredient(ItemType<BotanicPlanter>());
            recipe.AddIngredient(ItemType<ShadowspecBar>(), 15);
            recipe.Register();
        }
    }
}
