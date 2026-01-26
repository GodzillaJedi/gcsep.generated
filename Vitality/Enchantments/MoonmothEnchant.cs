using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.MoonlightDragonfly;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class MoonmothEnchant : BaseEnchant
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
        public override Color nameColor => Color.Azure;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MoonmothEffect>(Item);
            if (player.AddEffect<DragonflyPendantEffect>(Item))
            {
                ModContent.GetInstance<DragonflyPendant>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SilkGloveEffect>(Item))
            {
                ModContent.GetInstance<SilkGlove>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MoonMothAntennae>());
            recipe.AddIngredient(ModContent.ItemType<MoonMothShirt>());
            recipe.AddIngredient(ModContent.ItemType<MoonMothPants>());
            recipe.AddIngredient(ModContent.ItemType<DragonflyPendant>());
            recipe.AddIngredient(ModContent.ItemType<SilkGlove>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class MoonmothEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MoonmothEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MoonMothAntennae>().UpdateArmorSet(player);
            }
        }
        public class DragonflyPendantEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MoonmothEnchant>();
        }
        public class SilkGloveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MoonmothEnchant>();
        }
    }
}
