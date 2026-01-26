using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS;
using SOTS.Items.AbandonedVillage;
using SOTS.Items.Evil;
using SOTS.Items.Pyramid;
using SOTSBardHealer.Items;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class EarthenEnchant : BaseEnchant
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
        public override Color nameColor => new Color(102, 89, 54);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<EmeraldEffect>(Item))
            {
                ModContent.GetInstance<EmeraldBracelet>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpiritGloveEffect>(Item))
            {
                ModContent.GetInstance<SpiritGlove>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SerTouEffect>(Item))
            {
                ModContent.GetInstance<SerpentsTongue>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<EarthenEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EarthenHelmet>());
            recipe.AddIngredient(ModContent.ItemType<EarthenChestplate>());
            recipe.AddIngredient(ModContent.ItemType<EarthenLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SpiritGlove>());
            recipe.AddIngredient(ModContent.ItemType<EmeraldBracelet>());
            if (ModCompatibility.SOTSBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<SerpentsTongue>());
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class EarthenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EarthenEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<EarthenHelmet>().UpdateArmorSet(player);
            }
        }
        public class SerTouEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EarthenEnchant>();
        }
        public class EmeraldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EarthenEnchant>();
        }
        public class SpiritGloveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EarthenEnchant>();
        }
    }
}
