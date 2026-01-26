using AlchemistNPCLite.NPCs;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Core.Systems;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.AlchemistNPC
{
    [ExtendsFromMod(ModCompatibility.AlchNPCs.Name)]
    [JITWhenModsEnabled(ModCompatibility.AlchNPCs.Name)]
    public class AlchNPCNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type == ModContent.NPCType<Operator>() && shopName == "ModBags2")
            {
                int slot = 0;

                if (WorldSavingSystem.DownedDevi)
                {
                    GCSEUtils.AddToNextEmptySlot(ref slot, items, ModContent.ItemType<DeviBag>(), 200000);
                }
                if (WorldSavingSystem.DownedAbom)
                {
                    GCSEUtils.AddToNextEmptySlot(ref slot, items, ModContent.ItemType<AbomBag>(), 150000000);
                }
                if (WorldSavingSystem.DownedMutant)
                {
                    GCSEUtils.AddToNextEmptySlot(ref slot, items, ModContent.ItemType<MutantBag>(), 500000000);
                }
            }
        }
    }
}
