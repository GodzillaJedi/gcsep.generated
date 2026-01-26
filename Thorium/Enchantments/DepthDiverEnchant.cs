using CalamityMod;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Buffs;
using ThoriumMod.Buffs.Pet;
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.Coral;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Projectiles.LightPets;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.CoralEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DepthDiverEnchant : BaseEnchant
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
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
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DepthDiverEffect>(Item);
            player.AddEffect<DepthDiverNewEffect>(Item);
            if (player.AddEffect<DoubloonEffect>(Item))
            {
                ModContent.GetInstance<DrownedDoubloon>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SeaHorseEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(Item);

                if (player.FindBuffIndex(ModContent.BuffType<SeahorseWandBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<SeahorseWandBuff>(), 3600);
                }

                // Check the same projectile type you spawn
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SeahorseWandPro>()] < 1)
                {
                    int baseDamage = player.ApplyArmorAccDamageBonusesTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);

                    int projIndex = Projectile.NewProjectile(
                        source_ItemUse,
                        player.Center.X,
                        player.Center.Y,
                        0f,
                        -1f,
                        ModContent.ProjectileType<SeahorseWandPro>(),
                        damage,
                        0f,
                        player.whoAmI
                    );

                    if (Main.projectile.IndexInRange(projIndex))
                    {
                        Main.projectile[projIndex].originalDamage = baseDamage;
                    }
                }
            }
            ModContent.GetInstance<CoralEnchant>().UpdateAccessory(player, hideVisual);
        }

        public class DepthDiverEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DepthDiverEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DepthDiverHelmet>().UpdateArmorSet(player);
            }
        }
        public class DoubloonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DepthDiverEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class SeaHorseEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DepthDiverEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class DepthDiverNewEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DepthDiverEnchant>();

            public override void PostUpdateMiscEffects(Player player)
            {
                if (Main.gameMenu || !player.wet) return;

                float depthFactor = CalculateDepthFactor(player);

                player.lifeRegen += (int)(1 + 1.5f * depthFactor);
                player.GetDamage(DamageClass.Generic) += 0.02f + 0.08f * depthFactor;
                player.statDefense += (int)(2 + 8 * depthFactor);
            }

            private float CalculateDepthFactor(Player player)
            {
                float spaceHeight = (float)(Main.worldSurface * 0.35f * 16f);
                float underworldHeight = (Main.maxTilesY - 200) * 16f;
                float playerY = player.Center.Y;

                float depthFactor = (playerY - spaceHeight) / (underworldHeight - spaceHeight);
                return (float)MathHelper.Clamp(depthFactor, 0f, 1f);
            }
        }
        public override void AddRecipes()
        {


            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DepthDiverHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DepthDiverChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DepthDiverGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CoralEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DrownedDoubloon>());
            recipe.AddIngredient(ModContent.ItemType<BubbleConch>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
