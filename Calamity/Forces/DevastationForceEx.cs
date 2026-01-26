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
    public class DevastationForceEx : BaseForce
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
            ModContent.GetInstance<HydrothermicEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TitanHeartEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DaedalusEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ReaverEnchantEx>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TitanHeartEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusEnchantEx>());
            recipe.AddIngredient(ModContent.ItemType<ReaverEnchantEx>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
