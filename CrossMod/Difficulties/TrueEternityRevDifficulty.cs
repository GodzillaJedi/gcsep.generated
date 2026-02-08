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
using Terraria.Audio;

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
        public override Asset<Texture2D> Texture =>
            _texture ??= ModContent.Request<Texture2D>("gcsep/Assets/EternityRevIcon");

        public override Asset<Texture2D> TextureDisabled =>
            ModContent.Request<Texture2D>("gcsep/Assets/EternityRevIcon_Disabled");

        public override Asset<Texture2D> OutlineTexture =>
            ModContent.Request<Texture2D>("gcsep/Assets/EternityRevIcon_Outline");

        public override SoundStyle ActivationSound =>
            SoundID.Roar with { Pitch = -0.3f };

        public override int BackBoneGameModeID => 0;

        public override float DifficultyScale => 1f;

        public override LocalizedText Name =>
            Language.GetText("Mods.gcsep.TrueEternityRev.Name");

        public override LocalizedText ShortDescription =>
            Language.GetText("Mods.gcsep.TrueEternityRev.ShortDescription");

        public override LocalizedText ExpandedDescription =>
            Language.GetText("Mods.gcsep.TrueEternityRev.ExpandedDescription");

        public override Color ChatTextColor => Color.Pink;

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