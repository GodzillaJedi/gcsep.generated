using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Scuba;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class ScubaEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkGray;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<ScubaEffect>(Item))
            {
                ModContent.GetInstance<ScubaHelmet>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ScubaHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ScubaSuit>());
            recipe.AddIngredient(ModContent.ItemType<ScubaLeggings>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class ScubaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TrawlerHeader>();
            public override int ToggleItemType => ModContent.ItemType<ScubaEnchant>();
        }
    }
}
