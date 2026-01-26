using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class HelheimForceHeader : SoulHeader
    {
        public override float Priority => 7f;
        public override int Item => ModContent.ItemType<HelheimForce>();
    }
}
