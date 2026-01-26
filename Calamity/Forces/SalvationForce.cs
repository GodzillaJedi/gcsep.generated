using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Calamity.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Calamity.Enchantments.DemonShadeEnchant;
using static gcsep.Calamity.Enchantments.GemTechEnchant;
using static gcsep.Calamity.Enchantments.LunicCorpEnchant;
using static gcsep.Calamity.Enchantments.PrismaticEnchant;

namespace gcsep.Calamity.Forces
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class SalvationForce : BaseForce
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
            ModContent.GetInstance<DemonShadeEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LunicCorpEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GemTechEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PrismaticEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DemonShadeEnchant>());
            recipe.AddIngredient(ModContent.ItemType<LunicCorpEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GemTechEnchant>());
            recipe.AddIngredient(ModContent.ItemType<PrismaticEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
