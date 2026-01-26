using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Orchid.Forces;
using gcsep.Thorium.Forces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace gcsep.Content.SoulToggles
{
    public class AlchemistForceHeader : SoulHeader
    {
        public override float Priority => 6.8f;
        public override int Item => ModContent.ItemType<ForceOfAlchemy>();
    }
}
