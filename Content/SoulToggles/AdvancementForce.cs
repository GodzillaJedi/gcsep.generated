using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Redemption.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class AdvancementForceHeader : SoulHeader
    {
        public override float Priority => 8f;
        public override int Item => ModContent.ItemType<AdvancementForce>();
    }
}
