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
                IEntitySource source_ItemUse = player.GetSource_FromThis();
                if (player.FindBuffIndex(ModContent.BuffType<ReaverOrbBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<ReaverOrbBuff>(), 3600);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<ReaverOrb>()] < 1)
                {
                    Projectile.NewProjectile(source_ItemUse, player.Center, Vector2.Zero, ModContent.ProjectileType<ReaverOrb>(), 0, 0f, player.whoAmI);
                }
            }
        }
    }
}
