using CalamityMod;
using CalamityMod.Buffs.Summon;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonshadeStealth : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (!player.HasBuff(ModContent.BuffType<DemonshadeSetDevilBuff>()))
                return;
            player.Calamity().rogueStealthMax += 0.1f;
            player.Calamity().wearingRogueArmor = true;
            player.Calamity().stealthStrikeHalfCost = true;
        }
    }
}
