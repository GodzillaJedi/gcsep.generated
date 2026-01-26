using gcsep.Core;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public partial class CalNpcs : GlobalNPC
    {
        public override bool InstancePerEntity => true;
    }
}
