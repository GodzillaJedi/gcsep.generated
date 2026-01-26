using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Thorium.Souls;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class MotDEHeader : SoulHeader
    {
        public override float Priority => 7.5f;
        public override int Item => ModContent.ItemType<MotDE>();
    }
}
