using gcsep.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs.Minions
{
    public class AresMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int projType = ModContent.ProjectileType<AresMinion>();

            if (player.ownedProjectileCounts[projType] > 0)
            {
                player.GetModPlayer<GCSEPlayer>().AresSummon = true;
            }

            if (!player.GetModPlayer<GCSEPlayer>().AresSummon)
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
