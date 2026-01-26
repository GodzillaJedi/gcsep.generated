using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class VanaheimForceHeader : SoulHeader
    {
        public override float Priority => 7.4f;
        public override int Item => ModContent.ItemType<VanaheimForce>();
    }
}
