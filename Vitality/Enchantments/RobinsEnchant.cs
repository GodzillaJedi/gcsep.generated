using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Accessories;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Armor;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class RobinsEnchant : BaseEnchant
    {
        public override Color nameColor => Color.DarkGreen;
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
            player.AddEffect<RobinEffect>(Item);
            if (player.AddEffect<NonSlipEffect>(Item))
            {
                ModContent.GetInstance<NonSlipSocks>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ChlorBloodEffect>(Item))
            {
                ModContent.GetInstance<ChlorophyteBloodMagnet>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RobinsHood>());
            recipe.AddIngredient(ModContent.ItemType<RobinsShirt>());
            recipe.AddIngredient(ModContent.ItemType<RobinsPants>());
            recipe.AddIngredient(ModContent.ItemType<NonSlipSocks>());
            recipe.AddIngredient(ModContent.ItemType<ChlorophyteBloodMagnet>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class RobinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RobinsEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<RobinsHood>().UpdateArmorSet(player);
            }
        }
        public class NonSlipEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RobinsEnchant>();
        }
        public class ChlorBloodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RobinsEnchant>();
        }
    }
}
