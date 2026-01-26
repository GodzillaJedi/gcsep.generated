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
    public class VanaheimForce : BaseForce
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
            ModContent.GetInstance<BronzeEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DragonEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LichEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<WhiteDwarfEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FungusEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FlightEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BronzeEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DragonEnchant>());
            recipe.AddIngredient(ModContent.ItemType<LichEnchant>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfEnchant>());
            recipe.AddIngredient(ModContent.ItemType<FlightEnchant>());
            recipe.AddIngredient(ModContent.ItemType<FungusEnchant>());

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
