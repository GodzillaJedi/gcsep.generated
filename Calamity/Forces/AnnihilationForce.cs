using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Calamity.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Calamity.Enchantments.BrimflameEnchant;
using static gcsep.Calamity.Enchantments.EmpyreanEnchant;
using static gcsep.Calamity.Enchantments.FearfallenEnchant;
using static gcsep.Calamity.Enchantments.PlaguebringerEnchant;
using static gcsep.Calamity.Enchantments.PlagueReaperEnchant;

namespace gcsep.Calamity.Forces
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AnnihilationForce : BaseForce
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
            ModContent.GetInstance<EmpyreanEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PlagueReaperEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PlaguebringerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FearfallenEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BrimflameEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<EmpyreanEnchant>());
            recipe.AddIngredient(ModContent.ItemType<PlagueReaperEnchant>());
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerEnchant>());
            recipe.AddIngredient(ModContent.ItemType<FearfallenEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
