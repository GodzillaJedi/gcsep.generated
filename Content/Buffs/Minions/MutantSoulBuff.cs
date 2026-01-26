using FargowiltasSouls;
using Microsoft.Xna.Framework;
using gcsep.Content.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs.Minions
{
    public class MutantSoulBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {

            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GCSEPlayer>().equippedPhantasmalEnchantment = true;
            if (player.whoAmI == Main.myPlayer)
            {
                const int damage = 10000;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MutantSoul>()] < 1)
                    FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, ModContent.ProjectileType<MutantSoul>(), damage, 19f, player.whoAmI);
            }
        }
    }
}