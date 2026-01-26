using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Armor;
using VitalityMod.Items.Dungeon;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class GlowshroomEnchant : BaseEnchant
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
        public override Color nameColor => Color.Violet;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GlowshroomVitalityEffect>(Item);
            if (player.AddEffect<IncenseEffect>(Item))
            {
                ModContent.GetInstance<ManaIncense>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpectCuffEffect>(Item))
            {
                ModContent.GetInstance<SpectralCuffs>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GlowshroomHat>());
            recipe.AddIngredient(ModContent.ItemType<GlowshroomChestplate>());
            recipe.AddIngredient(ModContent.ItemType<GlowshroomLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ManaIncense>());
            recipe.AddIngredient(ModContent.ItemType<SpectralCuffs>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class GlowshroomVitalityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GlowshroomEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GlowshroomHat>().UpdateArmorSet(player);
            }
        }
        public class IncenseEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GlowshroomEnchant>();
        }
        public class SpectCuffEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GlowshroomEnchant>();
        }
    }
}
