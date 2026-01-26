using CalamityMod.Buffs.StatDebuffs;
using gcsep.Core;
using SacredTools.NPCs.Boss.Obelisk.Nihilus;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class SoACalNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void SetStaticDefaults()
        {
            NPCID.Sets.SpecificDebuffImmunity[ModContent.NPCType<Nihilus2>()][ModContent.BuffType<Enraged>()] = true;
            NPCID.Sets.SpecificDebuffImmunity[ModContent.NPCType<Nihilus>()][ModContent.BuffType<Enraged>()] = true;
        }
    }
}
