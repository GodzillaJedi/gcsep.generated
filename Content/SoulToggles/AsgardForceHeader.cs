using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class AsgardForceHeader : SoulHeader
    {
        public override float Priority => 6.9f;
        public override int Item => ModContent.ItemType<AsgardForce>();
    }
}
