using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ContinentOfJourney.Items.Accessories;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Calamity.Enchantments;
using gcsep.Content.Buffs.Minions;
using gcsep.Content.Projectiles.Minions;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            player.AddEffect<YharonSummonEffect>(Item);
            ModContent.GetInstance<ElementalArtifact>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<AuricBar>(5);

            recipe.AddIngredient<ElementalArtifact>();
            recipe.AddIngredient<OccultSkullCrown>();
            recipe.AddIngredient<FadedIdolatry>();
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
                ModContent.GetInstance<AquaticHeart>().UpdateAccessory(player, true);
            }
        }
        public class HideofAstrumDeusEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<HideofAstrumDeus>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HideofAstrumDeus>().UpdateAccessory(player, true);
            }
        }
        public class ToxicHeartEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ToxicHeart>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ToxicHeart>().UpdateAccessory(player, true);
            }
        }
        public class AuricSoulEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<FadedIdolatry>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FadedIdolatry>().UpdateAccessory(player, true);
            }
        }
        public class SkullCrownEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<OccultSkullCrown>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OccultSkullCrown>().UpdateAccessory(player, true);
            }
        }
        public class TheEvolutionEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<TheEvolution>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TheEvolution>().UpdateAccessory(player, true);
            }
        }
        public class LeviathanAmbergrisEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<LeviathanAmbergris>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LeviathanAmbergris>().UpdateAccessory(player, true);
            }
        }
        public class YharonSummonEffect : CalamitySoulEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CalamitySoulHeader>(); 
            public override int ToggleItemType => ModContent.ItemType<PotJT>();
            public static void Maintain(Player player, IEntitySource source, int buffType, int minionType, int baseDamage, float knockback)
            {
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.whoAmI != Main.myPlayer || player.dead || player.ownedProjectileCounts[minionType] >= 1)
                    return;

                int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);
                Projectile.NewProjectile(source, player.Center, -Vector2.UnitY * 5f, minionType, damage, knockback, player.whoAmI);
            }
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<GCSEPlayer>().YharonSummon = true;

                Maintain(player, GetSource_EffectItem(player),
                    ModContent.BuffType<YharonSummonBuff>(), ModContent.ProjectileType<YharonBossMinion>(), 1200, 2f);
            }
        }
    }
}