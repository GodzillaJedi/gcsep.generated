using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FungusEnchant : BaseEnchant
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
            player.AddEffect<FungusEffect>(Item);
        }
        public class FungusEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FungusEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FungusHat>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FungusHat>());
            recipe.AddIngredient(ModContent.ItemType<FungusGuard>());
            recipe.AddIngredient(ModContent.ItemType<FungusLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SwampSpike>());
            recipe.AddIngredient(ModContent.ItemType<SporeCoatingItem>(), 10);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
