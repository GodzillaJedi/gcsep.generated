using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Polarities.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Forces
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class WildernessForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Polarities;
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
            ModContent.GetInstance<ConvectiveEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<HaliteEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AerogelEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<StormcloudEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SunplateEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<ConvectiveEnchant>();
            recipe.AddIngredient<HaliteEnchant>();
            recipe.AddIngredient<SnakescaleEnchant>();
            recipe.AddIngredient<StormcloudEnchant>();
            recipe.AddIngredient<SunplateEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
