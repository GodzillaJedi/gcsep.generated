using FargowiltasSouls;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.NPCs.MutantEX.HitPlayer
{
    internal class MonstrGlobalProjectile : GlobalProjectile
    {
        private void ApplyHealthReduction(Player player, float percent)
        {
            var modPlayer = player.GetModPlayer<MonstrHealthPlayer>();

            // Store original max life once
            if (modPlayer.OriginalMaxLife <= 0)
                modPlayer.OriginalMaxLife = player.statLifeMax2;

            // Calculate reduction
            int damage = (int)(modPlayer.OriginalMaxLife * percent);
            if (damage < 1) damage = 1;

            // Apply reduction to our own variable
            modPlayer.HealthReduction += damage;

            // Clamp so HP never becomes 0
            if (modPlayer.HealthReduction >= modPlayer.OriginalMaxLife - 1)
                modPlayer.HealthReduction = modPlayer.OriginalMaxLife - 1;

            // Apply to current HP
            int newMax = modPlayer.OriginalMaxLife - modPlayer.HealthReduction;
            if (player.statLife > newMax)
                player.statLife = newMax;

            // Play hit sound
            SoundEngine.PlaySound(SoundID.NPCHit18, player.Center);

            // Sync multiplayer
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                modPlayer.SyncData();
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncPlayer, -1, -1, null, player.whoAmI);
            }
        }

        public override void AI(Projectile projectile)
        {
            // Only hostile projectiles matter
            if (!projectile.hostile)
                return;

            // Only check once per projectile
            if (!FargoSoulsUtil.BossIsAlive(ref GCSENpcs.mutantEX, ModContent.NPCType<MutantEX>()) &&
                !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()))
            {
                base.AI(projectile);
                return;
            }

            foreach (Player player in Main.player)
            {
                if (!player.active || player.dead)
                    continue;

                var modPlayer = player.GetModPlayer<MonstrHealthPlayer>();

                // iFrames prevent double hits
                if (modPlayer.iFrames > 0)
                    continue;

                if (projectile.Hitbox.Intersects(player.Hitbox))
                {
                    // MutantEX hit
                    if (FargoSoulsUtil.BossIsAlive(ref GCSENpcs.mutantEX, ModContent.NPCType<MutantEX>()))
                    {
                        ApplyHealthReduction(player, WorldSavingSystem.MasochistModeReal ? 0.15f : 0.10f);
                    }

                    // MutantBoss hit
                    if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<MutantBoss>()))
                    {
                        ApplyHealthReduction(player, WorldSavingSystem.MasochistModeReal ? 0.10f : 0.05f);
                    }

                    modPlayer.iFrames = 20;
                    projectile.Kill();
                    return;
                }
            }

            base.AI(projectile);
        }
    }
}
