using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Calamity.Souls;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class ThoriumSoulHeader : SoulHeader
    {
        public override float Priority => 6.8f;
        public override int Item => ModContent.ItemType<CalamitySoul>();
    }
}
