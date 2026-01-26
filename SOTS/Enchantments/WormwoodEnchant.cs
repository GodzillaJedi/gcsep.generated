using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS;
using SOTS.Items.Celestial;
using SOTS.Items.CritBonus;
using SOTS.Items.Nature;
using SOTS.Items.Temple;
using SOTS.Items.Tide;
using SOTS.Projectiles.Nature;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class WormwoodEnchant : BaseEnchant
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
        public override Color nameColor => new(255, 128, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<PearShieldEffect>(Item))
            {
                ModContent.GetInstance<PricklyPearShield>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CloverEffect>(Item))
            {
                ModContent.GetInstance<CloverCharm>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<TailEffect>(Item))
            {
                ModContent.GetInstance<LihzahrdTail>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SeaHeartEffect>(Item))
            {
                ModContent.GetInstance<HeartOfTheSea>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BotSymEffect>(Item))
            {
                ModContent.GetInstance<BotanicalSymbiote>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<NatureWreathEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<NatureWreath>());
            recipe.AddIngredient(ModContent.ItemType<NatureShirt>());
            recipe.AddIngredient(ModContent.ItemType<NatureLeggings>());
            recipe.AddIngredient(ModContent.ItemType<BotanicalSymbiote>());
            recipe.AddIngredient(ModContent.ItemType<HeartOfTheSea>());
            recipe.AddIngredient(ModContent.ItemType<LihzahrdTail>());
            recipe.AddIngredient(ModContent.ItemType<CloverCharm>());
            recipe.AddIngredient(ModContent.ItemType<PricklyPearShield>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class NatureWreathEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WormwoodEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<NatureWreath>().UpdateArmorSet(player);
            }
        }
        public class PearShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WormwoodEnchant>();
        }
        public class CloverEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WormwoodEnchant>();
        }
        public class TailEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WormwoodEnchant>();
        }
        public class SeaHeartEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WormwoodEnchant>();
        }
        public class BotSymEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WormwoodEnchant>();
        }
    }
}
