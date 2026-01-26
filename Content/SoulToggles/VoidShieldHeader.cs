using FargowiltasSouls.Core.Toggler.Content;
using gcsep.SOTS.Souls;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    internal class VoidShieldHeader : SoulHeader
    {
        public override float Priority => 7.5f;
        public override int Item => ModContent.ItemType<VoidShield>();
    }
}
