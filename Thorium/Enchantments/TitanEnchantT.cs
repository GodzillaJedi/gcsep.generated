using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Titan;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TitanEnchantT : BaseEnchant
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
            Item.value = 200000;
        }
        public static readonly int SetDamage = 18;
        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<TitanThoriumEffect>(Item))
            {
                ModContent.GetInstance<TitanHeadgear>().UpdateArmorSet(player);
                ModContent.GetInstance<TitanHelmet>().UpdateArmorSet(player);
                ModContent.GetInstance<TitanMask>().UpdateArmorSet(player);
            }
            if (player.AddEffect<CrystalEyeEffect>(Item))
            {
                ModContent.GetInstance<MaskoftheCrystalEye>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<AbyssalShellEffect>(Item))
            {
                ModContent.GetInstance<AbyssalShell>().UpdateAccessory(player, hideVisual);
            }
        }
        public class TitanThoriumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanEnchantT>();
            public override bool MutantsPresenceAffects => true;
        }
        public class CrystalEyeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanEnchantT>();
            public override bool MutantsPresenceAffects => true;
        }
        public class AbyssalShellEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanEnchantT>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("gcsep:AnyTitanHelmet");
            recipe.AddIngredient(ModContent.ItemType<TitanBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<TitanGreaves>());
            recipe.AddIngredient(ModContent.ItemType<MaskoftheCrystalEye>());
            recipe.AddIngredient(ModContent.ItemType<AbyssalShell>());
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
