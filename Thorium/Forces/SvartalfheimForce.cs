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
    public class SvartalfheimForce : BaseForce
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
            ModContent.GetInstance<GraniteEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<YewWoodEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<JesterEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ConduitEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TitanEnchantT>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AstroEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<GraniteEnchant>();
            recipe.AddIngredient<YewWoodEnchant>();
            recipe.AddIngredient<JesterEnchant>();
            recipe.AddIngredient<ConduitEnchant>();
            recipe.AddIngredient<TitanEnchantT>();
            recipe.AddIngredient<AstroEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
