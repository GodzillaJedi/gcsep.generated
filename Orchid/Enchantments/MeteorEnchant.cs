using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Alchemist.Accessories;
using OrchidMod.Content.Gambler.Accessories;
using OrchidMod.Content.Guardian.Armors.Meteorite;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class MeteorEnchant : BaseEnchant
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
        public override Color nameColor => Color.Aquamarine;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MeteoriteEffect>(Item);
            if (player.AddEffect<MeteorToolbeltEffect>(Item))
            {
                ModContent.GetInstance<MeteorToolbelt>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ConventEffect>(Item))
            {
                ModContent.GetInstance<KeystoneOfTheConvent>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DemPockMirrorEffect>(Item))
            {
                ModContent.GetInstance<DemonicPocketMirror>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GuardianMeteoriteHead>());
            recipe.AddIngredient(ModContent.ItemType<GuardianMeteoriteChest>());
            recipe.AddIngredient(ModContent.ItemType<GuardianMeteoriteLegs>());
            recipe.AddIngredient(ModContent.ItemType<MeteorToolbelt>());
            recipe.AddIngredient(ModContent.ItemType<KeystoneOfTheConvent>());
            recipe.AddIngredient(ModContent.ItemType<DemonicPocketMirror>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class MeteoriteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MeteorEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GuardianMeteoriteHead>().UpdateArmorSet(player);
            }
        }
        public class MeteorToolbeltEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MeteorEnchant>();
        }
        public class ConventEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MeteorEnchant>();
        }
        public class DemPockMirrorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MeteorEnchant>();
        }
    }
}
