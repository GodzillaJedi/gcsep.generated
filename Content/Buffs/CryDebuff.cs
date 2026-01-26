using gcsep.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs
{
    public class CryDebuff : ModBuff
    {
        public override string Texture => "gcsep/Content/Buffs/ChtuxlagorInferno";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<CSEThoriumNpcs>().crying = true;
        }
    }
}
