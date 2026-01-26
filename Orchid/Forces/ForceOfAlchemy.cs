using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Orchid.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Forces
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class ForceOfAlchemy : BaseForce
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
            ModContent.GetInstance<GitGudEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BambooEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<OutlawEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PyreEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PhosphorescentEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<GitGudEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BambooEnchant>());
            recipe.AddIngredient(ModContent.ItemType<OutlawEnchant>());
            recipe.AddIngredient(ModContent.ItemType<PyreEnchant>());
            recipe.AddIngredient(ModContent.ItemType<PhosphorescentEnchant>());


            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
