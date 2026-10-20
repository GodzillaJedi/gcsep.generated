using CalamityMod.Buffs.Summon;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Calamity.Enchantments;
using gcsep.Content.Projectiles.Enchantments;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Content.Buffs.Minions
{
    public class DemonshadeSetDevilBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // Standard Fargo/tModLoader buff properties
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int projType = ModContent.ProjectileType<DemonshadeRedDevil>();
            var gPlayer = player.GetModPlayer<GCSEPlayer>();

            // 1. DUAL SYSTEM CHECK: Keep flag locked true if wearing the Enchantment accessory OR if the projectile exists
            if (player.HasEffect<DemonShadeEnchant.RedDevil>() || player.ownedProjectileCounts[projType] > 0)
            {
                gPlayer.rDevil = true;
            }

            // 2. Clean up the buff safely if both the armor and accessory flags drop off
            if (!gPlayer.rDevil)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                // Keeps the buff timeline frozen infinite while wearing the armor set or accessory
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}

