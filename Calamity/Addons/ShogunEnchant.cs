using CalamityHunt.Common.Players;
using CalamityHunt.Content.Items.Accessories;
using CalamityHunt.Content.Items.Armor.Shogun;
using CalamityHunt.Content.Items.Misc;
using CalamityHunt.Content.Items.Rarities;
using CalamityHunt.Content.Items.Weapons.Melee;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using GMR;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
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
            if (player.AddEffect<SplendorEffect>(Item))
            {
                player.GetModPlayer<SplendorJamPlayer>().active = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
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
            private void ApplyTendrilCount(Player player, int itemSlot, Item item, bool modded)
            {
                if (item.type == ModContent.ItemType<TendrilCursorAttachment>())
                {
                    int num = 8;
                    if (!modded)
                    {
                        num = Math.Clamp((itemSlot - 1) % 10, 1, 8);
                    }

                    var vanity = player.GetModPlayer<VanityPlayer>();
                    vanity.tendrilCount = num;
                    vanity.tendrilSlot = itemSlot;
                }
            }
            private void UpdateTendrilCount(On_Player.orig_UpdateVisibleAccessory orig, Player self, int itemSlot, Item item, bool modded)
            {
                ApplyTendrilCount(self, itemSlot, item, modded);
                orig(self, itemSlot, item, modded);
            } 
        }
        public class SplendorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>();
        }
    }
}
