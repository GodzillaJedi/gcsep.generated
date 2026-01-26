using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Armor.Classless.Hardmode.SnakescaleArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.SelfsimilarArmor;
using Polarities.Content.Items.Weapons.Melee.Flails.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class SnakescaleEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Polarities;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(44, 64, 138);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SnakescaleEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SnakescaleMask>());
            recipe.AddIngredient(ModContent.ItemType<SnakescaleArmor>());
            recipe.AddIngredient(ModContent.ItemType<SnakescaleGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Snakebite>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class SnakescaleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnakescaleEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SnakescaleMask>().UpdateArmorSet(player);
            }
        }
    }
}
