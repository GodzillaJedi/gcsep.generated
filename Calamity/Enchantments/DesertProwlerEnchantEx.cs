using CalamityMod;
using CalamityMod.Buffs.Pets;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.DesertProwler;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Pets;
using CalamityMod.Projectiles.Typeless;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.NPCs.Boss;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DesertProwlerEnchantEx : BaseEnchant
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
        public override Color nameColor => new(102, 89, 54);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DesertProwlerEffect>(Item);
            player.AddEffect<LuxorEffect>(Item);
            //player.AddEffect<DimeEffect>(Item);
            player.AddEffect<DesertProwlerCloakEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerHat>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerShirt>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerPants>());
            recipe.AddIngredient(ModContent.ItemType<LuxorsGift>());
            recipe.AddIngredient(ModContent.ItemType<ThiefsDime>());
            recipe.AddIngredient(ModContent.ItemType<CrackshotColt>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class DesertProwlerCloakEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
            private bool ShroudedInSmoke(Player player, out CooldownInstance cd)
            {
                cd = null;
                if (player.Calamity().cooldowns.TryGetValue(SandsmokeBomb.ID, out cd))
                {
                    return cd.timeLeft > DesertProwlerHat.SmokeCooldown;
                }
                return false;
            }
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().wearingRogueArmor = true;
                player.Calamity().rogueStealthMax += DesertProwlerHat.SetBonusRogueStealth;

                DesertProwlerPlayer modPlayer = player.GetModPlayer<DesertProwlerPlayer>();
                modPlayer.desertProwlerSet = true;

                Color color = Color.Lerp(new Color(255, 229, 156), new Color(233, 225, 198), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
                player.setBonus = EffectItem(player).ModItem.GetLocalization("SetBonus").Format(DesertProwlerHat.SetBonusRogueStealth.ToStealth(), color.Hex3(), CalamityUtils.GetArmorSetBonusKey(), DesertProwlerHat.FreeCrit, DesertProwlerHat.BonusDamageCap, DesertProwlerHat.LightsOutReset.FramesToSeconds());

                if (CalamityKeybinds.ArmorSetBonusHotKey.JustPressed && Main.myPlayer == player.whoAmI)
                {
                    if (!player.Calamity().cooldowns.ContainsKey(SandsmokeBomb.ID) && player.HasItem(ModContent.ItemType<StormlionMandible>()))
                    {
                        player.ConsumeItem(ModContent.ItemType<StormlionMandible>());
                        player.AddCooldown(SandsmokeBomb.ID, DesertProwlerHat.SmokeCooldown + DesertProwlerHat.SmokeDuration);
                    }
                }

                if (!ShroudedInSmoke(player, out var cd))
                {
                    return;
                }

                if (cd.timeLeft == DesertProwlerHat.SmokeCooldown + DesertProwlerHat.SmokeDuration)
                {
                    modPlayer.SetBonusStartEffect();
                }

                player.moveSpeed *= DesertProwlerHat.SmokeMoveSpeedMult;
                player.invis = true;
                player.aggro = (int)((float)player.aggro * DesertProwlerHat.SmokeAggroMult);
                player.noKnockback = true;

                for (int i = 0; i < 2; i++)
                {
                    Vector2 vector = Main.rand.NextVector2Circular(80f, 50f);
                    Vector2 position = player.MountedCenter + vector;
                    Vector2 velocity = Main.rand.NextVector2Circular(0.5f, 0.5f) + player.velocity / 8f - Vector2.UnitY.RotatedByRandom(0.78539818525314331) * 0.06f;
                    velocity.X += 1.4f * (float)Math.Sin((vector.X + 80f) / 160f * MathF.PI) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
                    GeneralParticleHandler.SpawnParticle(new SandyDustParticle(position, velocity, Color.White, Main.rand.NextFloat(0.7f, 1.2f), Main.rand.Next(20, 50), 0.03f, Vector2.UnitY * 0.03f));
                }

                int num = Main.rand.Next(2, 3);
                for (int j = 0; j < num; j++)
                {
                    Color newColor = new Color(173, 156, 112);
                    Color newColor2 = new Color(143, 120, 63);
                    if (Main.rand.NextBool())
                    {
                        newColor = new Color(173, 139, 100);
                        newColor2 = new Color(149, 106, 50);
                    }

                    Vector3 hslVector = Main.rgbToHsl(newColor);
                    Vector3 hslVector2 = Main.rgbToHsl(newColor2);
                    float num2 = Main.rand.NextFloat(0f, 0.5f);
                    float num3 = Main.rand.NextFloat(-0.1f, 0f);
                    float amount = Main.rand.NextFloat();
                    hslVector.Z = Math.Clamp(hslVector.Z + num2, 0f, 1f);
                    hslVector2.Z = Math.Clamp(hslVector2.Z + num2, 0f, 1f);
                    hslVector.Y = Math.Clamp(hslVector.Y + num3, 0f, 1f);
                    hslVector2.Y = Math.Clamp(hslVector2.Y + num3, 0f, 1f);
                    hslVector.X = MathHelper.Lerp(hslVector.X, 0.168627456f, amount);
                    hslVector2.X = MathHelper.Lerp(hslVector2.X, 0.168627456f, amount);
                    newColor = Main.hslToRgb(hslVector);
                    newColor2 = Main.hslToRgb(hslVector2);
                    Vector2 vector2 = Main.rand.NextVector2Circular(40f, player.height);
                    Vector2 position2 = player.MountedCenter + vector2;
                    float num4 = MathF.PI - (vector2.X + 40f) / 80f * MathF.PI;
                    Vector2 velocity2 = Main.rand.NextVector2Circular(1f, 0.5f) - Vector2.UnitY * 0.05f + player.velocity * 0.5f + num4.ToRotationVector2() * ((1f - (float)Math.Sin(num4)) * 0.9f + 0.1f) * 1.5f;
                    velocity2.X += (float)Math.Sin((vector2.X + 40f) / 80f * MathF.PI) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
                    GeneralParticleHandler.SpawnParticle(new TimedSmokeParticle(position2, velocity2, newColor, newColor2, Main.rand.NextFloat(0.7f, 1.6f), Main.rand.NextFloat(0.4f, 0.55f), Main.rand.Next(20, 36), 0.01f));
                }

                Vector2 vector3 = Main.rand.NextVector2CircularEdge(1f, 1f);
                float num5 = Main.rand.NextFloat(30f);
                Vector2 position3 = player.MountedCenter + vector3 * num5;
                int type = (Main.rand.NextBool() ? 32 : 31);
                Dust dust = Dust.NewDustPerfect(position3, type);
                dust.noGravity = true;
                dust.fadeIn = 1f;
                Vector2 velocity3 = vector3.RotatedBy(1.5707963705062866) * 0.04f * num5;
                dust.velocity = velocity3;
            }
        }
        public class LuxorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().luxorsGift = true;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<Luxor>()] < 1 && !player.dead)
                {
                    Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<Luxor>(), 0, 0f, player.whoAmI);
                }
            }
        }
        public class DimeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                // Apply buff once, without resetting timer every tick
                if (!player.HasBuff(ModContent.BuffType<GoldieBuff>()))
                    player.AddBuff(ModContent.BuffType<GoldieBuff>(), 2); // 2 ticks, auto-refreshes

                // Proper minion damage scaling
                int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(20);

                // Spawn minion only if missing
                if (player.ownedProjectileCounts[ModContent.ProjectileType<GoldiePet>()] <= 0)
                {
                    Projectile.NewProjectileDirect(
                        player.GetSource_FromThis(),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<GoldiePet>(),
                        damage,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }
    }
}