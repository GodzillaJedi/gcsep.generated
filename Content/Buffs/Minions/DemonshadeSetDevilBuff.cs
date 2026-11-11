using CalamityMod.Buffs.Summon;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Calamity.Enchantments;
using gcsep.Content.Projectiles.Enchantments;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs.Minions
{
    public class DemonshadeSetDevilBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonshadeRedDevil>()] > 0)
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

