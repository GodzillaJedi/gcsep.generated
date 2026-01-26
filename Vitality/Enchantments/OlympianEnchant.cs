using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Olympian;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class OlympianEnchant : BaseEnchant
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
        public override Color nameColor => Color.Gold;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OlympianVitalityEffect>(Item);
            if (player.AddEffect<SolarRingsEffect>(Item))
            {
                ModContent.GetInstance<SolarbrandRing>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<LOTREffects>(Item))
            {
                ModContent.GetInstance<RingoftheLords>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OlympianHelmet>());
            recipe.AddIngredient(ModContent.ItemType<OlympianBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<OlympianLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SolarbrandRing>());
            recipe.AddIngredient(ModContent.ItemType<RingoftheLords>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class OlympianVitalityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OlympianEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OlympianHelmet>().UpdateArmorSet(player);
            }
        }
        public class SolarRingsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OlympianEnchant>();
        }
        public class LOTREffects : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OlympianEnchant>();
        }
    }
}
