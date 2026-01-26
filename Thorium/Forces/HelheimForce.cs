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
    public class HelheimForce : BaseForce
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
            ModContent.GetInstance<WarlockEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SilkEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DreadEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SpiritTrapperEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShadeMasterEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DreamWeaverEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WarlockEnchant>());
            recipe.AddIngredient(ModContent.ItemType<SilkEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DreadEnchant>());
            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperEnchant>());
            recipe.AddIngredient(ModContent.ItemType<ShadeMasterEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DreamWeaverEnchant>());

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
