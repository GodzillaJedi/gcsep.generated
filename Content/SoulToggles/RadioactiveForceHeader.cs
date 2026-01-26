using FargowiltasSouls.Core.Toggler.Content;
using gcsep.gunrightsmod.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class RadioactiveForceHeader : SoulHeader
    {
        public override float Priority => 7.4f;
        public override int Item => ModContent.ItemType<RadioactiveForce>();
    }
}
