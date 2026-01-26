using gcsep.Content.Projectiles;
using Terraria.ModLoader;

namespace gcsep
{
    public class GCSEKeySys : ModSystem
    {
        public static ModKeybind CruserAttack { get; set; }
        public static ModKeybind SupernovaAttack { get; set; }
        public override void Load()
        {
            CruserAttack = KeybindLoader.RegisterKeybind(base.Mod, "CruserAttack", "F");
            SupernovaAttack = KeybindLoader.RegisterKeybind(base.Mod, "SupernovaeAttack", "O");
        }
        public override void Unload()
        {
            CruserAttack = null;
            SupernovaAttack = null;
        }
    }
}
