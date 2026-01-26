using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Souls
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    public class ElementalArtifact : ModItem
    {
        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = ItemRarityID.Red;
            this.Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<ChaosStoneEffect>(Item))
                ModContent.GetInstance<ChaosStone>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<CryoStoneEffect>(Item))
                ModContent.GetInstance<CryoStone>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<AeroStoneEffect>(Item))
                ModContent.GetInstance<AeroStone>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<BloomStoneEffect>(Item))
                ModContent.GetInstance<BloomStone>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            recipe.AddIngredient<AeroStone>();
            recipe.AddIngredient<CryoStone>();
            recipe.AddIngredient<ChaosStone>();
            recipe.AddIngredient<BloomStone>();
            recipe.AddIngredient<GalacticaSingularity>(5);

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        [ExtendsFromMod(ModCompatibility.Calamity.Name)]
        public class ChaosStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ChaosStone>();
        }
        public class CryoStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<CryoStone>();
        }
        public class AeroStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<AeroStone>();
        }
        public class BloomStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<BloomStone>();
        }
    }
}
