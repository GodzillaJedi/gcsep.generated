using CalamityMod;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles.Minions
{
    public class YharonBossMinion : ModProjectile
    {
        public ref float AttackState => ref Projectile.ai[0];
        public ref float AttackTimer => ref Projectile.ai[1];
        public ref float DashCounter => ref Projectile.ai[2];
        public override string Texture => "CalamityMod/NPCs/Yharon/Yharon";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7; 
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 160;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft = 18000;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6; 
            Projectile.DamageType = DamageClass.Summon;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            var gPlayer = player.GetModPlayer<GCSEPlayer>();

            if (!gPlayer.YharonSummon || player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;

            NPC target = Projectile.Center.MinionHoming(2800f, player);

            if (target != null)
            {
                Vector2 toTarget = target.Center - Projectile.Center;
                float distance = toTarget.Length();
                toTarget.Normalize();

                AttackTimer++;

                switch ((int)AttackState)
                {
                    case 0: 
                        Vector2 hoverSpot = target.Center + new Vector2(player.direction * -450f, -250f);
                        Projectile.velocity = (Projectile.velocity * 15f + (hoverSpot - Projectile.Center) * 0.15f) / 16f;

                        if (AttackTimer % 12 == 0 && Main.myPlayer == Projectile.owner)
                        {
                            SoundEngine.PlaySound(in Yharon.FireSound, Projectile.Center);

                            int bomb = Projectile.NewProjectile(
                                Projectile.GetSource_FromThis(),
                                Projectile.Center,
                                toTarget * 15f,
                                ModContent.ProjectileType<FlareBomb>(),
                                Projectile.damage,
                                0f,
                                Projectile.owner,
                                target.whoAmI
                            );
                            if (bomb.WithinBounds(Main.maxProjectiles))
                            {
                                Main.projectile[bomb].hostile = false; 
                                Main.projectile[bomb].friendly = true;
                                Main.projectile[bomb].DamageType = DamageClass.Summon;
                            }
                        }

                        
                        if (AttackTimer == 150f)
                        {
                            SoundEngine.PlaySound(in Yharon.ShortRoarSound, Projectile.Center);
                        }

                        if (AttackTimer >= 180f)
                        {
                            AttackState = 1f; 
                            AttackTimer = 0f;
                            Projectile.velocity = toTarget * 30f; 
                            SoundEngine.PlaySound(in Yharon.RoarSound, Projectile.Center); 
                            Projectile.netUpdate = true;
                        }

                        AnimateWDragon(0, 4, 5);
                        break;

                    case 1: 
                        if (Main.rand.NextBool(2))
                        {
                            int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0f, 0f, 100, default, 1.8f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].velocity *= 1.5f;
                        }

                        if (AttackTimer >= 22f)
                        {
                            AttackTimer = 0f;
                            DashCounter++;
                            if (DashCounter >= 3f) 
                            {
                                AttackState = 2f; 
                                DashCounter = 0f;
                            }
                            else
                            {
                                Projectile.velocity = toTarget * 30f; 
                            }
                            Projectile.netUpdate = true;
                        }

                        Projectile.frame = 5;
                        break;

                    case 2: 
                        Projectile.velocity *= 0.82f; 

                        if (AttackTimer >= 30f && AttackTimer < 60f)
                        {
                            Projectile.frame = 6;
                        }
                        else
                        {
                            AnimateWDragon(0, 4, 5);
                        }

                        Vector2 circularOffset = Vector2.UnitX.RotatedByRandom(MathHelper.TwoPi) * Main.rand.NextFloat(50f, 100f);
                        Dust dRing = Dust.NewDustDirect(Projectile.Center + circularOffset, 0, 0, DustID.SolarFlare);
                        dRing.noGravity = true;
                        dRing.velocity = -circularOffset.SafeNormalize(Vector2.Zero) * 5f;

                        if (AttackTimer >= 90f) 
                        {
                            if (Main.myPlayer == Projectile.owner)
                            {
                                SoundEngine.PlaySound(in Yharon.OrbSound, Projectile.Center);

                                int flarenadoID = ModContent.ProjectileType<Flare>();

                                int flarenadoIndex = Projectile.NewProjectile(
                                    Projectile.GetSource_FromThis(),
                                    target.Center.X,
                                    target.Center.Y,
                                    player.direction * 4f,
                                    8f,
                                    flarenadoID,
                                    Projectile.damage * 2,
                                    0f,
                                    Projectile.owner
                                );

                                if (flarenadoIndex.WithinBounds(Main.maxProjectiles))
                                {
                                    Main.projectile[flarenadoIndex].hostile = false; 
                                    Main.projectile[flarenadoIndex].friendly = true;
                                    Main.projectile[flarenadoIndex].DamageType = DamageClass.Summon;
                                }
                            }

                            AttackState = 0f;
                            AttackTimer = 0f;
                            Projectile.netUpdate = true;
                        }
                        break;
                }
            }
            else 
            {
                AttackState = 0f;
                AttackTimer = 0f;
                DashCounter = 0f;
                double phaseAngle = Main.GlobalTimeWrappedHourly * 2.8f;
                Vector2 idleSpot = player.Center + new Vector2((float)Math.Sin(phaseAngle) * 220f, -160f);
                Vector2 displacement = idleSpot - Projectile.Center;
                if (displacement.Length() > 2200f)
                    Projectile.Center = player.Center;
                Projectile.velocity = (Projectile.velocity * 20f + displacement * 0.12f) / 21f;
                AnimateWDragon(0, 4, 6);
            }
            if (Projectile.velocity.X > 0.6f)
                Projectile.spriteDirection = 1; 
            else if (Projectile.velocity.X < -0.6f)
                Projectile.spriteDirection = -1;
            Projectile.rotation = Projectile.velocity.X * 0.025f;
            Lighting.AddLight(Projectile.Center, 1.4f, 0.5f, 0.15f); 
        }
        private void AnimateWDragon(int minFrame, int maxFrame, int tickSpeed)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= tickSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > maxFrame || Projectile.frame < minFrame)
                    Projectile.frame = minFrame;
            }
        }
        public override bool? CanDamage() => AttackState == 1f;
    }
}