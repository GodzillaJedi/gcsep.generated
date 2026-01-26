using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.GemstoneElemental;
using VitalityMod.Items.Geoformed;
using VitalityMod.Items.Geranium;
using VitalityMod.Items.GlowingMoss;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class GeraniumEnchant : BaseEnchant
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
        public override Color nameColor => Color.HotPink;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GeraniumEffect>(Item);
            if (player.AddEffect<SoLEffect>(Item))
            {
                ModContent.GetInstance<ShieldofLife>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ElixerEffect>(Item))
            {
                ModContent.GetInstance<DivineElixir>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GeraniumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GeraniumBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<GeraniumLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ShieldofLife>());
            recipe.AddIngredient(ModContent.ItemType<DivineElixir>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class GeraniumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeraniumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GeraniumHelmet>().UpdateArmorSet(player);
            }
        }
        public class SoLEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeraniumEnchant>();
        }
        public class ElixerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeraniumEnchant>();
        }
    }
}
