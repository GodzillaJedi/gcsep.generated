using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Empowerments;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossBoreanStrider;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.ThoriumEnchant;
using static gcsep.Thorium.Forces.MuspelheimForce;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CyberPunkEnchant : BaseEnchant
    {
        public static readonly int SetEmpowermentLevel = 2;
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
            Item.rare = 6;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CyberPunkEffect>(Item);
            player.AddEffect<CyberPunkHelmEffect>(Item);
            if (player.AddEffect<AutoTunerEffect>(Item))
            {
                ModContent.GetInstance<AutoTuner>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DissEffect>(Item))
            {
                ModContent.GetInstance<DissTrack>().UpdateAccessory(player, hideVisual);
            }
        }
        public class CyberPunkHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyberPunkEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override bool MinionEffect => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CyberPunkHeadset>().UpdateArmorSet(player);
            }
        }
        public class AutoTunerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyberPunkEnchant>();
            public override bool MutantsPresenceAffects => true;

        }
        public class DissEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyberPunkEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class CyberPunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CyberPunkEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override bool MinionEffect => true;

            public override void PostUpdateEquips(Player player)
            {
                if (Main.gameMenu || !player.active) return;

                int cyberOrbType = ModContent.NPCType<CyberneticOrb>();
                int count = 0;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.type == cyberOrbType && npc.ai[0] == player.whoAmI)
                        count++;
                }

                int multiplier = 2;
                if (player.HasEffect<ThoriumEffect>()) multiplier = 4;

                if (count < multiplier)
                {
                    if (Main.netMode == NetmodeID.SinglePlayer)
                    {
                        NPC.NewNPC(
                            NPC.GetBossSpawnSource(player.whoAmI),
                            (int)player.Center.X,
                            (int)player.Center.Y,
                            cyberOrbType,
                            0,
                            player.whoAmI,
                            0f,
                            multiplier
                        );
                    }
                    else if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        var netMessage = gcsep.Instance.GetPacket();
                        netMessage.Write((byte)gcsep.PacketID.RequestCyberOrbs);
                        netMessage.Write((byte)player.whoAmI);
                        netMessage.Write((byte)multiplier);
                        netMessage.Send();
                    }
                }
            }
        }
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
        public class CyberneticOrb : ModNPC
        {
            public override string Texture => "ThoriumMod/Projectiles/Healer/TemplarJudgmentPro";
            public override void SetStaticDefaults()
            {
                NPCID.Sets.CantTakeLunchMoney[Type] = true;
                NPCID.Sets.ImmuneToAllBuffs[Type] = true;
                this.ExcludeFromBestiary();
            }

            public override void SetDefaults()
            {
                NPC.width = 30;
                NPC.height = 30;
                NPC.damage = 20;
                NPC.defense = 0;
                NPC.lifeMax = 30;
                NPC.friendly = true;
                NPC.dontCountMe = true;
                NPC.netAlways = true;
                NPC.HitSound = SoundID.NPCHit9;
                NPC.DeathSound = SoundID.NPCDeath11;
                NPC.noGravity = true;
                NPC.noTileCollide = true;
                NPC.knockBackResist = 0.8f;
                NPC.lavaImmune = true;
                NPC.aiStyle = -1;
            }

            public override void AI()
            {
                const float Radius = 64f;
                const float BaseAngularSpeed = 0.05f;

                int ownerIndex = (int)NPC.ai[0];
                if (ownerIndex < 0 || ownerIndex >= Main.maxPlayers) return;

                Player player = Main.player[ownerIndex];
                if (!player.active || player.dead || !player.HasEffect<CyberPunkEffect>())
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    return;
                }

                List<NPC> minions = new List<NPC>();
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC n = Main.npc[i];
                    if (n.active && n.type == NPC.type && n.ai[0] == NPC.ai[0])
                        minions.Add(n);
                }

                minions.Sort((a, b) => a.whoAmI.CompareTo(b.whoAmI));
                int minionIndex = minions.IndexOf(NPC);
                int minionCount = minions.Count;

                Vector2 toPlayer = NPC.Center - player.Center;
                if (toPlayer.Length() > 1000f)
                {
                    PlaceMinionAtOrbitPosition(player, minionIndex, minionCount);
                    NPC.velocity = Vector2.Zero;
                    return;
                }

                float angleOffset = (MathHelper.TwoPi / minionCount) * minionIndex;
                float globalRotation = (float)Main.timeForVisualEffects * BaseAngularSpeed;
                float targetAngle = globalRotation + angleOffset;

                Vector2 targetPosition = player.Center + new Vector2(
                    (float)Math.Cos(targetAngle) * Radius,
                    (float)Math.Sin(targetAngle) * Radius
                );

                Vector2 direction = targetPosition - NPC.Center;
                if (direction.Length() > 10f)
                {
                    direction.Normalize();
                    NPC.velocity = (NPC.velocity * 15f + direction * 16f) / 16f;
                }
                else
                {
                    NPC.velocity *= 0.95f;
                }

                const float RepelStrength = 0.1f;
                foreach (NPC other in minions)
                {
                    if (other.whoAmI == NPC.whoAmI) continue;

                    Vector2 repel = NPC.DirectionFrom(other.Center);
                    float distanceToOther = Vector2.Distance(NPC.Center, other.Center);

                    if (distanceToOther < NPC.width)
                    {
                        float repelFactor = MathHelper.Clamp(1f - distanceToOther / NPC.width, 0f, 1f);
                        NPC.velocity += repel * RepelStrength * repelFactor;
                    }
                }
            }

            private void PlaceMinionAtOrbitPosition(Player player, int index, int count)
            {
                if (count == 0) return;

                float angle = (MathHelper.TwoPi / count) * index;
                Vector2 targetPosition = player.Center + new Vector2(
                    (float)Math.Cos(angle) * 64f,
                    (float)Math.Sin(angle) * 64f
                );

                NPC.Center = targetPosition;
            }

            //public override void FindFrame(int frameHeight)
            //{
            //    if (NPC.ai[2] <= 1)
            //        NPC.frame.Y = 0;
            //    else if (NPC.ai[2] <= 2)
            //        NPC.frame.Y = frameHeight;
            //    else
            //        NPC.frame.Y = frameHeight * 2;
            //}
            public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
            {
                modifiers.FinalDamage *= 2;
            }
            public override bool? CanBeHitByProjectile(Projectile projectile)
            {
                switch (projectile.type)
                {
                    case ProjectileID.RottenEgg:
                        return false;

                    case ProjectileID.AshBallFalling:
                    case ProjectileID.CrimsandBallFalling:
                    case ProjectileID.DirtBall:
                    case ProjectileID.EbonsandBallFalling:
                    case ProjectileID.MudBall:
                    case ProjectileID.PearlSandBallFalling:
                    case ProjectileID.SandBallFalling:
                    case ProjectileID.SiltBall:
                    case ProjectileID.SlushBall:
                        if (projectile.velocity.X == 0)
                            return false;
                        break;

                    default:
                        break;
                }

                return null;
            }
            public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
            {
                if (FargoSoulsUtil.CanDeleteProjectile(projectile))
                {
                    projectile.timeLeft = 0;
                    projectile.FargoSouls().canHurt = false;
                }
            }

            public override void HitEffect(NPC.HitInfo hit)
            {
                int heart = NPC.ai[2] > 1 ? 1 : 0;

                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<CyberHitbox>(), NPC.damage, 6f, (int)NPC.ai[0], heart);

                if (NPC.life <= 0)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric);
                        Main.dust[d].velocity *= 2.5f;
                        Main.dust[d].scale += 0.5f;
                    }
                }
            }

            public override bool CheckActive() => false;

            public override bool PreKill() => false;
        }

        public class CyberHitbox : ModProjectile
        {
            public override string Texture => FargoSoulsUtil.EmptyTexture;

            public override void SetDefaults()
            {
                Projectile.width = 60;
                Projectile.height = 60;
                Projectile.aiStyle = -1;
                Projectile.friendly = true;
                Projectile.penetrate = 1;
                Projectile.timeLeft = 1;
                Projectile.tileCollide = false;
                Projectile.ignoreWater = true;
                Projectile.extraUpdates = 1;
                Projectile.hide = true;

                Projectile.usesLocalNPCImmunity = true;
                Projectile.localNPCHitCooldown = 0;
            }

            public override bool? CanDamage() => true;

            public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
            {
                if (target == null) return;

                modifiers.HitDirectionOverride = Main.player[Projectile.owner].Center.X > target.Center.X ? -1 : 1;
                target.AddBuff(BuffID.Confused, 600);
                target.AddBuff(BuffID.Electrified, 600);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CyberPunkHeadset>());
            recipe.AddIngredient(ModContent.ItemType<CyberPunkSuit>());
            recipe.AddIngredient(ModContent.ItemType<CyberPunkLeggings>());
            recipe.AddIngredient(ModContent.ItemType<AutoTuner>());
            recipe.AddIngredient(ModContent.ItemType<DissTrack>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
