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
    public class ForceOfShamanist : BaseForce
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
            ModContent.GetInstance<HorizonEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DawnlightEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MeteorEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TycheEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<HorizonEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DawnlightEnchant>());
            recipe.AddIngredient(ModContent.ItemType<MeteorEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TycheEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
