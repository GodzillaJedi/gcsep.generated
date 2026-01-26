using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS.Items.CritBonus;
using SOTS.Items.Earth;
using SOTS.Items.Fragments;
using SOTS.Items.Planetarium.FromChests;
using SOTSBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class VibrantEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 40000;
        }
        public override Color nameColor => new(255, 128, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<WorldlyEffect>(Item))
            {
                ModContent.GetInstance<WorldlyPolarizer>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SoulCharmEffect>(Item))
            {
                ModContent.GetInstance<SoulCharm>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SkywareEffect>(Item))
            {
                ModContent.GetInstance<SkywareBattery>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SubsonicEffect>(Item))
            {
                ModContent.GetInstance<SubsonicTuner>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<VibrantHelmEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VibrantHelmet>());
            recipe.AddIngredient(ModContent.ItemType<VibrantChestplate>());
            recipe.AddIngredient(ModContent.ItemType<VibrantLeggings>());
            recipe.AddIngredient(ModContent.ItemType<WorldlyPolarizer>());
            recipe.AddIngredient(ModContent.ItemType<SoulCharm>());
            recipe.AddIngredient(ModContent.ItemType<SkywareBattery>());
            if (ModCompatibility.SOTSBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<SubsonicTuner>());
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class VibrantHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VibrantEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VibrantHelmet>().UpdateArmorSet(player);
            }
        }
        public class WorldlyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VibrantEnchant>();
        }
        public class SoulCharmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VibrantEnchant>();
        }
        public class SkywareEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VibrantEnchant>();
        }
        public class SubsonicEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VibrantEnchant>();
        }
    }
}
