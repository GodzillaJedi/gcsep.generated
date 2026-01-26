using Terraria.ModLoader;
using Terraria;
using gcsep.Core;
using FargowiltasSouls;

namespace gcsep.Content.Buffs
{
    //[JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    //[ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class NihilityPresenceBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.FargoSouls().MutantPresence = true;
        }
    }
}
