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
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Keep the buff alive as long as the effect is active
            player.buffTime[buffIndex] = 18000;

            // Mark the orb as active for this tick
            player.CSE().rOrb = true;
        }
    }
}
