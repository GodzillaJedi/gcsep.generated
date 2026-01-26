using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Thorium.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Thorium.Enchantments.CoralEnchant;
using static gcsep.Thorium.Enchantments.CryomancerEnchant;
using static gcsep.Thorium.Enchantments.DepthDiverEnchant;
using static gcsep.Thorium.Enchantments.IcyEnchant;
using static gcsep.Thorium.Enchantments.NagaSkinEnchant;
using static gcsep.Thorium.Enchantments.TideHunterEnchant;
using static gcsep.Thorium.Enchantments.TideTurnerEnchant;
using static gcsep.Thorium.Enchantments.WhisperingEnchant;

namespace gcsep.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class JotunheimForce : BaseForce
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
            ModContent.GetInstance<WhisperingEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DepthDiverEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TideHunterEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<NagaSkinEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CryomancerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TideTurnerEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WhisperingEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DepthDiverEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterEnchant>());
            recipe.AddIngredient(ModContent.ItemType<NagaSkinEnchant>());
            recipe.AddIngredient(ModContent.ItemType<CryomancerEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TideTurnerEnchant>());

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
