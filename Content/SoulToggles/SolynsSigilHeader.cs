using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Calamity.Addons;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class SolynsSigilHeader : SoulHeader
    {
        public override float Priority => 9f;
        public override int Item => ModContent.ItemType<SolynsSigil>();
    }
}
