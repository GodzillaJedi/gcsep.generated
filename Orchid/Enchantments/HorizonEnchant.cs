using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Gambler.Accessories;
using OrchidMod.Content.Guardian.Accessories;
using OrchidMod.Content.Guardian.Armors.Horizon;
using OrchidMod.Content.Shapeshifter.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class HorizonEnchant : BaseEnchant
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
        public override Color nameColor => new(153, 200, 193);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HorizonEffect>(Item);
            if (player.AddEffect<ColossolWormToothEffect>(Item))
            {
                ModContent.GetInstance<ColossalWormTooth>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GoblinEffect>(Item))
            {
                ModContent.GetInstance<GoblinDagger>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<VultureCharmEffect>(Item))
            {
                ModContent.GetInstance<VultureCharm>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GuardianHorizonHead>());
            recipe.AddIngredient(ModContent.ItemType<GuardianHorizonChest>());
            recipe.AddIngredient(ModContent.ItemType<GuardianHorizonLegs>());
            recipe.AddIngredient(ModContent.ItemType<GoblinDagger>());
            recipe.AddIngredient(ModContent.ItemType<ColossalWormTooth>());
            recipe.AddIngredient(ModContent.ItemType<VultureCharm>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class HorizonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HorizonEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GuardianHorizonHead>().UpdateArmorSet(player);
            }
        }
        public class GoblinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HorizonEnchant>();
        }
        public class ColossolWormToothEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HorizonEnchant>();
        }
        public class VultureCharmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HorizonEnchant>();
        }
    }
}
