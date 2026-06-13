using CalamityMod.Buffs.Summon;
using gcsep.Content.Projectiles.Enchantments;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs.Minions
{
    public class DemonshadeSetDevilBuff : BaseSummonBuff
    {
        protected override int MinionProjectileType =>
            ModContent.ProjectileType<DemonshadeRedDevil>();

        protected override ref bool MinionBool
        {
            get
            {
                return ref BuffOwner.GetModPlayer<GCSEPlayer>().rDevil;
            }
        }
    }
}

