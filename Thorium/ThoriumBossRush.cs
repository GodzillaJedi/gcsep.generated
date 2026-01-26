using CalamityMod.NPCs.DevourerofGods;
using gcsep.Core;
using Terraria.ModLoader;
using ThoriumMod.NPCs.BossThePrimordials;
using static CalamityMod.Events.BossRushEvent;

namespace gcsep.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class ThoriumBossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ModLoader.HasMod("RagnarokMod") && !ModLoader.HasMod("ThoriumRework"))
            {
                for (int i = Bosses.Count - 1; i >= 0; i--)
                {
                    if (Bosses[i].EntityID == ModContent.NPCType<DevourerofGodsHead>())
                    {
                        Bosses.Insert(i, new Boss(ModContent.NPCType<DreamEater>()));
                    }
                }
            }
        }
    }
}