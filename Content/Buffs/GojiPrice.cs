using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs
{
    public class GojiPrice : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[base.Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen = 0;
            player.endurance = 0f;
        }
    }
}
