using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Vitality.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class EvilForceHeader : SoulHeader
    {
        public override float Priority => 7f;
        public override int Item => ModContent.ItemType<ForceOfEvil>();
    }
}
