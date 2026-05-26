using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Content.Items.Accessories.Combat.Defensive;
using VitalityMod.Content.Items.Armor.Summoner.ArcaneGold;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class ArcaneGoldEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkGoldenrod;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ArcaneGoldEffect>(Item);
            if (player.AddEffect<ShieldProcEffect>(Item))
            {
                ModContent.GetInstance<ProtectorsShield>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GildedFists>(Item))
            {
                ModContent.GetInstance<GildedKnuckles>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ArcaneGoldHeadpiece>());
            recipe.AddIngredient(ModContent.ItemType<ArcaneGoldChainmail>());
            recipe.AddIngredient(ModContent.ItemType<ArcaneGoldLeggings>());
            recipe.AddIngredient(ModContent.ItemType<GildedKnuckles>());
            recipe.AddIngredient(ModContent.ItemType<ProtectorsShield>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class ArcaneGoldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ArcaneGoldHeadpiece>().UpdateArmorSet(player);
            }
        }
        public class ShieldProcEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
        }
        public class GildedFists : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
        }
    }
}
