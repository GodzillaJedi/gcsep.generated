using Terraria;
using Terraria.ModLoader;
using gcsep.Core;

namespace gcsep.Content.Buffs
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class YharimBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[this.Type] = false;
            Main.pvpBuff[this.Type] = true;
            Main.buffNoSave[this.Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            Mod calamity = ModLoader.GetMod("CalamityMod");

            // Defense
            player.statDefense += 10;

            // Damage & crit
            player.GetDamage(DamageClass.Generic) += 0.10f; // +10% damage
            player.GetCritChance(DamageClass.Generic) += 1f; // +1% crit

            // Max HP & regen
            player.statLifeMax2 += 5;
            player.lifeRegen += 2;

            // Movement
            player.moveSpeed += 0.01f;
            player.jumpSpeedBoost += 0.1f;
            player.maxFallSpeed += 0.1f;

            // Mana
            player.manaCost -= 0.05f;

            // DR (clamped)
            player.endurance += 0.05f;
            if (player.endurance > 0.95f)
                player.endurance = 0.95f;

            // Minions
            player.slotsMinions += 1;

            // Utility
            player.ammoBox = true;
            player.spaceGun = true;

            // Safe breath & lava immunity
            player.breathMax += 50;
            player.lavaMax += 2;

            // Life steal (safe cap)
            player.lifeSteal += 0.005f;
            if (player.lifeSteal > 0.1f)
                player.lifeSteal = 0.1f;

            // Dash (safe)
            player.dash = 1; // 1 = Tabi dash

            // Calamity stealth
            if (calamity != null)
                calamity.Call("AddMaxStealth", player.whoAmI, 0.2f);
        }
    }
}