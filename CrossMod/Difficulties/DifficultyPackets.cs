using FargowiltasCrossmod.Core.Calamity.Systems;
using FargowiltasSouls.Core.Systems;
using gcsep.Core;
using gcsep.Systems;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Crossmod.Difficulties
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    public class DifficultyPackets
    {

        [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
        public class TrueEternityRevPacket : BaseDLCPacket
        {
            public override void Write(ModPacket packet, params object[] context)
            {
                BitsByte containmentFlagWrapper = new()
                {
                    [0] = WorldSaveSystem.trueRevEternity,
                    [1] = WorldSavingSystem.EternityMode,
                    [2] = WorldSavingSystem.ShouldBeEternityMode
                };
                packet.Write(containmentFlagWrapper);
            }

            public override void Read(BinaryReader reader)
            {
                BitsByte containmentFlagWrapper = reader.ReadByte();
                WorldSaveSystem.trueRevEternity = containmentFlagWrapper[0];
                WorldSavingSystem.EternityMode = containmentFlagWrapper[1];
                WorldSavingSystem.ShouldBeEternityMode = containmentFlagWrapper[2];
            }
        }
        [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
        [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
        public class TrueEternityDeathPacket : BaseDLCPacket
        {
            public override void Write(ModPacket packet, params object[] context)
            {
                BitsByte containmentFlagWrapper = new()
                {
                    [0] = WorldSaveSystem.trueDeathEternity,
                    [1] = WorldSaveSystem.trueRevEternity,
                    [2] = WorldSavingSystem.EternityMode,
                    [3] = WorldSavingSystem.ShouldBeEternityMode
                };
                packet.Write(containmentFlagWrapper);
            }

            public override void Read(BinaryReader reader)
            {
                BitsByte containmentFlagWrapper = reader.ReadByte();
                WorldSaveSystem.trueDeathEternity = containmentFlagWrapper[0];
                WorldSaveSystem.trueRevEternity = containmentFlagWrapper[1];
                WorldSavingSystem.EternityMode = containmentFlagWrapper[2];
                WorldSavingSystem.ShouldBeEternityMode = containmentFlagWrapper[3];
            }
        }
    }
}
