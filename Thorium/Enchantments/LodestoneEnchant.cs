using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Lodestone;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Projectiles;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LodestoneEnchant : BaseEnchant
    {
        public static readonly int SetDamageReduction = 6;
        public static readonly int SetLifePercentageInterval = 25;
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
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<AstroBeetleEffect>(Item))
            {
                ThoriumPlayer thoriumPlayer = player.GetThoriumPlayer();
                thoriumPlayer.thoriumEndurance += 0.05f;
                thoriumPlayer.MetalShieldMax = 20;
            }
            if (player.AddEffect<ObsidianScaleEffect>(Item))
            {
                player.fireWalk = true;
                player.GetThoriumPlayer().accReducedKnockback = true;

                int debuffType = 323;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (!npc.CanBeChasedBy() || player.DistanceSQ(npc.Center) >= 30625f)
                        continue;

                    if (!npc.wet && !npc.buffImmune[debuffType] && !npc.HasBuff(debuffType))
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, 174, 0f, 0f, 125, default, 1.35f);
                            Dust dust = Main.dust[dustIndex];
                            dust.noGravity = true;
                            dust.velocity *= 0.75f;

                            int offsetX = Main.rand.Next(-50, 51);
                            int offsetY = Main.rand.Next(-50, 51);
                            dust.position.X += offsetX;
                            dust.position.Y += offsetY;
                            dust.velocity.X = -offsetX * 0.075f;
                            dust.velocity.Y = -offsetY * 0.075f;
                        }
                    }

                    npc.AddBuff(debuffType, 30);
                }
            }
            if (player.AddEffect<SandweaverEffect>(Item))
            {
                var thoriumPlayer = player.GetThoriumPlayer();
                thoriumPlayer.accSandweaversTiara.Set(base.Item);

                int projType = ModContent.ProjectileType<SandweaversTiaraPro>();

                if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] < 1)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_Accessory(thoriumPlayer.accSandweaversTiara.Item),
                            player.Center,
                            Vector2.Zero,
                            projType,
                            50,
                            8f,
                            player.whoAmI,
                            i
                        );
                    }
                }
            }
            if (player.AddEffect<LodestoneEffect>(Item))
            {
                ThoriumPlayer thoriumPlayer = player.GetThoriumPlayer();

                // Prevent division by zero
                float maxLife = Math.Max(player.statLifeMax2, 1);

                // Convert interval to a fraction safely
                float intervalFraction = SetLifePercentageInterval / 100f;
                intervalFraction = Math.Max(intervalFraction, 0.01f); // never allow 0

                // Total number of stages
                int stages = (int)(1f / intervalFraction);

                // Current stage based on missing life
                float lifePercent = player.statLife / maxLife;
                int currentStage = (int)(lifePercent / intervalFraction);

                // Clamp to valid range
                currentStage = Math.Clamp(currentStage, 0, stages);

                // Missing stages = damage reduction bonus
                int missingStages = stages - currentStage;

                thoriumPlayer.thoriumEndurance += missingStages * (SetDamageReduction / 100f);
                thoriumPlayer.lodestoneStage = missingStages;
                thoriumPlayer.orbital = true;
                thoriumPlayer.orbitalRotation3 = thoriumPlayer.orbitalRotation3.RotatedBy(-0.05);
            }
        }
        public class SandweaverEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LodestoneEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class AstroBeetleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LodestoneEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ObsidianScaleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LodestoneEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class LodestoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LodestoneEnchant>();
            public override bool MutantsPresenceAffects => true;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LodeStoneFaceGuard>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneShinGuards>());
            recipe.AddIngredient(ModContent.ItemType<LodeStoneClaymore>()); //astrobettle husk
            recipe.AddIngredient(ModContent.ItemType<AstroBeetleHusk>());
            recipe.AddIngredient(ModContent.ItemType<ObsidianScale>());
            recipe.AddIngredient(ModContent.ItemType<SandweaversTiara>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
