using System;
using CalamityMod;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles.Enchantments
{
    public class DemonshadeRedDevil : ModProjectile
    {
        public ref float State => ref Projectile.ai[0];
        public ref float Timer => ref Projectile.ai[1];

        private int spawnDustTimer = 12; // small burst on spawn

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft = 18000;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            GCSEPlayer gPlayer = player.GetModPlayer<GCSEPlayer>();

            // ❗ Minion should only exist while rDevil is true
            if (!gPlayer.rDevil)
            {
                Projectile.active = false;
                return;
            }

            // Keep minion alive
            if (player.dead)
                gPlayer.rDevil = false;

            if (gPlayer.rDevil)
                Projectile.timeLeft = 2;

            // -----------------------------------------
            // SPAWN DUST (fixed, optimized)
            // -----------------------------------------
            if (spawnDustTimer > 0)
            {
                spawnDustTimer--;

                int amount = Main.rand.Next(3, 7);
                for (int i = 0; i < amount; i++)
                {
                    int d = Dust.NewDust(
                        Projectile.position + new Vector2(0f, 16f),
                        Projectile.width,
                        Projectile.height - 16,
                        235,
                        Main.rand.NextFloat(-1.5f, 1.5f),
                        Main.rand.NextFloat(-1.5f, 1.5f),
                        150,
                        default,
                        Main.rand.NextFloat(1.0f, 1.4f)
                    );

                    Dust dustRef = Main.dust[d];
                    dustRef.noGravity = true;
                    dustRef.velocity *= 1.5f;
                    dustRef.fadeIn = 0.5f;
                }
            }

            // -----------------------------------------
            // ANIMATION
            // -----------------------------------------
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame > 4)
                    Projectile.frame = 0;
            }

            // -----------------------------------------
            // LIGHT
            // -----------------------------------------
            float lightScale = Main.rand.NextFloat(0.90f, 1.10f) * Main.essScale;
            Lighting.AddLight(Projectile.Center, 1f * lightScale, 0f, 0.15f * lightScale);

            // -----------------------------------------
            // TARGET ACQUISITION
            // -----------------------------------------
            Vector2 targetPos = Projectile.position;
            float targetDist = 2000f;
            bool foundTarget = false;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC n = Main.npc[player.MinionAttackTargetNPC];
                if (n.CanBeChasedBy(Projectile))
                {
                    float dist = Vector2.Distance(n.Center, Projectile.Center);
                    if (dist < targetDist)
                    {
                        targetDist = dist;
                        targetPos = n.Center;
                        foundTarget = true;
                    }
                }
            }
            else
            {
                foreach (NPC n in Main.ActiveNPCs)
                {
                    if (n.CanBeChasedBy(Projectile))
                    {
                        float dist = Vector2.Distance(n.Center, Projectile.Center);
                        if (dist < targetDist && Collision.CanHitLine(Projectile.Center, 1, 1, n.Center, 1, 1))
                        {
                            targetDist = dist;
                            targetPos = n.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // -----------------------------------------
            // RETURN TO PLAYER IF TOO FAR
            // -----------------------------------------
            float maxRange = foundTarget ? 3000f : 2000f;
            if (Vector2.Distance(player.Center, Projectile.Center) > maxRange)
            {
                State = 1f;
                Projectile.netUpdate = true;
            }

            // -----------------------------------------
            // MOVEMENT
            // -----------------------------------------
            if (foundTarget && State == 0f)
            {
                Vector2 dir = targetPos - Projectile.Center;
                float dist = dir.Length();
                dir.Normalize();

                if (dist > 200f)
                    dir *= 25f;
                else
                    dir *= -9f;

                Projectile.velocity = (Projectile.velocity * 40f + dir) / 41f;
            }
            else
            {
                bool returning = State == 1f;
                float speed = returning ? 24f : 15f;

                Vector2 dir = player.Center - Projectile.Center + new Vector2(0f, -30f);
                float dist = dir.Length();

                if (dist < 150f && returning && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    State = 0f;
                    Projectile.netUpdate = true;
                }

                if (dist > 2000f)
                {
                    Projectile.Center = player.Center;
                    Projectile.netUpdate = true;
                }

                if (dist > 70f)
                {
                    dir.Normalize();
                    dir *= speed;
                    Projectile.velocity = (Projectile.velocity * 40f + dir) / 41f;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    Projectile.velocity = new Vector2(-0.2f, -0.1f);
                }
            }

            // -----------------------------------------
            // ATTACK LOGIC
            // -----------------------------------------
            if (Timer > 0f)
                Timer += Main.rand.Next(1, 3);

            if (Timer > 80f)
            {
                Timer = 0f;
                Projectile.netUpdate = true;
            }

            if (State == 0f && foundTarget && Timer == 0f)
            {
                Timer++;

                if (Main.myPlayer == Projectile.owner &&
                    Collision.CanHitLine(Projectile.Center, 1, 1, targetPos, 1, 1))
                {
                    Vector2 vel = Vector2.Normalize(targetPos - Projectile.Center) * 24f;

                    int p = Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.Center,
                        vel,
                        114,
                        Projectile.damage,
                        0f,
                        Projectile.owner
                    );

                    if (p.WithinBounds(Main.maxProjectiles))
                    {
                        Main.projectile[p].timeLeft = 300;
                        Main.projectile[p].usesLocalNPCImmunity = true;
                        Main.projectile[p].localNPCHitCooldown = 10;
                        Main.projectile[p].DamageType = DamageClass.Generic;
                    }

                    Projectile.netUpdate = true;
                }
            }
        }

        public override bool? CanDamage() => false;
    }

}
