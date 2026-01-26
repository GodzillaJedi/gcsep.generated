using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Armor;
using VitalityMod.Items.MosquitoMonarch;
using static gcsep.Vitality.Enchantments.OccultEnchant;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class TrailHunterEnchant : BaseEnchant
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
        public override Color nameColor => Color.LightPink;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OcculticEffect>(Item);
            if (player.AddEffect<WizardryEffect>(Item))
            {
                ModContent.GetInstance<SanguineRing>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TrailhunterHat>());
            recipe.AddIngredient(ModContent.ItemType<TrailhunterCoat>());
            recipe.AddIngredient(ModContent.ItemType<TrailhunterLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SanguineRing>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class TrailhunterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TrailHunterEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TrailhunterHat>().UpdateArmorSet(player);
            }
        }
        public class SanRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TrailHunterEnchant>();
        }
    }
}
