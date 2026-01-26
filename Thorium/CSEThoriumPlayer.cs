using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using ThoriumMod.Projectiles;
using ThoriumMod.Projectiles.Thrower;
using ThoriumMod.Sounds;
using static gcsep.Thorium.Enchantments.IllumiteEnchant;

namespace gcsep.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]

    public partial class CSEThoriumPlayer : ModPlayer
    {
        public bool broadcast;
        public bool SpiritTrapperEnchant;
        public bool ThoriumSoul;
        public bool ThunderTalonEternity;
        public bool DarkenedCloak;
        public bool tripleDamageNextHit;
        public bool illumiteNightVision;
        public bool steelWasDashing;
        public bool[] nobleCoinSeen = new bool[4];
        public bool championsRebuttal;
        public bool setBronze;
        public bool yewCharging;

        public int yewCharge;
        public int yewChargeTimer;
        public int championDamage;
        public int silkManaStacks;
        public int silkManaAccumulator;
        public int silkEffectTimer;
        public int silkPreviousMana;
        public int steelDashCooldown;
        public int steelBuffDuration;
        public int yewArrowCooldown;
        public int healCD;
        public int pyroCooldown;
        public int bronzeSwordTimer;
        public int assassinCooldown;
        public override void ResetEffects()
        {
            SpiritTrapperEnchant = false;
            ThoriumSoul = false;
            tripleDamageNextHit = false;
            if (!Player.HasEffect<IllumiteEffect>())
            {
                illumiteNightVision = false;
            }

        }
        public override void UpdateDead()
        {
            illumiteNightVision = false;
        }
        public override void ModifyHitNPC(Terraria.NPC target, ref Terraria.NPC.HitModifiers modifiers)
        {
            if (tripleDamageNextHit)
            {
                modifiers.FinalDamage *= 3;
                tripleDamageNextHit = false;
            }
        }
        public override void OnHurt(Player.HurtInfo info)
        {
            if (championsRebuttal)
            {
                int bonusDamage = info.Damage * 2;
                championDamage += bonusDamage;

                CombatText.NewText(Player.Hitbox, new Color(255, 100, 50), "+" + bonusDamage, dramatic: false, dot: true);
            }
        }
        public override void OnHitNPC(Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            if (yewCharging && !hit.Crit)
            {
                yewChargeTimer = 120;

                if (Player.ownedProjectileCounts[ModContent.ProjectileType<YewVisual>()] < 1)
                {
                    Terraria.Projectile.NewProjectile(
                        Player.GetSource_OnHit(target),
                        Player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<YewVisual>(),
                        0,
                        0f,
                        Player.whoAmI
                    );
                }

                if (yewCharge < 4)
                {
                    yewCharge++;
                }
            }
        }
        public override void OnHitByProjectile(Terraria.Projectile proj, Player.HurtInfo info)
        {
            if (setBronze &&
                proj.DamageType == DamageClass.Throwing &&
                proj.type != ModContent.ProjectileType<LightStrike>() &&
                proj.type != ModContent.ProjectileType<ThrowingGuideFollowup>() &&
                Main.rand.NextBool(5))
            {
                SoundEngine.PlaySound(ThoriumSounds.ParalyzeSound, proj.Center);

                Terraria.Projectile.NewProjectile(
                    Player.GetSource_OnHit(proj),
                    new Vector2(Player.Center.X, Player.Center.Y - 600f),
                    new Vector2(0f, 15f),
                    ModContent.ProjectileType<LightStrike>(),
                    (int)Player.GetTotalDamage(DamageClass.Throwing).ApplyTo(30f),
                    1f,
                    Player.whoAmI
                );
            }
        }
        public override void PostUpdate()
        {
            if (SpiritTrapperEnchant && Player.ownedProjectileCounts[ModContent.ProjectileType<SpiritTrapperVisual>()] >= 5)
            {
                Player.statLifeMax2 += 10;
                Player.HealEffect(10, true);
                for (int num23 = 0; num23 < 5; num23++)
                {
                    Terraria.Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpiritTrapperVisual>(), 0, 0, Player.whoAmI, (float)num23, 0f);
                }
                for (int num24 = 0; num24 < 1000; num24++)
                {
                    Terraria.Projectile projectile3 = Main.projectile[num24];
                    if (projectile3.active && projectile3.type == ModContent.ProjectileType<SpiritTrapperVisual>())
                    {
                        projectile3.Kill();
                    }
                }
            }
        }

    }    
}