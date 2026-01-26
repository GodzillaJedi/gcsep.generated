using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.LivingWood;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class LivingWoodEnchantV : BaseEnchant
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
        public override Color nameColor => Color.Maroon;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<LivingWoodEffectV>(Item);
            if (player.AddEffect<NotSafeCactusEffect>(Item))
            {
                ModContent.GetInstance<CactusSafeguard>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<HerbEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LivingWoodHood>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CactusSafeguard>());
            recipe.AddIngredient(ModContent.ItemType<HerbEnchant>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class LivingWoodEffectV : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingWoodEnchantV>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LivingWoodHood>().UpdateArmorSet(player);
            }
        }
        public class NotSafeCactusEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingWoodEnchantV>();
        }
    }
}
