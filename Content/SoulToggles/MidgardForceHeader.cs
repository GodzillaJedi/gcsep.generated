using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class MidgardForceHeader : SoulHeader
    {
        public override float Priority => 7.2f;
        public override int Item => ModContent.ItemType<MidgardForce>();
    }
}
