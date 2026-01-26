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

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class HerbEnchant : BaseEnchant
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
        public override Color nameColor => Color.GreenYellow;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HerbEffect>(Item);
            if (player.AddEffect<HoneycombEffect>(Item))
            {
                ModContent.GetInstance<HoneycombAmulet>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<NectarEffect>(Item))
            {
                ModContent.GetInstance<NectarAmulet>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HerbHelmet>());
            recipe.AddIngredient(ModContent.ItemType<HerbShirt>());
            recipe.AddIngredient(ModContent.ItemType<HerbGreaves>());
            recipe.AddIngredient(ModContent.ItemType<HoneycombAmulet>());
            recipe.AddIngredient(ModContent.ItemType<NectarAmulet>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class HerbEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HerbEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HerbHelmet>().UpdateArmorSet(player);
            }
        }
        public class HoneycombEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HerbEnchant>();
        }
        public class NectarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HerbEnchant>();
        }
    }
}
