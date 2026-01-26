using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using SOTS.Items;
using SOTS.Items.AbandonedVillage;
using SOTS.Items.ChestItems;
using SOTS.Items.CritBonus;
using SOTS.Items.Earth;
using SOTS.Items.Gems;
using SOTS.Items.OreItems;
using SOTS.Items.Permafrost;
using SOTS.Items.Pyramid;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SOTS.Souls
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name)]
    public class VoidShield : BaseSoul
    {
        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
            Item.defense = 5;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<BulwarkEffect>(Item))
                ModContent.GetInstance<BulwarkOfTheAncients>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<CrestEffect>(Item))
                ModContent.GetInstance<CrestofDasuver>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<OlympianEffect>(Item))
                ModContent.GetInstance<OlympianAegis>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<SpiritEffect>(Item))
                ModContent.GetInstance<SpiritShield>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<ChiseledEffect>(Item))
                ModContent.GetInstance<ChiseledBarrier>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<FortressEffect>(Item))
                ModContent.GetInstance<FortressGenerator>().UpdateAccessory(player, hideVisual);

            ModContent.GetInstance<WardingCharm>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShatterHeartShield>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ChallengerRing>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GraniteProtector>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<Lockpick>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<HellfireIcosahedron>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CursedIcosahedron>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BorealisIcosahedron>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShieldofDesecar>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShieldofStekpla>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);

            recipe.AddIngredient<BulwarkOfTheAncients>();
            recipe.AddIngredient<CrestofDasuver>();
            recipe.AddIngredient<OlympianAegis>();
            recipe.AddIngredient<SpiritShield>();
            recipe.AddIngredient<ChiseledBarrier>();
            recipe.AddIngredient<FortressGenerator>();
            recipe.AddIngredient<WardingCharm>();
            recipe.AddIngredient<ShatterHeartShield>();
            recipe.AddIngredient<ChallengerRing>();
            recipe.AddIngredient<Lockpick>();
            recipe.AddIngredient<HellfireIcosahedron>();
            recipe.AddIngredient<CursedIcosahedron>();
            recipe.AddIngredient<BorealisIcosahedron>();
            recipe.AddIngredient<ShieldofStekpla>();
            recipe.AddIngredient<ShieldofDesecar>();

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();
        }
        [JITWhenModsEnabled(ModCompatibility.SOTS.Name)]
        [ExtendsFromMod(ModCompatibility.SOTS.Name)]
        public class BulwarkEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<BulwarkOfTheAncients>();

            public override Header ToggleHeader => Header.GetHeader<VoidShieldHeader>();
        }
        public class CrestEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<CrestofDasuver>();

            public override Header ToggleHeader => Header.GetHeader<VoidShieldHeader>();
        }

        public class OlympianEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<OlympianAegis>();
            public override Header ToggleHeader => Header.GetHeader<VoidShieldHeader>();
        }
        public class SpiritEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<SpiritShield>();

            public override Header ToggleHeader => Header.GetHeader<VoidShieldHeader>();
        }

        public class ChiseledEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ChiseledBarrier>();

            public override Header ToggleHeader => Header.GetHeader<VoidShieldHeader>();
        }

        public class FortressEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<FortressGenerator>();

            public override Header ToggleHeader => Header.GetHeader<VoidShieldHeader>();
        }
    }
}
