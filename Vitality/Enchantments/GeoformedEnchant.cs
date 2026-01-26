using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.GemstoneElemental;
using VitalityMod.Items.Geoformed;
using VitalityMod.Items.GlowingMoss;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class GeoformedEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => Color.Indigo;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GeoformedEffect>(Item);
            if (player.AddEffect<GemRingEffect>(Item))
            {
                ModContent.GetInstance<GemstoneRing>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<HeliumRingsEffect>(Item))
            {
                ModContent.GetInstance<HeliumRing>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GeoformedHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GeoformedBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<GeoformedLeggings>());
            recipe.AddIngredient(ModContent.ItemType<GemstoneRing>());
            recipe.AddIngredient(ModContent.ItemType<HeliumRing>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class GeoformedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeoformedEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GeoformedHelmet>().UpdateArmorSet(player);
            }
        }
        public class GemRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeoformedEnchant>();
        }
        public class HeliumRingsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeoformedEnchant>();
        }
    }
}
