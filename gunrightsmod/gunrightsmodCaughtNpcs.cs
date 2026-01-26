using gcsep.Core;
using gunrightsmod.Content.NPCs;
using gcsep.Items;
using Terraria.ModLoader;

namespace gcsep.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    internal class gunrightsmodCaughtNpcs : ModSystem
    {
        public static void gunrightsmodRegisterItems()
        {
            CaughtNPCItem.Add("Politician", ModContent.NPCType<Politician>(), "'“Might be better for society to just not release this one”'");
        }
    }
}
