using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.SOTS.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SOTS.Forces
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name)]
    public class ShadowsForce : BaseForce
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
            ModContent.GetInstance<PatchLeatherEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<EarthenEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<VesperaEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CursedEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<VibrantEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<VoidRangerEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<PatchLeatherEnchant>();
            recipe.AddIngredient<EarthenEnchant>();
            recipe.AddIngredient<VesperaEnchant>();
            recipe.AddIngredient<CursedEnchant>();
            recipe.AddIngredient<VibrantEnchant>();
            recipe.AddIngredient<VoidRangerEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
