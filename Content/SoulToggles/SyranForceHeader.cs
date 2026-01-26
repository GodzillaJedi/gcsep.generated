using FargowiltasSouls.Core.Toggler.Content;
using gcsep.SoA.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class SyranForceHeader : SoulHeader
    {
        public override float Priority => 6.5f;
        public override int Item => ModContent.ItemType<SyranForce>();
    }
}
