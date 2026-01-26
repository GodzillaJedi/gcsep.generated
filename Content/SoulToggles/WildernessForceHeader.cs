using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Polarities.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class WildernessForceHeader : SoulHeader
    {
        public override float Priority => 6.4f;
        public override int Item => ModContent.ItemType<WildernessForce>();
    }
}
