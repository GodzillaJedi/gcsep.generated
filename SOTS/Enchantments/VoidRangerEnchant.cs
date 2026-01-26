using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS;
using SOTS.Items;
using SOTS.Items.Celestial;
using SOTS.Items.Earth;
using SOTSBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class VoidRangerEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(88, 126, 121);
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<VoidspaceEmblemEffect>(Item))
            {
                ModContent.GetInstance<VoidspaceEmblem>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SubspaceEmblemEffect>(Item))
            {
                ModContent.GetInstance<SubspaceLocket>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SerpentSpineEffect>(Item))
            {
                ModContent.GetInstance<SerpentSpine>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AlchemistsEffect>(Item))
            {
                ModContent.GetInstance<AlchemistsCharm>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FoggyEffect>(Item))
            {
                ModContent.GetInstance<FoggyClairvoyance>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<InfrasonicEffect>(Item))
            {
                ModContent.GetInstance<InfrasonicTuner>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<VoidspaceEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoidspaceMask>());
            recipe.AddIngredient(ModContent.ItemType<VoidspaceBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<VoidspaceLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SubspaceLocket>());
            recipe.AddIngredient(ModContent.ItemType<VoidspaceEmblem>());
            recipe.AddIngredient(ModContent.ItemType<SerpentSpine>());
            recipe.AddIngredient(ModContent.ItemType<AlchemistsCharm>());
            recipe.AddIngredient(ModContent.ItemType<FoggyClairvoyance>());
            if (ModCompatibility.SOTSBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<InfrasonicTuner>());
            }

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class VoidspaceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidRangerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.setBonus = Language.GetTextValue("Mods.SOTS.ArmorSetBonus.Voidspace");
                player.SOTSPlayer().VoidspaceFlames = true;
            }
        }
        public class VoidspaceEmblemEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidRangerEnchant>();
        }
        public class SubspaceEmblemEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidRangerEnchant>();
        }
        public class SerpentSpineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidRangerEnchant>();
        }
        public class FoggyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidRangerEnchant>();
        }
        public class AlchemistsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidRangerEnchant>();
        }
        public class InfrasonicEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidRangerEnchant>();
        }
    }
}
