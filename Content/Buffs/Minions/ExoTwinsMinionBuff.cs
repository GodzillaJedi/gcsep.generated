using Terraria;
using Terraria.ModLoader;
using gcsep.Content.Projectiles.Minions;

namespace gcsep.Content.Buffs.Minions
{
    public class ExoTwinsMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int projType = ModContent.ProjectileType<ExoTwinsMinion>();

            if (player.ownedProjectileCounts[projType] > 0)
            {
                player.GetModPlayer<GCSEPlayer>().TwinsSummon = true;
            }

            if (!player.GetModPlayer<GCSEPlayer>().TwinsSummon)
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
