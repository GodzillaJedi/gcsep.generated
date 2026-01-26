using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS;
using SOTS.Items.AbandonedVillage;
using SOTS.Items.Chaos;
using SOTS.Items.CritBonus;
using SOTSBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class ElementalEnchant : BaseEnchant
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
        public override Color nameColor => new(244, 25, 255);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<EyeChaosEffect>(Item))
            {
                ModContent.GetInstance<EyeOfChaos>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ChaosBadgeEffect>(Item))
            {
                ModContent.GetInstance<ChaosBadge>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<VoidmageEffect>(Item))
            {
                ModContent.GetInstance<ChaosBadge>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<HypersonicTunerEffect>(Item))
            {
                ModContent.GetInstance<HypersonicTuner>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ElementalArmorEffect>(Item))
            {
                player.setBonus = Language.GetTextValue("Mods.SOTS.ArmorSetBonus.Elemental");
                player.SOTSPlayer().ElementalBlinkBuff = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElementalHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ElementalBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<ElementalLeggings>());
            recipe.AddIngredient(ModContent.ItemType<EyeOfChaos>());
            recipe.AddIngredient(ModContent.ItemType<ChaosBadge>());
            recipe.AddIngredient(ModContent.ItemType<VoidmageIncubator>());
            if (ModCompatibility.SOTSBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<HypersonicTuner>());
            }

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class ElementalArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElementalEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ElementalHelmet>().UpdateArmorSet(player);
            }
        }
        public class HypersonicTunerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElementalEnchant>();
        }
        public class VoidmageEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElementalEnchant>();
        }
        public class ChaosBadgeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElementalEnchant>();
        }
        public class EyeChaosEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElementalEnchant>();
        }
    }
}
