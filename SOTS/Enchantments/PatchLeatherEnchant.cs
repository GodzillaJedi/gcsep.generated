using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS;
using SOTS.Items;
using SOTS.Items.Chaos;
using SOTS.Items.CritBonus;
using SOTS.Items.Permafrost;
using SOTS.Items.Pyramid;
using SOTS.Projectiles.Pyramid;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class PatchLeatherEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 40000;
        }
        public override Color nameColor => new(102, 89, 54);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<SnakesEffect>(Item))
            {
                ModContent.GetInstance<BundleOfSnakes>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FocusEffect>(Item))
            {
                ModContent.GetInstance<FocusReticle>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<PatchEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PatchLeatherHat>());
            recipe.AddIngredient(ModContent.ItemType<PatchLeatherTunic>());
            recipe.AddIngredient(ModContent.ItemType<PatchLeatherPants>());
            recipe.AddIngredient(ModContent.ItemType<BundleOfSnakes>());
            recipe.AddIngredient(ModContent.ItemType<FocusReticle>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class PatchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PatchLeatherEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PatchLeatherHat>().UpdateArmorSet(player);
            }
        }
        public class SnakesEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PatchLeatherEnchant>();
        }
        public class FocusEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PatchLeatherEnchant>();
        }
    }
}
