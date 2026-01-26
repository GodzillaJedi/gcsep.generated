using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Calamity.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Calamity.Enchantments.AuricTeslaEnchant;
using static gcsep.Calamity.Enchantments.BloodflareEnchant;
using static gcsep.Calamity.Enchantments.GodSlayerEnchant;
using static gcsep.Calamity.Enchantments.SilvaEnchant;
using static gcsep.Calamity.Enchantments.TarragonEnchant;

namespace gcsep.Calamity.Forces
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class ExaltationForce : BaseForce
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
            ModContent.GetInstance<TarragonEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BloodflareEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GodSlayerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SilvaEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AuricTeslaEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TarragonEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerEnchant>());
            recipe.AddIngredient(ModContent.ItemType<SilvaEnchant>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
