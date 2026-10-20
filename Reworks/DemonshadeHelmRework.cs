using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Demonshade;
using gcsep.Content.Buffs.Minions;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace gcsep.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeHelmRework : GlobalItem
    {
        public static int SetBonusMinionSlotBoost = 8;
        public static float SetBonusSummonDamageBoost = 1f;
        public static int DevilDamage = 1000;

        // 1. CRITICAL FIX: You must define IsArmorSet or UpdateArmorSet will NEVER run!
        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<DemonshadeHelm>() &&
                body.type == ModContent.ItemType<DemonshadeBreastplate>() &&
                legs.type == ModContent.ItemType<DemonshadeGreaves>())
            {
                return "DemonshadeAddonSet"; // Pass a matching flag to trigger the update loop
            }
            return "";
        }

        // 2. This method is executed naturally by tModLoader once the string registry returns true
        public override void UpdateArmorSet(Player player, string set)
        {
            if (set == "DemonshadeAddonSet")
            {
                CalamityPlayer calamityPlayer = player.Calamity();

                // Keep baseline background trackers established
                calamityPlayer.wearingRogueArmor = true;
                calamityPlayer.WearingPostMLSummonerSet = true;

                // Grab your custom addon tracking player
                var gPlayer = player.GetModPlayer<GCSEPlayer>();

                // Only spawn minion for the local player and if not tiny/chibii
                if (player.whoAmI == Main.myPlayer && !calamityPlayer.chibii)
                {
                    // Mark minion as active
                    gPlayer.rDevil = true;

                    // Apply buff if missing safely
                    if (!player.HasBuff(ModContent.BuffType<DemonshadeSetDevilBuff>()))
                    {
                        player.AddBuff(ModContent.BuffType<DemonshadeSetDevilBuff>(), 3600);
                    }

                    // Spawn the legendary Red Devil minion if it is missing from world tracking tables
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonshadeRedDevil>()] < 1)
                    {
                        IEntitySource source = player.GetSource_FromThis();
                        int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(DevilDamage);

                        var proj = Projectile.NewProjectileDirect(
                            source,
                            player.Center,
                            -Vector2.UnitY,
                            ModContent.ProjectileType<DemonshadeRedDevil>(),
                            damage,
                            0f, // FIXED: Set to 0f so it starts in standard targeting state
                            player.whoAmI
                        );

                        proj.originalDamage = damage;
                    }
                }

                // Apply your custom throwback buff scaling attributes safely
                player.maxMinions += SetBonusMinionSlotBoost;
                player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
            }
        }
    }
}
