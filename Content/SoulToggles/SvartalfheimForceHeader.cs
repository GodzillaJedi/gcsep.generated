using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class SvartalfheimForceHeader : SoulHeader
    {
        public override float Priority => 7.4f;
        public override int Item => ModContent.ItemType<SvartalfheimForce>();
    }
}
