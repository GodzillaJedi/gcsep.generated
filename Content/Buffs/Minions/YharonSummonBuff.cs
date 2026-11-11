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
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

}
