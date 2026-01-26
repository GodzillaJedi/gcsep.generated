using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities.Content.Items.Armor.Classless.PreHardmode.AerogelArmor;
using Polarities.Content.Items.Weapons.Melee.Yoyos.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class AerogelEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(150, 168, 214);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AerogelHelmEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AerogelHood>());
            recipe.AddIngredient(ModContent.ItemType<AerogelRobe>());
            recipe.AddIngredient(ModContent.ItemType<Flywheel>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class AerogelHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StormcloudEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AerogelHood>().UpdateArmorSet(player);
            }
        }
    }
}
