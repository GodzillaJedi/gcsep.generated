using gcsep.Core;
using Terraria;
using Terraria.ModLoader;
using VitalityMod.Content.Projectiles.Summoner.Sentries;

namespace gcsep.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class XenoniteClusterBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int projType = ModContent.ProjectileType<XenoniteClusterProj>();

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