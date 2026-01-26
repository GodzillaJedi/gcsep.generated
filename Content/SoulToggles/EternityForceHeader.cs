using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Content.Items.Accessories;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class EternityForceHeader : SoulHeader
    {
        public override float Priority => 6.3f;
        public override int Item => ModContent.ItemType<EternityForce>();
    }
}
