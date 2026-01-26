using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Armor;
using VitalityMod.Items.GrandAntlion;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class MetalsmithEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkSlateGray;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MetalsmithEffect>(Item);
            if (player.AddEffect<AntlionDiggerEffect>(Item))
            {
                ModContent.GetInstance<AntlionDiggingClaw>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FossilizedEffect>(Item))
            {
                ModContent.GetInstance<FossilizedChains>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MetalsmithHelmet>());
            recipe.AddIngredient(ModContent.ItemType<MetalsmithChestplate>());
            recipe.AddIngredient(ModContent.ItemType<MetalsmithLeggings>());
            recipe.AddIngredient(ModContent.ItemType<FossilizedChains>());
            recipe.AddIngredient(ModContent.ItemType<AntlionDiggingClaw>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class MetalsmithEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MetalsmithEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MetalsmithHelmet>().UpdateArmorSet(player);
            }
        }
        public class FossilizedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MetalsmithEnchant>();
        }
        public class AntlionDiggerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MetalsmithEnchant>();
        }
    }
}
