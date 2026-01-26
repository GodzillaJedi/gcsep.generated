using Terraria.ModLoader;
using Terraria;

namespace gcsep.Content.Buffs
{
    public class SniperBuff : ModBuff
    {
        public override string Texture => "gcsep/Content/Buffs/ChtuxlagorInferno";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }
    }

    public class SniperCooldownBuff : ModBuff
    {
        public override string Texture => "gcsep/Content/Buffs/ChtuxlagorInferno";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}
