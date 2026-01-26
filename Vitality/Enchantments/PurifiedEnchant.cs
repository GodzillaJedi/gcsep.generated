using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Purified;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class PurifiedEnchant : BaseEnchant
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
        public override Color nameColor => Color.LimeGreen;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PurifiedEffect>(Item);
            if (player.AddEffect<BeekeepersBadgeEffect>(Item))
            {
                ModContent.GetInstance<BeekeepersBadge>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<RobinsEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PurifiedHelmet>());
            recipe.AddIngredient(ModContent.ItemType<PurifiedBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<PurifiedLeggings>());
            recipe.AddIngredient(ModContent.ItemType<BeekeepersBadge>());
            recipe.AddIngredient(ModContent.ItemType<RobinsEnchant>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class PurifiedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PurifiedEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PurifiedHelmet>().UpdateArmorSet(player);
            }
        }
        public class BeekeepersBadgeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PurifiedEnchant>();
        }
    }
}
