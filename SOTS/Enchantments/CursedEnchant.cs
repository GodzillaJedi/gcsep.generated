using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS;
using SOTS.Items.Evil;
using SOTS.Items.Pyramid;
using SOTS.Void;
using SOTSBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class CursedEnchant : BaseEnchant
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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<MidnightEffect>(Item))
            {
                ModContent.GetInstance<MidnightPrism>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<HeartJarEffect>(Item))
            {
                ModContent.GetInstance<HeartInAJar>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<WitchHeartEffect>(Item))
            {
                ModContent.GetInstance<WitchHeart>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<HypersonicTuneEffect>(Item))
            {
                ModContent.GetInstance<HypersonicTuner>().UpdateAccessory(player, hideVisual);
            }
            //if (player.AddEffect<FourDEffect>(Item))
            //{
            //    ModContent.GetInstance<Hero>().UpdateAccessory(player, hideVisual);
            //}
            if (player.AddEffect<CursedEffect>(Item))
            {
                SOTSPlayer sOTSPlayer = SOTSPlayer.ModPlayer(player);
                VoidPlayer voidPlayer = VoidPlayer.ModPlayer(player);
                voidPlayer.voidMeterMax2 += 80;
                player.statManaMax2 += 80;
                voidPlayer.voidCost -= 0.15f;
                player.manaCost -= 0.15f;
                sOTSPlayer.RubyMonolith = true;
                sOTSPlayer.RubyMonolithIsNOTVanity = true;
                sOTSPlayer.CanCurseSwap = true;
            }
        }
        public override Color nameColor => new Color(244, 25, 255);
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CursedRobe>());
            recipe.AddIngredient(ModContent.ItemType<CursedHood>());
            recipe.AddIngredient(ModContent.ItemType<WitchHeart>());
            recipe.AddIngredient(ModContent.ItemType<HeartInAJar>());
            recipe.AddIngredient(ModContent.ItemType<MidnightPrism>());
            if (ModCompatibility.SOTSBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<HypersonicTuner>());
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class HypersonicTuneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CursedEnchant>();
        }
        public class MidnightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CursedEnchant>();
        }
        public class HeartJarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CursedEnchant>();
        }
        public class WitchHeartEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CursedEnchant>();
        }
        public class CursedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CursedEnchant>();
        }
        public class FourDEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CursedEnchant>();
        }
    }
}
