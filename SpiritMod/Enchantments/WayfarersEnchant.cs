using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Armor.Daybloom;
using SpiritMod.Items.Armor.WayfarerSet;
using SpiritMod.Items.Sets.ToolsMisc.BrilliantHarvester;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.StreamSurferEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class WayfarersEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(150, 105, 97);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<WayfarerTreads>(Item))
            {
                ModContent.GetInstance<ExplorerTreads>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<WayfarerBand>(Item))
            {
                ModContent.GetInstance<MetalBand>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<WayfarerArmorBuff>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<WayfarerHead>();
            recipe.AddIngredient<WayfarerBody>();
            recipe.AddIngredient<WayfarerLegs>();
            recipe.AddIngredient<GemPickaxe>();
            recipe.AddIngredient<MetalBand>();
            recipe.AddIngredient<ExplorerTreads>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class WayfarerTreads : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WayfarersEnchant>();
        }
        public class WayfarerBand : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WayfarersEnchant>();
        }
        public class WayfarerArmorBuff : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WayfarersEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<WayfarerHead>().UpdateArmorSet(player);
            }
        }
    }
}
