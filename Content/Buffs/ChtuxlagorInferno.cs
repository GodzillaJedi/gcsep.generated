using FargowiltasSouls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace gcsep.Content.Buffs
{
    public class ChtuxlagorInferno : ModBuff
    {
        public const int TickNumber = 10000;
        public const int DPS = 100000;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            // Reduce CURRENT life safely (never below 1)
            int reduction = player.statLifeMax2 / 10;
            if (reduction < 1) reduction = 1;

            player.statLife -= reduction;
            if (player.statLife < 1)
                player.statLife = 1;

            // Zero out endurance (safe)
            player.endurance = 0f;

            // Heavy damage and crit reduction (flat, not multiplicative)
            player.GetDamage(DamageClass.Generic) -= 0.95f;   // -95% damage
            player.GetCritChance(DamageClass.Generic) -= 50f; // -50% crit

            // Minions & turrets
            player.maxMinions = 1;
            player.maxTurrets = 1;

            // Mana cripple
            player.manaCost += 0.9f;
            player.statManaMax2 = 10;

            // Armor pen cripple
            player.GetArmorPenetration(DamageClass.Generic) -= 50;

            // Movement cripple (clamped)
            player.moveSpeed -= 0.9f;
            if (player.moveSpeed < -0.5f)
                player.moveSpeed = -0.5f;

            // Remove all immunity
            player.immune = false;
            player.immuneNoBlink = false;
            player.immuneTime = 0;

            // Remove mobility
            player.noFallDmg = false;
            player.wingTime = 0;
            player.wingTimeMax = 0;
            player.rocketTime = 0;

            // Debuffs
            player.slow = true;
            player.moonLeech = true;

            // Fargo’s Souls debuffs
            var fargo = player.FargoSouls();
            fargo.FlamesoftheUniverse = true;
            fargo.Shadowflame = true;
            fargo.Asocial = true;
            fargo.MutantNibble = true;
            fargo.Atrophied = true;
            fargo.Kneecapped = true;
            fargo.CurseoftheMoon = true;
            fargo.Defenseless = true;
            fargo.GodEater = true;
            fargo.noDodge = true;
            fargo.MutantPresence = true;
            fargo.Hexed = true;
            fargo.Infested = true;
            fargo.Jammed = true;
            fargo.DeathMarked = true;
            fargo.OceanicMaul = true;
            fargo.noSupersonic = true;
            fargo.SqueakyToy = true;

            // Remove immunity to ALL debuffs
            for (int i = 0; i < BuffLoader.BuffCount; i++)
            {
                if (Main.debuff[i])
                    player.buffImmune[i] = false;
            }
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.CSE().chtuxlagorInferno < npc.buffTime[buffIndex])
            {
                npc.CSE().chtuxlagorInferno = npc.buffTime[buffIndex];
            }

            npc.defense = 0;
            npc.defDefense = 0;

            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                    npc.buffImmune[index] = false;
            }

            npc.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}
