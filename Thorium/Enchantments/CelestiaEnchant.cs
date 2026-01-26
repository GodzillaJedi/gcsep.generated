using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs.Healer;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Projectiles.Healer;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CelestiaEnchant : BaseEnchant
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
            Item.rare = 6;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CelestiaCircleEffect>(Item);
        }
        public class CelestiaCircleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CelestiaEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CelestialCrown>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CelestialCrown>());
            recipe.AddIngredient(ModContent.ItemType<CelestialVestment>());
            recipe.AddIngredient(ModContent.ItemType<CelestialLeggings>());
            recipe.AddIngredient(ModContent.ItemType<CelestialWand>());
            //recipe.AddIngredient(ModContent.ItemType<HealingRain>());
            recipe.AddIngredient(ModContent.ItemType<Revivify>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
