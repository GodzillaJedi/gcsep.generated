using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity.Souls
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    public class PotJT : ModItem
    {
        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
            this.Item.defense = 13;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<AquaticHeartEffect>(Item))
                ModContent.GetInstance<AquaticHeart>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<HideofAstrumDeusEffect>(Item))
                ModContent.GetInstance<HideofAstrumDeus>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<ToxicHeartEffect>(Item))
                ModContent.GetInstance<ToxicHeart>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<AuricSoulEffect>(Item))
                ModContent.GetInstance<AuricSoulArtifact>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<SkullCrownEffect>(Item))
                ModContent.GetInstance<OccultSkullCrown>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<TheEvolutionEffect>(Item))
                ModContent.GetInstance<TheEvolution>().UpdateAccessory(player, hideVisual);
            if (player.AddEffect<LeviathanAmbergrisEffect>(Item))
                ModContent.GetInstance<LeviathanAmbergris>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementalArtifact>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<AuricBar>(5);

            recipe.AddIngredient<ElementalArtifact>();
            recipe.AddIngredient<OccultSkullCrown>();
            recipe.AddIngredient<AuricSoulArtifact>();
            recipe.AddIngredient<LeviathanAmbergris>();
            recipe.AddIngredient<TheEvolution>();
            recipe.AddIngredient<AquaticHeart>();
            recipe.AddIngredient<HideofAstrumDeus>();
            recipe.AddIngredient<ToxicHeart>();

            recipe.AddTile<CosmicAnvil>();

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        [ExtendsFromMod(ModCompatibility.Calamity.Name)]
        public class AquaticHeartEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<AquaticHeart>();
        }
        public class HideofAstrumDeusEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<HideofAstrumDeus>();
        }
        public class ToxicHeartEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ToxicHeart>();
        }
        public class AuricSoulEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<AuricSoulArtifact>();
        }
        public class SkullCrownEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<OccultSkullCrown>();
        }
        public class TheEvolutionEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<TheEvolution>();
        }
        public class LeviathanAmbergrisEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<LeviathanAmbergris>();
        }
    }
}
