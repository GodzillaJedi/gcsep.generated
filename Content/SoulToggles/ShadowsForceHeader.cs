using FargowiltasSouls.Core.Toggler.Content;
using gcsep.SOTS.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class ShadowsForceHeader : SoulHeader
    {
        public override float Priority => 6.1f;
        public override int Item => ModContent.ItemType<ShadowsForce>();
    }
}
