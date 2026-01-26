using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Content.Items.Armor;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class LumberjackArmorHeader : SoulHeader
    {
        public override float Priority => 273572985f;
        public override int Item => ModContent.ItemType<TrueLumberjackMask>();
    }
}
