using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.NPCs.Guntera
{
    public class Gun : ModBuff
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SecretBosses;
        }
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
    }
}