using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Chaos;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class ChaosEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkViolet;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ChaosVitalityEffect>(Item);
            if (player.AddEffect<ShadowStoneEffect>(Item))
            {
                ModContent.GetInstance<ShadowStone>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<MoonOrbEffect>(Item))
            {
                ModContent.GetInstance<MoonbindersOrb>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ChaosHood>());
            recipe.AddIngredient(ModContent.ItemType<ChaosChainmail>());
            recipe.AddIngredient(ModContent.ItemType<ChaosGreaves>());
            recipe.AddIngredient(ModContent.ItemType<MoonbindersOrb>());
            recipe.AddIngredient(ModContent.ItemType<ShadowStone>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class ChaosVitalityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ChaosEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ChaosHood>().UpdateArmorSet(player);
            }
        }
        public class ShadowStoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ChaosEnchant>();
        }
        public class MoonOrbEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ChaosEnchant>();
        }
    }
}
