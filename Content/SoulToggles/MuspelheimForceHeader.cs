using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class MuspelheimForceHeader : SoulHeader
    {
        public override float Priority => 7.3f;
        public override int Item => ModContent.ItemType<MuspelheimForce>();
    }
}
