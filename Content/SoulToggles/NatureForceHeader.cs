using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Vitality.Forces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class NatureForceHeader : SoulHeader
    {
        public override float Priority => 7f;
        public override int Item => ModContent.ItemType<ForceOfNature>();
    }
}
