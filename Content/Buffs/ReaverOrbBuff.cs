using CalamityMod;
using CalamityMod.CalPlayer;
using gcsep.Content.Projectiles.Enchantments;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs
{
    public class ReaverOrbBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[base.Type] = true;
            Main.buffNoSave[base.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            CalamityPlayer calamityPlayer = player.Calamity();
            GCSEPlayer gCSEPlayer = player.CSE();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ReaverOrb>()] > 0)
            {
                gCSEPlayer.rOrb = true;
            }
            if (!gCSEPlayer.rOrb)
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
