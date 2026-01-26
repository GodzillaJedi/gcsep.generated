using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Accessory.SanguineWardTree;
using SpiritMod.Items.Accessory.TalismanTree.GrislyTongue;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.BismiteSet.BismiteArmor;
using SpiritMod.Items.Sets.BloodcourtSet.BloodCourt;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class BloodcourtEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(190, 6, 6);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BloodCourtBolt>(Item);
            if (player.AddEffect<BloodCourtWard>(Item))
            {
                ModContent.GetInstance<BloodWard>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BloodCourtTongue>(Item))
            {
                ModContent.GetInstance<GrislyTongue>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BloodCourtHead>(1);
            recipe.AddIngredient<BloodCourtChestplate>(1);
            recipe.AddIngredient<BloodCourtLeggings>(1);
            recipe.AddIngredient<HealingCodex>(1);
            recipe.AddIngredient<GrislyTongue>(1);
            recipe.AddIngredient<BloodWard>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class BloodCourtBolt : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodcourtEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BloodCourtHead>().UpdateArmorSet(player);
            }
        }
        public class BloodCourtTongue : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodcourtEnchant>();
        }
        public class BloodCourtWard : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodcourtEnchant>();
        }
    }
}