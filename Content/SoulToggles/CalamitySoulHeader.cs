using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Calamity.Souls;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class CalamitySoulHeader : SoulHeader
    {
        public override float Priority => 6.2f;
        public override int Item => ModContent.ItemType<CalamitySoul>();
    }
}
