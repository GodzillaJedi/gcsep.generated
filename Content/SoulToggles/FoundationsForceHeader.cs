using FargowiltasSouls.Core.Toggler.Content;
using gcsep.SoA.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class FoundationsForceHeader : SoulHeader
    {
        public override float Priority => 6.7f;
        public override int Item => ModContent.ItemType<FoundationsForce>();
    }
}
