using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Vitality.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Vitality.Forces
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class ForceOfChaos : BaseForce
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
            ModContent.GetInstance<AnarchyEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ChaosEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShadewitchEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PurifiedEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AnarchyEnchant>());
            recipe.AddIngredient(ModContent.ItemType<ChaosEnchant>());
            recipe.AddIngredient(ModContent.ItemType<ShadewitchEnchant>());
            recipe.AddIngredient(ModContent.ItemType<PurifiedEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
