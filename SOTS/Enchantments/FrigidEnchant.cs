using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS;
using SOTS.Items;
using SOTS.Items.Chaos;
using SOTS.Items.Permafrost;
using SOTS.Items.Tide;
using SOTS.Void;
using SOTSBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class FrigidEnchant : BaseEnchant
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
        public override Color nameColor => new Color(255, 128, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<PrismarineEffect>(Item))
            {
                ModContent.GetInstance<PrismarineNecklace>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AntennaeEffect>(Item))
            {
                ModContent.GetInstance<HydrokineticAntennae>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AqueductEffect>(Item))
            {
                ModContent.GetInstance<ArcaneAqueduct>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FrigidEffect>(Item))
            {
                ModContent.GetInstance<FrigidCrown>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<FrigidArmorEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrigidCrown>());
            recipe.AddIngredient(ModContent.ItemType<FrigidRobe>());
            recipe.AddIngredient(ModContent.ItemType<ShatterShardChestplate>());
            recipe.AddIngredient(ModContent.ItemType<FrigidGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ArcaneAqueduct>());
            recipe.AddIngredient(ModContent.ItemType<HydrokineticAntennae>());
            recipe.AddIngredient(ModContent.ItemType<PrismarineNecklace>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class PrismarineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchant>();
        }
        public class AntennaeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchant>();
        }
        public class AqueductEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchant>();
        }
        public class FrigidEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchant>();
        }
        public class FrigidArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FrigidCrown>().UpdateArmorSet(player);
            }
        }
    }
}
