using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Thorium.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AsgardForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }


        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<WhiteKnightEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SacredEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FallenPaladinEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CelestiaEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<RhapsodistEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WhiteKnightEnchant>());
            recipe.AddIngredient(ModContent.ItemType<SacredEnchant>());
            recipe.AddIngredient(ModContent.ItemType<CelestiaEnchant>());
            recipe.AddIngredient(ModContent.ItemType<FallenPaladinEnchant>());
            recipe.AddIngredient(ModContent.ItemType<RhapsodistEnchant>());

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
