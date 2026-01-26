using gcsep.Core;
using SpiritMod.NPCs.Town;
using gcsep.Items;
using Terraria.ModLoader;

namespace gcsep.SpiritMod
{
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    internal class SpiritModCaughtNpcs : ModSystem
    {
        public static void SpiritModRegisterItems()
        {
            CaughtNPCItem.Add("Adventurer", ModContent.NPCType<Adventurer>(), "''");
            CaughtNPCItem.Add("Gambler", ModContent.NPCType<Gambler>(), "''");
            CaughtNPCItem.Add("Rogue", ModContent.NPCType<Rogue>(), "''");
            CaughtNPCItem.Add("RuneWizard", ModContent.NPCType<RuneWizard>(), "''");
        }
    }
}
