using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Armor.Daybloom;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.TideDrops.StreamSurfer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SunflowerEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(246, 197, 26);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SunflowerHelmEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DaybloomHead>();
            recipe.AddIngredient<DaybloomBody>();
            recipe.AddIngredient<DaybloomLegs>();
            recipe.AddIngredient<BriarFlowerItem>();
            recipe.AddIngredient<HangingSunPot>(3);
            recipe.AddIngredient(ItemID.SunflowerMinecart);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class SunflowerHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SunflowerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DaybloomHead>().UpdateArmorSet(player);
            }
        }
    }
}
