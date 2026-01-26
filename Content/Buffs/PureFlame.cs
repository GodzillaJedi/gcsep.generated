using Terraria.ModLoader;
using Terraria;

namespace gcsep.Content.Buffs
{
    public class PureFlameBuff : ModBuff
    {
        public override string Texture => "gcsep/Content/Buffs/ChtuxlagorInferno";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = -10; 
        }
    }
}
