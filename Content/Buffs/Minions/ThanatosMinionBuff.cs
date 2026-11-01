using Terraria;
using Terraria.ModLoader;
using gcsep.Content.Projectiles;

namespace gcsep.Content.Buffs.Minions
{
    public class ThanatosMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int projType = ModContent.ProjectileType<ThanatosMinion>();

            if (player.ownedProjectileCounts[projType] > 0)
            {
                player.GetModPlayer<GCSEPlayer>().ThanatosSummon = true;
            }

            if (!player.GetModPlayer<GCSEPlayer>().ThanatosSummon)
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
