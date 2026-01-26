using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Projectiles.Healer;
using ThoriumMod.Projectiles.Thrower;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class WhiteDwarfEnchant : BaseEnchant
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
            Item.rare = 10;
            Item.value = 300000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<WhiteDwarfEffect>(Item))
            {
                player.GetThoriumPlayer().setWhiteDwarf = true;
            }
        }
        public class WhiteDwarfEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WhiteDwarfEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfMask>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfGuard>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfGreaves>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfKunai>());
            recipe.AddIngredient(ModContent.ItemType<WhiteDwarfPickaxe>());
            recipe.AddIngredient(ModContent.ItemType<AngelsEnd>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
