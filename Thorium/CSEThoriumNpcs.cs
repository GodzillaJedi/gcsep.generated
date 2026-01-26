using Fargowiltas.NPCs;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.Placeable;
using ThoriumMod.NPCs.BossBoreanStrider;
using ThoriumMod.NPCs.BossBuriedChampion;
using ThoriumMod.NPCs.BossFallenBeholder;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.NPCs.BossThePrimordials;
using ThoriumMod.NPCs.BossViscount;
using static Terraria.ModLoader.ModContent;
using ThoriumMod.NPCs.BossMini;


namespace gcsep.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public partial class CSEThoriumNpcs : GlobalNPC
    {
        public bool isCutOpen;
        public bool crying;
        private Vector2 previousPosition;
        public override bool InstancePerEntity => true;
        public override bool PreKill(NPC npc)
        {
            bool doDeviText = false;
            if (npc.type == ModContent.NPCType<PatchWerk>() && !ThoriumWorldSave.downedPatchWrek)
            {
                doDeviText = true;
                ThoriumWorldSave.downedPatchWrek = true;
            }
            if (npc.type == ModContent.NPCType<Illusionist>() && !ThoriumWorldSave.downedIllusionist)
            {
                doDeviText = true;
                ThoriumWorldSave.downedIllusionist = true;
            }
            if (npc.type == ModContent.NPCType<CorpseBloom>() && !ThoriumWorldSave.downedCorpseBloom)
            {
                doDeviText = true;
                ThoriumWorldSave.downedCorpseBloom = true;
            }
            if (doDeviText && Main.netMode != NetmodeID.Server)
            {
                Main.NewText("A new item has been unlocked in Deviantt's shop!", Color.HotPink);
            }
            return true;
        }
        public override void PostAI(NPC npc)
        {
            if (isCutOpen)
            {
                if (npc.position != previousPosition && npc.velocity != Vector2.Zero)
                {
                    npc.SimpleStrikeNPC(5, 0);

                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
                }
                previousPosition = npc.position;
            }
        }
        public override void SetDefaults(NPC npc)
        {
            int num1 = ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded ? 30000 : 20000;
            int num2 = ModCompatibility.Calamity.Loaded || ModCompatibility.SacredTools.Loaded ? 300000 : 150000;
            int num3 = 30000000;

            int num12 = 50;
            int num22 = 100;
            int num32 = 200;

            if (gcsep.SwarmActive)
            {
                if (npc.type == NPCType<QueenJellyfish>() || npc.type == NPCType<TheGrandThunderBird>() || npc.type == NPCType<GraniteEnergyStorm>() || npc.type == NPCType<BuriedChampion>() || npc.type == NPCType<Viscount>() || npc.type == NPCType<StarScouter>())
                {
                    npc.lifeMax = num1 * gcsep.SwarmItemsUsed;
                    npc.damage = num12 * gcsep.SwarmItemsUsed;
                }
                if (npc.type == NPCType<BoreanStrider>() || npc.type == NPCType<FallenBeholder>() || npc.type == NPCType<FallenBeholder2>() || npc.type == NPCType<Lich>() || npc.type == NPCType<LichHeadless>() || npc.type == NPCType<ForgottenOne>() || npc.type == NPCType<ForgottenOneCracked>() || npc.type == NPCType<ForgottenOneReleased>())
                {
                    npc.lifeMax = num2 * gcsep.SwarmItemsUsed;
                    npc.damage = num22 * gcsep.SwarmItemsUsed;
                }
                if (npc.type == NPCType<DreamEater>())
                {
                    npc.lifeMax = num3 * gcsep.SwarmItemsUsed;
                    npc.damage = num32 * gcsep.SwarmItemsUsed;
                }
            }
        }
        public override void OnHitByProjectile(NPC target, Projectile proj, NPC.HitInfo hit, int damageDone)
        {
            if (proj.DamageType == ModContent.GetInstance<HealerDamage>())
            {
                switch (proj.owner.ToPlayer().meleeEnchant)
                {
                    case 1:
                        target.AddBuff(BuffID.Venom, 180);
                        break;
                    case 2:
                        target.AddBuff(BuffID.CursedInferno, 180);
                        break;
                    case 3:
                        target.AddBuff(BuffID.OnFire, 180);
                        break;
                    case 4:
                        target.AddBuff(BuffID.Midas, 180);
                        break;
                    case 5:
                        target.AddBuff(BuffID.Ichor, 180);
                        break;
                    case 6:
                        target.AddBuff(BuffID.Confused, 180);
                        break;
                    case 8:
                        target.AddBuff(BuffID.Poisoned, 180);
                        break;
                }
            }
        }
        public override void ResetEffects(NPC npc)
        {
            isCutOpen = false;
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (crying)
            {
                //how tf you can play so bad that your enemies cry hearing it
                hurtInfo.Damage = (int)(hurtInfo.Damage * 0.9f);
            }
        }
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCType<LumberJack>())
            {
                shop.Add(new Item(ItemType<ThoriumMod.Items.Misc.Deadwood>()) { shopCustomPrice = Item.buyPrice(copper: 20) }, Condition.InGraveyard);
                shop.Add(new Item(ItemType<EvergreenBlock>()) { shopCustomPrice = Item.buyPrice(copper: 20) }, Condition.DownedEverscream);
                shop.Add(new Item(ItemType<YewWood>()) { shopCustomPrice = Item.buyPrice(copper: 20) }, Condition.DownedGoblinArmy);
            }
            base.ModifyShop(shop);
        }
        public override void OnHitByItem(NPC target, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (item.DamageType == ModContent.GetInstance<HealerDamage>())
            {
                switch (player.meleeEnchant)
                {
                    case 1:
                        target.AddBuff(BuffID.Venom, 180);
                        break;
                    case 2:
                        target.AddBuff(BuffID.CursedInferno, 180);
                        break;
                    case 3:
                        target.AddBuff(BuffID.OnFire, 180);
                        break;
                    case 4:
                        target.AddBuff(BuffID.Midas, 180);
                        break;
                    case 5:
                        target.AddBuff(BuffID.Ichor, 180);
                        break;
                    case 6:
                        target.AddBuff(BuffID.Confused, 180);
                        break;
                    case 8:
                        target.AddBuff(BuffID.Poisoned, 180);
                        break;
                }
            }
        }
    }
}