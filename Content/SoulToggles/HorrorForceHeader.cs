using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Spooky.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class HorrorForceHeader : SoulHeader
    {
        public override float Priority => 7.1f;
        public override int Item => ModContent.ItemType<HorrorForce>();
    }
}
