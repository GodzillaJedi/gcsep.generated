using CalamityMod.Systems;
using CalamityMod.World;
using FargowiltasSouls.Core.Systems;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using gcsep.SoA;
using gcsep.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static CalamityMod.Systems.DifficultyModeSystem;
using FargowiltasCrossmod.Core.Calamity.Systems;

namespace gcsep.Crossmod.Difficulties
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    public class TrueEternityRevDifficulty : DifficultyMode
    {
        public override bool Enabled
        {
            get => WorldSaveSystem.trueRevEternity;
            set
            {
                WorldSaveSystem.trueRevEternity = value;

                if (value)
                {
                    CalamityWorld.revenge = true;
                    CalamityWorld.death = false;
                    TrueModeManager.setTrueMode(value);
                    WorldSavingSystem.EternityMode = true;
                    WorldSavingSystem.ShouldBeEternityMode = true;
                }

                if (Main.netMode != NetmodeID.SinglePlayer)
                    PacketManager.SendPacket<DifficultyPackets.TrueEternityRevPacket>();
            }
        }

        private Asset<Texture2D> _texture;
        public override Asset<Texture2D> Texture
        {
            get
            {
                _texture ??= ModContent.Request<Texture2D>("gcsep/Assets/EternityRevIcon");

                return _texture;
            }
        }

        public override LocalizedText ExpandedDescription => Language.GetText("Mods.gcsep.TrueEternityRev.ExpandedDescription");

        public TrueEternityRevDifficulty()
        {
            DifficultyScale = 1f;
            Name = Language.GetText("Mods.gcsep.TrueEternityRev.Name");
            ShortDescription = Language.GetText("Mods.gcsep.TrueEternityRev.ShortDescription");

            ActivationTextKey = "Mods.gcsep.TrueEternityRev.Activation";
            DeactivationTextKey = "Mods.gcsep.TrueEternityRev.Deactivation";

            ActivationSound = SoundID.Roar with { Pitch = -0.3f };
            ChatTextColor = Color.Pink;
        }

        public override int FavoredDifficultyAtTier(int tier)
        {
            DifficultyMode[] tierList = DifficultyTiers[tier];

            for (int i = 0; i < tierList.Length; i++)
            {
                if (tierList[i].Name.Value == "Death")
                    return i;
            }

            return 0;
        }
    }
}