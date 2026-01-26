using FargowiltasSouls.Core.Toggler.Content;
using gcsep.SpiritMod.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class AtlantisForceHeader : SoulHeader
    {
        public override float Priority => 7.4f;
        public override int Item => ModContent.ItemType<AtlantisForce>();
    }
}