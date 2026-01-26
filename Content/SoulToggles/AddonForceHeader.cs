using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Calamity.Addons;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class AddonsForceHeader : SoulHeader
    {
        public override float Priority => 6.1f;
        public override int Item => ModContent.ItemType<AddonsForce>();
    }
}
