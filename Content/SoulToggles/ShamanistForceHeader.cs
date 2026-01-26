using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Orchid.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class ShamanistForceHeader : SoulHeader
    {
        public override float Priority => 6.8f;
        public override int Item => ModContent.ItemType<ForceOfShamanist>();
    }
}
