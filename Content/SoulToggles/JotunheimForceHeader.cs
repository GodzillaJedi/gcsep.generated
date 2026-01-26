using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class JotunheimForceHeader : SoulHeader
    {
        public override float Priority => 7.1f;
        public override int Item => ModContent.ItemType<JotunheimForce>();
    }
}
