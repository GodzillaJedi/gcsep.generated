using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Thorium.Enchantments;
using gcsep.Vitality.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AlfheimForce : BaseForce
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
            ModContent.GetInstance<BulbEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AssassinEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LifeBloomEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LifeBinderEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BiotechEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BulbEnchant>());
            recipe.AddIngredient(ModContent.ItemType<AssassinEnchant>());
            recipe.AddIngredient(ModContent.ItemType<LifeBloomEnchant>());
            recipe.AddIngredient(ModContent.ItemType<LifeBinderEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BiotechEnchant>());

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
