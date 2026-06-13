using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.Reaver;
using gcsep.Content.Buffs.Minions;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<DemonshadeHelm>() &&
                   body.type == ModContent.ItemType<DemonshadeBreastplate>() &&
                   legs.type == ModContent.ItemType<DemonshadeGreaves>()
                   ? "DemonshadeRedDevil"
                   : "";
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            CalamityPlayer calamityPlayer = player.Calamity();
            calamityPlayer.dsSetBonus = true;
            calamityPlayer.wearingRogueArmor = true;
            calamityPlayer.WearingPostMLSummonerSet = true;

            GCSEPlayer gPlayer = player.GetModPlayer<GCSEPlayer>();

            // Only spawn minion for the local player and if not chibii
            if (player.whoAmI == Main.myPlayer && !calamityPlayer.chibii)
            {
                // Mark minion as active
                gPlayer.rDevil = true;

                // Apply buff if missing
                if (player.FindBuffIndex(ModContent.BuffType<DemonshadeSetDevilBuff>()) == -1)
                    player.AddBuff(ModContent.BuffType<DemonshadeSetDevilBuff>(), 3600);

                // Spawn minion if not already present
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonshadeRedDevil>()] < 1)
                {
                    IEntitySource source = player.GetSource_FromThis();
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(DevilDamage);

                    Projectile.NewProjectileDirect(
                        source,
                        player.Center,
                        -Vector2.UnitY,
                        ModContent.ProjectileType<DemonshadeRedDevil>(),
                        damage,
                        0f,
                        player.whoAmI,
                        2f
                    ).originalDamage = DevilDamage;
                }
            }

            // Apply set bonuses
            player.maxMinions += SetBonusMinionSlotBoost;
            player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
        }
    }

}
