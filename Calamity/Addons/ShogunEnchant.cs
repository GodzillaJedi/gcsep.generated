using CalamityHunt.Common.Players;
using CalamityHunt.Content.Items.Accessories;
using CalamityHunt.Content.Items.Armor.Shogun;
using CalamityHunt.Content.Items.Misc;
using CalamityHunt.Content.Items.Rarities;
using CalamityHunt.Content.Items.Weapons.Melee;
using CatalystMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using GMR;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Goozma.Name)]
    [JITWhenModsEnabled(ModCompatibility.Goozma.Name)]
    public class ShogunEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ModContent.RarityType<VioletRarity>();
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ShogunEffect>(Item);
            player.AddEffect<TendrilsEffect>(Item);
            player.AddEffect<SplendorEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ShogunHelm>());
            recipe.AddIngredient(ModContent.ItemType<ShogunChestplate>());
            recipe.AddIngredient(ModContent.ItemType<ShogunPants>());
            recipe.AddIngredient(ModContent.ItemType<SplendorJam>());
            recipe.AddIngredient(ModContent.ItemType<TendrilCursorAttachment>());
            recipe.AddIngredient(ModContent.ItemType<ScytheOfTheOldGod>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public class ShogunEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ShogunHelm>().UpdateArmorSet(player);
            }
        }
        public class TendrilsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TendrilCursorAttachment>().UpdateVanity(player);
            }
        }
        public class SplendorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SplendorJam>().UpdateAccessory(player, true);
            }
        }

    }
}
