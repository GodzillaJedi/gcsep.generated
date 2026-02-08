using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.NormalNPCs;
using ContinentOfJourney.NPCs.Boss_ScarabBelief;
using ContinentOfJourney.NPCs.Boss_SlimeGod;
using ContinentOfJourney.NPCs.Boss_TheOverwatcher;
using ContinentOfJourney.NPCs.Boss_TheSon;
using gcsep.Core;
using Terraria.ModLoader;
using static CalamityMod.Events.BossRushEvent;

namespace gcsep.Homeward
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name, ModCompatibility.Calamity.Name)]
    public class HwjBossRush : ModSystem
    {
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                if (Bosses[i].EntityID == ModContent.NPCType<WildBumblebirb>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<SlimeGod>(), TimeChangeContext.Night));
                    Bosses.Insert(i, new Boss(ModContent.NPCType<Overseer>(), TimeChangeContext.Night));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<DevourerofGodsHead>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<ScarabBelief>()));
                }
                if (Bosses[i].EntityID == ModContent.NPCType<Draedon>())
                {
                    Bosses.Insert(i, new Boss(ModContent.NPCType<TheSon>()));
                }
            }
        }
    }
}