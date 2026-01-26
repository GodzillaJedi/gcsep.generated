using FargowiltasSouls;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using gcsep.Core;
using SacredTools.Items.Placeable;
using SacredTools.NPCs.Boss.Araghur;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace gcsep.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAEternityDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule emodeRule = new(new EModeDropCondition());
            if (npc.type == ModContent.NPCType<AraghurHead>()) //|| npc.type == ModContent.NPCType<Abaddon>())
            {
                emodeRule.OnSuccess(FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<FlariumCrate>(), 5));
            }
        }
    }
}
