using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Gambler.Accessories;
using OrchidMod.Content.Guardian.Armors.Bamboo;
using OrchidMod.Content.Shapeshifter.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class BambooEnchant : BaseEnchant
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
        public override Color nameColor => Color.GreenYellow;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BambooEffect>(Item);
            if (player.AddEffect<ShawlWindEffect>(Item))
            {
                ModContent.GetInstance<ShawlOfTheWind>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PlantEnzymeEffect>(Item))
            {
                ModContent.GetInstance<PlantEnzymes>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SproutLuckyEffect>(Item))
            {
                ModContent.GetInstance<LuckySprout>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BunCloversEffect>(Item))
            {
                ModContent.GetInstance<BundleOfClovers>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GuardianBambooHead>());
            recipe.AddIngredient(ModContent.ItemType<GuardianBambooChest>());
            recipe.AddIngredient(ModContent.ItemType<GuardianBambooLegs>());
            recipe.AddIngredient(ModContent.ItemType<PlantEnzymes>());
            recipe.AddIngredient(ModContent.ItemType<ShawlOfTheWind>());
            recipe.AddIngredient(ModContent.ItemType<LuckySprout>());
            recipe.AddIngredient(ModContent.ItemType<BundleOfClovers>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class BambooEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BambooEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GuardianBambooHead>().UpdateArmorSet(player);
            }
        }
        public class ShawlWindEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BambooEnchant>();
        }
        public class PlantEnzymeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BambooEnchant>();
        }
        public class SproutLuckyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BambooEnchant>();
        }
        public class BunCloversEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BambooEnchant>();
        }
    }
}
