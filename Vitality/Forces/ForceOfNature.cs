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
    public class ForceOfNature : BaseForce
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
            ModContent.GetInstance<AntlionEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GlacialEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShiverEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MoonmothEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GlowshroomEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LivingWoodEnchantV>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MosquitoEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TempleEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AntlionEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GlacialEnchant>());
            recipe.AddIngredient(ModContent.ItemType<ShiverEnchant>());
            recipe.AddIngredient(ModContent.ItemType<MoonmothEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GlowshroomEnchant>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodEnchantV>());
            recipe.AddIngredient(ModContent.ItemType<MosquitoEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TempleEnchant>());

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());

            recipe.Register();
        }
    }
}
