using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Accessories;
using VitalityMod.Items.Armor;
using VitalityMod.Items.Dreadnaught;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class TempleEnchant : BaseEnchant
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
        public override Color nameColor => Color.Gray;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TempleEffect>(Item);
            if (player.AddEffect<WrathfulCoreEffect>(Item))
            {
                ModContent.GetInstance<CoreOfWrath>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TempleHelmet>());
            recipe.AddIngredient(ModContent.ItemType<TempleBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<TempleLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CoreOfWrath>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class TempleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TempleEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TempleHelmet>().UpdateArmorSet(player);
            }
        }
        public class WrathfulCoreEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TempleEnchant>();
        }
    }
}
