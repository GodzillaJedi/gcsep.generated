using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs
{
    public class FearBuff : ModBuff
    {
        public override string Texture => "gcsep/Content/Buffs/ChtuxlagorInferno";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }
    }
}
