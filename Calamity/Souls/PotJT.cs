using CalamityMod;
using CalamityMod.CalPlayer;
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
            player.AddEffect<AquaticHeartEffect>(Item);
            player.AddEffect<HideofAstrumDeusEffect>(Item);
            player.AddEffect<ToxicHeartEffect>(Item);
            player.AddEffect<AuricSoulEffect>(Item);
            player.AddEffect<SkullCrownEffect>(Item);
            player.AddEffect<TheEvolutionEffect>(Item);
            player.AddEffect<LeviathanAmbergrisEffect>(Item);
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
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().aquaticHeart = true;
            }
        }
        public class HideofAstrumDeusEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<HideofAstrumDeus>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.hideOfDeus = true;
                if (calamityPlayer.hideOfDeusMeleeBoostTimer > 0)
                {
                    player.GetDamage<TrueMeleeDamageClass>() += 0.3f;
                }
            }
        }
        public class ToxicHeartEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ToxicHeart>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.toxicHeart = true;
                calamityPlayer.toxicHeartVisuals = true;
                calamityPlayer.SicknessDebuffMultiplier += 0.5f;
            }
        }
        public class AuricSoulEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<AuricSoulArtifact>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().auricSArtifact = true;
            }
        }
        public class SkullCrownEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<OccultSkullCrown>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.laudanum = true;
                calamityPlayer.heartOfDarkness = true;
                calamityPlayer.stressPills = true;
            }
        }
        public class TheEvolutionEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<TheEvolution>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().evolution = true;
            }
        }
        public class LeviathanAmbergrisEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<LeviathanAmbergris>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.lAmbergris = true;
                calamityPlayer.lAmbergrisVisual = true;
            }
        }
    }
}
