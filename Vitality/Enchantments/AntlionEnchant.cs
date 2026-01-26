using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.AnarchulesBeetle;
using VitalityMod.Items.GrandAntlion;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class AntlionEnchant : BaseEnchant
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
        public override Color nameColor => Color.LightSalmon;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AntlionEffect>(Item);
            if (player.AddEffect<SacIchorEffect>(Item))
            {
                ModContent.GetInstance<IchorSack>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GloveOSummoningEffect>(Item))
            {
                ModContent.GetInstance<SummonersGlove>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AntlionHelmet>());
            recipe.AddIngredient(ModContent.ItemType<AntlionHood>());
            recipe.AddIngredient(ModContent.ItemType<AntlionHide>());
            recipe.AddIngredient(ModContent.ItemType<AntlionGreaves>());
            recipe.AddIngredient(ModContent.ItemType<IchorSack>());
            recipe.AddIngredient(ModContent.ItemType<SummonersGlove>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class AntlionEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AntlionHelmet>().UpdateArmorSet(player);
                ModContent.GetInstance<AntlionHood>().UpdateArmorSet(player);
            }
        }
        public class SacIchorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
        }
        public class GloveOSummoningEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AnarchyEnchant>();
        }
    }
}
