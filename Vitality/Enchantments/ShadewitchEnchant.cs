using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Armor;
using VitalityMod.Items.Accessories;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class ShadewitchEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkRed;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ShadewitchEffect>(Item);
            if (player.AddEffect<CarefreeMelodyEffect>(Item))
            {
                ModContent.GetInstance<CarefreeMelody>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GrimmEffect>(Item))
            {
                ModContent.GetInstance<Grimmchild>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadewitchHat>());
            recipe.AddIngredient(ModContent.ItemType<ShadewitchCoat>());
            recipe.AddIngredient(ModContent.ItemType<ShadewitchLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SolarbrandRing>());
            recipe.AddIngredient(ModContent.ItemType<RingoftheLords>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class ShadewitchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShadewitchEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ShadewitchHat>().UpdateArmorSet(player);
            }
        }
        public class CarefreeMelodyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShadewitchEnchant>();
        }
        public class GrimmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ChaosForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShadewitchEnchant>();
        }
    }
}
