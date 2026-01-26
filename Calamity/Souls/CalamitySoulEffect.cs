using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using Terraria.ModLoader;

namespace gcsep.Calamity.Souls
{
    [JITWhenModsEnabled(new string[] { "CalamityMod" })]
    [ExtendsFromMod(new string[] { "CalamityMod" })]
    public abstract class CalamitySoulEffect : AccessoryEffect
    {
        public override Header ToggleHeader
        {
            get => Header.GetHeader<CalamitySoulHeader>();
        }
    }
}
