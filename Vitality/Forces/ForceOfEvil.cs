using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Vitality.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Vitality.Forces
{
    public class ForceOfEvil : BaseForce
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
            ModContent.GetInstance<BloodkeeperEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DarkbloodEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<EbonwitchEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MartianGunnerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<OccultEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TrailHunterEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BloodkeeperEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DarkbloodEnchant>());
            recipe.AddIngredient(ModContent.ItemType<EbonwitchEnchant>());
            recipe.AddIngredient(ModContent.ItemType<MartianGunnerEnchant>());
            recipe.AddIngredient(ModContent.ItemType<OccultEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TrailHunterEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
