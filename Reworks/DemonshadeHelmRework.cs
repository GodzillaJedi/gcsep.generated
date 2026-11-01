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
        public override void UpdateArmorSet(Player player, string set)
        {
            if (set == "DemonshadeAddonSet")
            {
                CalamityPlayer calamityPlayer = player.Calamity();

                calamityPlayer.wearingRogueArmor = true;
                calamityPlayer.WearingPostMLSummonerSet = true;

                var gPlayer = player.GetModPlayer<GCSEPlayer>();

                if (player.whoAmI == Main.myPlayer && !calamityPlayer.chibii)
                {
                    gPlayer.rDevil = true;

                    if (!player.HasBuff(ModContent.BuffType<DemonshadeSetDevilBuff>()))
                    {
                        player.AddBuff(ModContent.BuffType<DemonshadeSetDevilBuff>(), 3600);
                    }

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
                            0f, 
                            player.whoAmI
                        );

                        proj.originalDamage = damage;
                    }
                }

                player.maxMinions += SetBonusMinionSlotBoost;
                player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
            }
        }
    }
}
