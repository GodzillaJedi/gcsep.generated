using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Calamity.Forces;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class AnnihilationForceHeader : SoulHeader
    {
        public override float Priority => 6.9f;
        public override int Item => ModContent.ItemType<AnnihilationForce>();
    }
}
