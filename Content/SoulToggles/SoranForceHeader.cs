using FargowiltasSouls.Core.Toggler.Content;
using gcsep.SoA.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class SoranForceHeader : SoulHeader
    {
        public override float Priority => 6.4f;
        public override int Item => ModContent.ItemType<SoranForce>();
    }
}
