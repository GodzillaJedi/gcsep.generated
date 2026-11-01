using gcsep.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs.Minions
{
    public class YharonSummonBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int projType = ModContent.ProjectileType<YharonBossMinion>();

            if (player.ownedProjectileCounts[projType] > 0)
            {
                player.GetModPlayer<GCSEPlayer>().YharonSummon = true;
            }

            if (!player.GetModPlayer<GCSEPlayer>().YharonSummon)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}
