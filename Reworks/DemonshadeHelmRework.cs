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

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ModContent.ItemType<DemonshadeHelm>() &&
                body.type == ModContent.ItemType<DemonshadeBreastplate>() &&
                legs.type == ModContent.ItemType<DemonshadeGreaves>())
            {
                return "DemonshadeAddonSet";
            }
            return "";
        }
        public static class DemonshadeDevilHelper
        {
            public static void EnsureDevil(Player player, IEntitySource source, int baseDamage)
            {
                if (player.whoAmI != Main.myPlayer || player.dead)
                    return;

                int devilType = ModContent.ProjectileType<DemonshadeRedDevil>();
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == player.whoAmI && p.type == devilType)
                        return;
                }

                int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);
                Projectile proj = Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, devilType, damage, 0f, player.whoAmI);
                proj.originalDamage = damage;
            }
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            if (set != "DemonshadeAddonSet")
                return;

            CalamityPlayer calamityPlayer = player.Calamity();

            calamityPlayer.wearingRogueArmor = true;
            calamityPlayer.WearingPostMLSummonerSet = true;

            if (!calamityPlayer.chibii)
            {
                player.GetModPlayer<GCSEPlayer>().rDevil = true;

                // Only the owner adds the buff and spawns the minion
                if (player.whoAmI == Main.myPlayer)
                {
                    int buffType = ModContent.BuffType<DemonshadeSetDevilBuff>();
                    if (!player.HasBuff(buffType))
                        player.AddBuff(buffType, 3600);

                    DemonshadeDevilHelper.EnsureDevil(player, player.GetSource_FromThis(), DevilDamage);
                }
            }

            player.maxMinions += SetBonusMinionSlotBoost;
            player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
        }
    }
}