using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Consolaria.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class HeroHeader : EnchantHeader
    {
        public override int Item => ModContent.ItemType<HeroForce>();
        public override float Priority => 0.81f;
    }
}
