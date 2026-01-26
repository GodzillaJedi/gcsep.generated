using Terraria.ModLoader;
using Terraria;

namespace gcsep.Content.Buffs
{
    public class MushroomBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.statDefense -= 10;
        }
    }
}
