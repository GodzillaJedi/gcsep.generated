using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Gambler.Accessories;
using OrchidMod.Content.Gambler.Armors.Dungeon;
using OrchidMod.Content.Shapeshifter.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class TycheEnchant : BaseEnchant
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
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TycheEffect>(Item);
            if (player.AddEffect<LollipopEffect>(Item))
            {
                ModContent.GetInstance<SlimyLollipop>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<LensElementalEffect>(Item))
            {
                ModContent.GetInstance<ElementalLens>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DeepLocketEffect>(Item))
            {
                ModContent.GetInstance<DeepwaterLocket>().UpdateAccessory(player, hideVisual);
            }
        }
        public override Color nameColor => Color.Blue;
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GamblerDungeonHead>());
            recipe.AddIngredient(ModContent.ItemType<GamblerDungeonBody>());
            recipe.AddIngredient(ModContent.ItemType<GamblerDungeonLegs>());
            recipe.AddIngredient(ModContent.ItemType<SlimyLollipop>());
            recipe.AddIngredient(ModContent.ItemType<ElementalLens>());
            recipe.AddIngredient(ModContent.ItemType<DeepwaterLocket>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class TycheEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TycheEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GamblerDungeonHead>().UpdateArmorSet(player);
            }
        }
        public class LollipopEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TycheEnchant>();
        }
        public class LensElementalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TycheEnchant>();
        }
        public class DeepLocketEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TycheEnchant>();
        }
    }
}
