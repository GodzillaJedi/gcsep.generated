using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class AlfheimForceHeader : SoulHeader
    {
        public override float Priority => 6.8f;
        public override int Item => ModContent.ItemType<AlfheimForce>();
    }
}
