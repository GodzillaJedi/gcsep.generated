using FargowiltasSouls.Core.Toggler.Content;
using gcsep.SoA.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class GenerationsForceHeader : SoulHeader
    {
        public override float Priority => 6.6f;
        public override int Item => ModContent.ItemType<GenerationsForce>();
    }
}
