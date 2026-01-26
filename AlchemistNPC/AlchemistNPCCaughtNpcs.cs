using AlchemistNPCLite.NPCs;
using Fargowiltas.Items.CaughtNPCs;
using gcsep.Core;
using Terraria.ModLoader;

namespace gcsep.AlchemistNPC
{
    [ExtendsFromMod(ModCompatibility.AlchNPCs.Name)]
    [JITWhenModsEnabled(ModCompatibility.AlchNPCs.Name)]
    internal class AlchemistNPCCaughtNpcs : ModSystem
    {
        public static void AlchemistNPCCaughtNpcsRegisterItems()
        {
            CaughtNPCItem.Add("Alchemist", ModContent.NPCType<Alchemist>());
            CaughtNPCItem.Add("Architect", ModContent.NPCType<Architect>());
            CaughtNPCItem.Add("Brewer", ModContent.NPCType<Brewer>());
            CaughtNPCItem.Add("Jeweler", ModContent.NPCType<Jeweler>());
            CaughtNPCItem.Add("Musician", ModContent.NPCType<Musician>());
            CaughtNPCItem.Add("Operator", ModContent.NPCType<Operator>());
            CaughtNPCItem.Add("Tinkerer", ModContent.NPCType<Tinkerer>());
            CaughtNPCItem.Add("YoungBrewer", ModContent.NPCType<YoungBrewer>());
        }
    }
}
