using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.AnarchulesBeetle;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class AnarchyEnchant : BaseEnchant
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
        public override Color nameColor => Color.Lavender;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AnarchyEffect>(Item);
            if (player.AddEffect<AnarchyCapeEffect>(Item))
            {
                ModContent.GetInstance<AnarchyCape>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AnarchyHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<AnarchyChainmail>());
            recipe.AddIngredient(ModContent.ItemType<AnarchyLeggings>());
            recipe.AddIngredient(ModContent.ItemType<AnarchyCape>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class AnarchyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AnarchyHeadgear>().UpdateArmorSet(player);
            }
        }
        public class AnarchyCapeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
        }
    }
}
