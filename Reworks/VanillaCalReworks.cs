using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Reworks
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class VanillaCalReworks : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            //Zenith
            if (item.type == 4956)
                item.damage = 260;
        }
    }
}