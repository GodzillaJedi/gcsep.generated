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
using FargowiltasCrossmod.Core.Calamity.Systems;

namespace gcsep.Crossmod.Difficulties
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name, ModCompatibility.FargoCrossmod.Name)]
    public class TrueEternityDeathDifficulty : DifficultyMode
    {
        public override bool Enabled
        {
            get => WorldSaveSystem.trueDeathEternity;
            set
            {
                WorldSaveSystem.trueRevEternity = value;
                WorldSaveSystem.trueDeathEternity = value;

                if (value)
                {
                    CalamityWorld.revenge = true;
                    CalamityWorld.death = true;
                    TrueModeManager.setTrueMode(value);
                    WorldSavingSystem.EternityMode = true;
                    WorldSavingSystem.ShouldBeEternityMode = true;
                }

                if (Main.netMode != NetmodeID.SinglePlayer)
                    PacketManager.SendPacket<DifficultyPackets.TrueEternityDeathPacket>();
            }
        }

        private Asset<Texture2D> _texture;
        public override Asset<Texture2D> Texture
        {
            get
            {
                _texture ??= ModContent.Request<Texture2D>("gcsep/Assets/TrueEternityDeathIcon");

                return _texture;
            }
        }

        public override LocalizedText ExpandedDescription => Language.GetText("Mods.gcsep.TrueEternityDeath.ExpandedDescription");

        public TrueEternityDeathDifficulty()
        {
            DifficultyScale = 2f;
            Name = Language.GetText("Mods.gcsep.TrueEternityDeath.Name");
            ShortDescription = Language.GetText("Mods.gcsep.EternityDeath.ShortDescription");

            ActivationTextKey = "Mods.gcsep.TrueEternityDeath.Activation";
            DeactivationTextKey = "Mods.gcsep.TrueEternityDeath.Deactivation";

            ActivationSound = SoundID.Roar with { Pitch = -0.1f };
            ChatTextColor = Color.DeepPink;
        }

        public override int FavoredDifficultyAtTier(int tier)
        {
            DifficultyMode[] tierList = DifficultyModeSystem.DifficultyTiers[tier];

            for (int i = 0; i < tierList.Length; i++)
            {
                if (tierList[i].Name.Value == "Death")
                    return i;
            }

            return 0;
        }
    }
}