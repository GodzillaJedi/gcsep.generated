using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Accessories.ExpertMode.PreHardmode;
using Polarities.Content.Items.Accessories.Information.PreHardmode;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.ConvectiveArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.FractalArmor;
using Polarities.Content.Items.Weapons.Melee.Warhammers.PreHardmode.Other;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class FractalEnchant : BaseEnchant
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

        public override Color nameColor => new(255, 119, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FractalEffect>(Item);
            if (player.AddEffect<AnchorEffect>(Item))
            {
                ModContent.GetInstance<DimensionalAnchor>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CoPEffect>(Item))
            {
                ModContent.GetInstance<CloakofPockets>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("gcsep:FractalHelms");
            recipe.AddIngredient(ModContent.ItemType<FractalBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<FractalGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CloakofPockets>());
            recipe.AddIngredient(ModContent.ItemType<DimensionalAnchor>());
            recipe.AddIngredient(ModContent.ItemType<RiftOnAStick>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class FractalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FractalEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FractalHelmetMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<FractalHelmetMage>().UpdateArmorSet(player);
                ModContent.GetInstance<FractalHelmetRanger>().UpdateArmorSet(player);
                ModContent.GetInstance<FractalHelmetSummoner>().UpdateArmorSet(player);
            }
        }
        public class AnchorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FractalEnchant>();
        }
        public class CoPEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FractalEnchant>();
        }
    }
}
