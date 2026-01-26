using CalamityMod.Items.Armor.Daedalus;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Calamity.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Guardian.Accessories;
using OrchidMod.Content.Guardian.Armors.Empress;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class DawnlightEnchant : BaseEnchant
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
        public override Color nameColor => new(255, 213, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DawnlightEffect>(Item);
            if (player.AddEffect<HolyMailOEffect>(Item))
            {
                ModContent.GetInstance<ParryingMailHoly>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<MechMailOEffect>(Item))
            {
                ModContent.GetInstance<ParryingMailMech>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GuardianEmpressHead>());
            recipe.AddIngredient(ModContent.ItemType<GuardianEmpressChest>());
            recipe.AddIngredient(ModContent.ItemType<GuardianEmpressChestAlt>());
            recipe.AddIngredient(ModContent.ItemType<GuardianEmpressLegs>());
            recipe.AddIngredient(ModContent.ItemType<ParryingMailHoly>());
            recipe.AddIngredient(ModContent.ItemType<ParryingMailMech>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

    }
    public class DawnlightEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
        public override int ToggleItemType => ModContent.ItemType<DawnlightEnchant>();
        public override void PostUpdateEquips(Player player)
        {
            ModContent.GetInstance<GuardianEmpressHead>().UpdateArmorSet(player);
        }
    }
    public class HolyMailOEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
        public override int ToggleItemType => ModContent.ItemType<DawnlightEnchant>();
    }
    public class MechMailOEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ShamanistForceHeader>();
        public override int ToggleItemType => ModContent.ItemType<DawnlightEnchant>();
    }
}
