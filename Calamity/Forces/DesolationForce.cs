using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Calamity.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Calamity.Enchantments.AstralEnchant;
using static gcsep.Calamity.Enchantments.FathomSwarmerEnchant;
using static gcsep.Calamity.Enchantments.MolluskEnchant;
using static gcsep.Calamity.Enchantments.OmegaBlueEnchant;
using static gcsep.Calamity.Enchantments.UmbraphileEnchant;

namespace gcsep.Calamity.Forces
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DesolationForce : BaseForce
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
            ModContent.GetInstance<MolluskEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<OmegaBlueEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FathomSwarmerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<UmbraphileEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AstralEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MolluskEnchant>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueEnchant>());
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerEnchant>());
            recipe.AddIngredient(ModContent.ItemType<UmbraphileEnchant>());
            recipe.AddIngredient(ModContent.ItemType<AstralEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
