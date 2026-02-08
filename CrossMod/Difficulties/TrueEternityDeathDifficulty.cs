using CalamityMod.Systems;
using CalamityMod.World;
using FargowiltasCrossmod.Core.Calamity.Systems;
using FargowiltasSouls.Core.Systems;
using gcsep.Core;
using gcsep.SoA;
using gcsep.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

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
        public override Asset<Texture2D> Texture =>
            _texture ??= ModContent.Request<Texture2D>("gcsep/Assets/TrueEternityDeathIcon");

        public override Asset<Texture2D> TextureDisabled =>
            ModContent.Request<Texture2D>("gcsep/Assets/TrueEternityDeathIcon_Disabled");

        public override Asset<Texture2D> OutlineTexture =>
            ModContent.Request<Texture2D>("gcsep/Assets/TrueEternityDeathIcon_Outline");

        public override LocalizedText ExpandedDescription =>
            Language.GetText("Mods.gcsep.TrueEternityDeath.ExpandedDescription");

        public override float DifficultyScale => 2f;

        public override LocalizedText Name =>
            Language.GetText("Mods.gcsep.TrueEternityDeath.Name");

        public override LocalizedText ShortDescription =>
            Language.GetText("Mods.gcsep.EternityDeath.ShortDescription");

        public override SoundStyle ActivationSound =>
            SoundID.Roar with { Pitch = -0.1f };

        public override Color ChatTextColor => Color.DeepPink;

        public override int BackBoneGameModeID => 0;

        public override int[] FavoredDifficultyAtTier(int tier)
        {
            DifficultyMode[] tierList = DifficultyModeSystem.DifficultyTiers[tier];

            for (int i = 0; i < tierList.Length; i++)
            {
                if (tierList[i].Name.Value == "Death")
                    return new int[] { i };
            }

            return new int[] { 0 };
        }
    }
}