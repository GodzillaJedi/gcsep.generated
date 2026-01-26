using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Guardian.Accessories;
using OrchidMod.Content.Shapeshifter.Armors.Ashwood;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class PyreEnchant : BaseEnchant
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
        public override Color nameColor => Color.Crimson;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PyreEffect>(Item);
            if (player.AddEffect<MechSpikeEffect>(Item))
            {
                ModContent.GetInstance<MechanicalSpike>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<TerrorFangEffect>(Item))
            {
                ModContent.GetInstance<TerrifyingMonsterFang>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShapeshifterAshwoodHead>());
            recipe.AddIngredient(ModContent.ItemType<ShapeshifterAshwoodChest>());
            recipe.AddIngredient(ModContent.ItemType<ShapeshifterAshwoodLegs>());
            recipe.AddIngredient(ModContent.ItemType<MechanicalSpike>());
            recipe.AddIngredient(ModContent.ItemType<TerrifyingMonsterFang>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class PyreEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PyreEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ShapeshifterAshwoodHead>().UpdateArmorSet(player);
            }
        }
        public class MechSpikeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PyreEnchant>();
        }
        public class TerrorFangEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PyreEnchant>();
        }
    }
}
