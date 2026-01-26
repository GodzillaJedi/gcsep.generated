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
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ThoriumMod.Utilities;
using ThoriumRework;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class RhapsodistEnchant : BaseEnchant
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
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SoloistHelmEffect>(Item);
            player.AddEffect<InspiratorHelmEffect>(Item);
        }
        public class SoloistHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RhapsodistEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SoloistHat>().UpdateArmorSet(player);
            }
        }
        public class InspiratorHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RhapsodistEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<InspiratorsHelmet>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<InspiratorsHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SoloistHat>());
            recipe.AddIngredient(ModContent.ItemType<RhapsodistChestWoofer>());
            recipe.AddIngredient(ModContent.ItemType<RhapsodistBoots>());
            recipe.AddIngredient(ModContent.ItemType<JingleBells>());
            recipe.AddIngredient(ModContent.ItemType<Sousaphone>());
            recipe.AddIngredient(ModContent.ItemType<EdgeofImagination>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
