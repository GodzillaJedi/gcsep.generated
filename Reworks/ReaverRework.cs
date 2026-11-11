using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Armor.Reaver;
using gcsep.Content.Buffs;
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
    public class ReaverRework : GlobalItem
    {
        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<ReaverHeadExplore>() && body.type == ModContent.ItemType<ReaverScaleMail>() && legs.type == ModContent.ItemType<ReaverCuisses>() ? "ReaverOrb" : "";
        }
        public static class ReaverOrbHelper
        {
            public static void EnsureOrb(Player player, IEntitySource source)
            {
                if (player.whoAmI != Main.myPlayer || player.dead)
                    return;

                int orbType = ModContent.ProjectileType<ReaverOrb>();
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == player.whoAmI && p.type == orbType)
                        return;
                }

                Projectile.NewProjectile(source, player.Center, Vector2.Zero, orbType, 0, 0f, player.whoAmI);
            }
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            CalamityPlayer calamity = player.Calamity();
            calamity.reaverExplore = true;   
            calamity.wearingRogueArmor = true;

            player.findTreasure = true;
            player.blockRange += 4;
            player.aggro -= 200;

            if (player.whoAmI == Main.myPlayer)
            {
                int buffType = ModContent.BuffType<ReaverOrbBuff>();
                if (player.FindBuffIndex(buffType) == -1)
                    player.AddBuff(buffType, 3600);

                ReaverOrbHelper.EnsureOrb(player, player.GetSource_FromThis());
            }
        }
    }

}
