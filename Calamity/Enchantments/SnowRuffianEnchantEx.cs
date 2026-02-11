using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.SnowRuffian;
using CalamityMod.Items.Armor.Sulphurous;
using CalamityMod.Projectiles.Typeless;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class SnowRuffianEnchantEx : BaseEnchant
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

        public override Color nameColor => new Color(160, 185, 213);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SnowRuffianArmorEffect>(Item);
            player.AddEffect<ScuttlerEffect>(Item);
            player.AddEffect<CamperEffect>(Item);
            ModContent.GetInstance<SnowRuffianEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianMask>());
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianChestplate>());
            recipe.AddIngredient(ModContent.ItemType<ScuttlersJewel>());
            recipe.AddIngredient(ModContent.ItemType<TheCamper>());
            recipe.AddIngredient(ModContent.ItemType<SnowRuffianEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class SnowRuffianArmorEffect : AccessoryEffect
        {
            private bool shouldBoost;
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnowRuffianEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SnowRuffianMask>().UpdateArmorSet(player);
            }
        }
        public class SnowRuffianEnchantEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnowRuffianEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.FargoCrossmod.Mod.Find<ModItem>("TitanHeartEnchant").UpdateAccessory(player, true);
            }
        }
        public class ScuttlerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnowRuffianEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().scuttlersJewel = true;
            }
        }
        public class CamperEffect : AccessoryEffect
        {
            private int auraCounter;
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<SnowRuffianEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                IEntitySource source_Accessory = player.GetSource_Accessory(EffectItem(player));
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.camper = true;
                player.AddBuff(89, 60);
                Main.SceneMetrics.HasHeartLantern = true;
                player.AddBuff(87, 60);
                Main.SceneMetrics.HasCampfire = true;
                if (!player.HasBuff(207))
                {
                    player.AddBuff(207, 80);
                }
                else
                {
                    for (int i = 0; i < Player.MaxBuffs; i++)
                    {
                        if (player.buffType[i] == 207 && player.buffTime[i] < 80)
                        {
                            player.buffTime[i] = 80;
                        }
                    }
                }

                Lighting.AddLight(player.Center, 0.825f, 0.66f, 0f);
                if (Main.myPlayer != player.whoAmI)
                {
                    return;
                }

                if (player.StandingStill())
                {
                    player.GetDamage<GenericDamageClass>() += 0.15f;
                    auraCounter++;
                    float num = 200f;
                    if (auraCounter == 9)
                    {
                        auraCounter = 0;
                        ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            NPC current = enumerator.Current;
                            if (current.IsAnEnemy() && !current.dontTakeDamage && Vector2.Distance(player.Center, current.Center) <= num)
                            {
                                int damage = (int)player.GetBestClassDamage().ApplyTo(Main.rand.Next(100, 121));
                                Projectile.NewProjectile(source_Accessory, current.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), damage, 0f, player.whoAmI, current.whoAmI);
                            }
                        }
                    }

                    if (player.HeldItem != null && !player.HeldItem.IsAir && player.HeldItem.stack > 0)
                    {
                        bool num2 = player.HeldItem.CountsAsClass<SummonDamageClass>();
                        bool flag = player.HeldItem.CountsAsClass<ThrowingDamageClass>();
                        bool flag2 = player.HeldItem.CountsAsClass<MeleeDamageClass>();
                        bool flag3 = player.HeldItem.CountsAsClass<RangedDamageClass>();
                        bool flag4 = player.HeldItem.CountsAsClass<MagicDamageClass>();
                        if (num2)
                        {
                            player.GetKnockback<SummonDamageClass>() += 0.1f;
                            player.AddBuff(150, 60);
                        }
                        else if (flag)
                        {
                            calamityPlayer.rogueVelocity += 0.1f;
                        }
                        else if (flag2)
                        {
                            player.GetAttackSpeed<MeleeDamageClass>() += 0.1f;
                            player.AddBuff(159, 60);
                        }
                        else if (flag3)
                        {
                            player.AddBuff(93, 60);
                        }
                        else if (flag4)
                        {
                            player.AddBuff(29, 60);
                        }
                    }
                }
                else
                {
                    auraCounter = 0;
                }
            }
        }
    }
}

