using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Gambler.Accessories;
using OrchidMod.Content.Gambler.Armors.Outlaw;
using OrchidMod.Content.Guardian.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class OutlawEnchant : BaseEnchant
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
        public override Color nameColor => Color.BurlyWood;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OutlawEffect>(Item);
            if (player.AddEffect<BurnCloversEffect>(Item))
            {
                ModContent.GetInstance<BurningClovers>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PastBattleEffect>(Item))
            {
                ModContent.GetInstance<BadgeBattlesPast>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ImpDieEffect>(Item))
            {
                ModContent.GetInstance<ImpDiceCup>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BunChipsEffect>(Item))
            {
                ModContent.GetInstance<PileOfChips>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OutlawHat>());
            recipe.AddIngredient(ModContent.ItemType<OutlawVest>());
            recipe.AddIngredient(ModContent.ItemType<OutlawPants>());
            recipe.AddIngredient(ModContent.ItemType<BurningClovers>());
            recipe.AddIngredient(ModContent.ItemType<BadgeBattlesPast>());
            recipe.AddIngredient(ModContent.ItemType<ImpDiceCup>());
            recipe.AddIngredient(ModContent.ItemType<PileOfChips>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class OutlawEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OutlawEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OutlawHat>().UpdateArmorSet(player);
            }
        }
        public class BurnCloversEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OutlawEnchant>();
        }
        public class PastBattleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OutlawEnchant>();
        }
        public class ImpDieEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OutlawEnchant>();
        }
        public class BunChipsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OutlawEnchant>();
        }
    }
}
