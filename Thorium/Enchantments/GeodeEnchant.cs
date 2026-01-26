using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Geode;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GeodeEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GeodeEffect>(Item);
            if (player.AddEffect<CrystalSpearTipEffect>(Item))
            {
                ModContent.GetInstance<CrystalSpearTip>().UpdateAccessory(player, hideVisual);
            }
        }
        public class GeodeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeodeEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GeodeHelmet>().UpdateArmorSet(player);
            }
        }
        public class CrystalSpearTipEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeodeEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {


            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<GeodeHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GeodeChestplate>());
            recipe.AddIngredient(ModContent.ItemType<GeodeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CrystalGeode>(), 100);
            recipe.AddIngredient(ModContent.ItemType<CrystalSpearTip>());
            recipe.AddIngredient(ModContent.ItemType<GeodePickaxe>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
