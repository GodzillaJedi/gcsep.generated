using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials;
using ThoriumMod.Items.Donate;

namespace gcsep.Thorium.Toggles
{
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    public class BlastShieldEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<BlastShield>();

        public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
    }
    public class OmegaCoreEffect : AccessoryEffect
    {
        public override int ToggleItemType => ModContent.ItemType<TheOmegaCore>();

        public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
    }
}
