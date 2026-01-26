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
    public class MuspelheimForce : BaseForce
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
            ModContent.GetInstance<CyberPunkEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DemonBloodEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SandstoneEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<NobleEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PyromancerEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<CyberPunkEnchant>();
            recipe.AddIngredient<DemonBloodEnchant>();
            recipe.AddIngredient<SandstoneEnchant>();
            recipe.AddIngredient<NobleEnchant>();
            recipe.AddIngredient<PyromancerEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
