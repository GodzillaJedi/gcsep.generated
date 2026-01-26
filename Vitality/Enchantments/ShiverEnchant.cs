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

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class ShiverEnchant : BaseEnchant
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
        public override Color nameColor => Color.CadetBlue;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ShiverEffect>(Item);
            if (player.AddEffect<SharpPocketEffect>(Item))
            {
                ModContent.GetInstance<PocketSharpener>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IceHelmet>());
            recipe.AddIngredient(ModContent.ItemType<IceBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<IceLeggings>());
            recipe.AddIngredient(ModContent.ItemType<PocketSharpener>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class ShiverEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShiverEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<IceHelmet>().UpdateArmorSet(player);
            }
        }
        public class SharpPocketEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShiverEnchant>();
        }
    }
}
