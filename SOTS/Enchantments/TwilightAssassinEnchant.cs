using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS.Items.Chaos;
using SOTS.Items.Planetarium;
using SOTS.Items.Planetarium.FromChests;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class TwilightAssassinEnchant : BaseEnchant
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
            if (player.AddEffect<HyperdriveEffect>(Item))
            {
                ModContent.GetInstance<Hyperdrive>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BladeNecklaceEffect>(Item))
            {
                ModContent.GetInstance<BladeNecklace>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BlinkEffect>(Item))
            {
                ModContent.GetInstance<BlinkPack>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<TwilightAssassinEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TwilightAssassinsCirclet>());
            recipe.AddIngredient(ModContent.ItemType<TwilightAssassinsChestplate>());
            recipe.AddIngredient(ModContent.ItemType<TwilightAssassinsLeggings>());
            recipe.AddIngredient(ModContent.ItemType<Hyperdrive>());
            recipe.AddIngredient(ModContent.ItemType<BladeNecklace>());
            recipe.AddIngredient(ModContent.ItemType<BlinkPack>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class TwilightAssassinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TwilightAssassinEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TwilightAssassinsCirclet>().UpdateArmorSet(player);
            }
        }
        public class HyperdriveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TwilightAssassinEnchant>();
        }
        public class BladeNecklaceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TwilightAssassinEnchant>();
        }
        public class BlinkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TwilightAssassinEnchant>();
        }
    }
}
