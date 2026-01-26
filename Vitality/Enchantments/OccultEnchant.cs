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
    public class OccultEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkBlue;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OcculticEffect>(Item);
            if (player.AddEffect<WizardryEffect>(Item))
            {
                ModContent.GetInstance<GuidetoWizardry>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DarkWizardryEffect>(Item))
            {
                ModContent.GetInstance<GuidetoShadowWizardry>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OccultHood>());
            recipe.AddIngredient(ModContent.ItemType<OccultRobe>());
            recipe.AddIngredient(ModContent.ItemType<GuidetoWizardry>());
            recipe.AddIngredient(ModContent.ItemType<GuidetoShadowWizardry>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class OcculticEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OccultEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OccultHood>().UpdateArmorSet(player);
            }
        }
        public class WizardryEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OccultEnchant>();
        }
        public class DarkWizardryEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OccultEnchant>();
        }
    }
}
