using CalamityHunt.Common.Players;
using CalamityHunt.Content.Buffs;
using CalamityHunt.Content.Items.Accessories;
using CalamityHunt.Content.Items.Armor.Shogun;
using CalamityHunt.Content.Items.Misc;
using CalamityHunt.Content.Items.Rarities;
using CalamityHunt.Content.Items.Weapons.Melee;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
                var jam = player.GetModPlayer<SplendorJamPlayer>();
                jam.active = true;
                jam.rainbow = true;
            }
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
                if (player.TryGetModPlayer<VanityPlayer>(out var vanityPlayer))
                {
                    vanityPlayer.tendrilCursor = true;
                }
            }
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<TendrilsEffect>())
                {
                    if (player.TryGetModPlayer<VanityPlayer>(out var vanityPlayer))
                    {
                        vanityPlayer.tendrilCursor = false;
                    }
                }
            }
        }
        public class SplendorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShogunEnchant>(); 
            public override void PostUpdateEquips(Player player)
            {
                if (player.TryGetModPlayer<SplendorJamPlayer>(out var jamPlayer))
                {
                    jamPlayer.active = true;
                    jamPlayer.rainbow = true;
                }
            }
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<SplendorEffect>())
                {
                    if (player.TryGetModPlayer<SplendorJamPlayer>(out var jamPlayer))
                    {
                        // Safely clear the variables so the rainbow effect vanishes instantly
                        jamPlayer.active = false;
                        jamPlayer.rainbow = false;
                    }
                }
            }
        }
    }
}
