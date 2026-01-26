using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Items.Accessories;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace gcsep
{
    public class GCSEItems : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item entity)
        {
            if (entity.type == ModContent.ItemType<Penetrator>() || entity.type == ModContent.ItemType<SparklingLove>() || entity.type == ModContent.ItemType<StyxGazer>())
            {
                entity.damage *= 2;
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (entity.type == ModContent.ItemType<StyxCrown>())
                {
                    entity.defense = 35;
                }
                if (entity.type == ModContent.ItemType<StyxChestplate>())
                {
                    entity.defense = 45;
                }
                if (entity.type == ModContent.ItemType<StyxLeggings>())
                {
                    entity.defense = 40;
                }
                if (entity.type == ModContent.ItemType<MutantBody>())
                {
                    entity.defense = 100;
                }
                if (entity.type == ModContent.ItemType<MutantMask>())
                {
                    entity.defense = 70;
                }
                if (entity.type == ModContent.ItemType<MutantPants>())
                {
                    entity.defense = 70;
                }
            }
            if (entity.type == ModContent.ItemType<GuardianTome>())
            {
                entity.damage = 1500;
            }
            if (entity.type == ModContent.ItemType<TheBiggestSting>())
            {
                entity.damage = 9750;
            }
            if (entity.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>())
            {
                entity.damage = 7800;
            }
            if (entity.type == ModContent.ItemType<SlimeRain>())
            {
                entity.damage = 6800;
            }
        }
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ModContent.ItemType<MutantBag>())
            {
                itemLoot.Add(ItemDropRule.ByCondition(new EModeDropCondition(), ModContent.ItemType<EternalEnergy>(), 1, 20, 30));
            }
        }
        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            // Helper: safely apply an accessory from your mod
            void Apply(string internalName)
            {
                if (ModContent.TryFind(Mod.Name, internalName, out ModItem item))
                    item.UpdateAccessory(player, hideVisual);
            }

            // Cache item type once
            int type = Item.type;

            // Soul identity flags
            bool isUniverse = type == ModContent.ItemType<UniverseSoul>();
            bool isEternity = type == ModContent.ItemType<EternitySoul>();
            bool isStargate = type == ModContent.ItemType<StargateSoul>();
            bool isAnyMajorSoul = isUniverse || isEternity || isStargate;

            // ---------------------------------------------------------
            // 1. Direct stat bonuses
            // ---------------------------------------------------------

            if (isUniverse &&
                ModCompatibility.SacredTools.Loaded &&
                ModCompatibility.Thorium.Loaded &&
                ModCompatibility.Calamity.Loaded)
            {
                player.maxMinions += 2;
            }

            if (type == ModContent.ItemType<BerserkerSoul>())
                player.GetDamage<MeleeDamageClass>() += 0.03f;

            if (type == ModContent.ItemType<SnipersSoul>())
                player.GetDamage<RangedDamageClass>() += 0.03f;

            if (type == ModContent.ItemType<ConjuristsSoul>())
                player.GetDamage<MeleeDamageClass>() += 0.03f;

            if (type == ModContent.ItemType<ArchWizardsSoul>())
                player.GetDamage<RangedDamageClass>() += 0.03f;

            // ---------------------------------------------------------
            // 2. Cross‑mod Souls of the Universe effects
            // ---------------------------------------------------------

            if (isAnyMajorSoul)
            {
                // SacredTools
                if (ModCompatibility.SacredTools.Loaded &&
                    !ModCompatibility.Calamity.Loaded)
                {
                    Apply("StalkerSoul");
                }

                // Thorium
                if (ModCompatibility.Thorium.Loaded)
                {
                    Apply("BardSoul");
                    Apply("GuardianAngelsSoul");
                }

                // BeekeeperClass
                if (ModCompatibility.BeekeeperClass.Loaded)
                {
                    Apply("BeekeeperSoul");
                }
            }

            // ---------------------------------------------------------
            // 3. Stargate/Eternity‑tier effects
            // ---------------------------------------------------------

            bool isEternityOrStargate = isEternity || isStargate;
            if (!isEternityOrStargate)
                return;

            // SacredTools
            if (ModCompatibility.SacredTools.Loaded)
                Apply("SoASoul");

            // FargoCrossmod + Calamity
            if (ModCompatibility.FargoCrossmod.Loaded &&
                ModCompatibility.Calamity.Loaded)
            {
                Apply("CalamitySoul");
            }

            // Thorium
            if (ModCompatibility.Thorium.Loaded)
                Apply("ThoriumSoul");

            // Polarities
            if (ModCompatibility.Polarities.Loaded)
            {
                Apply("WildernessForce");
                Apply("SpacetimeForce");
            }

            // Spooky
            if (ModCompatibility.Spooky.Loaded)
            {
                Apply("TerrorForce");
                Apply("HorrorForce");
            }

            // Secrets of the Shadows
            if (ModCompatibility.SOTS.Loaded)
            {
                Apply("SecretsForce");
                Apply("ShadowsForce");
            }

            // Redemption
            if (ModCompatibility.Redemption.Loaded)
                Apply("AdvancementForce");

            // Consolaria
            if (ModCompatibility.Consolaria.Loaded)
                Apply("HeroForce");

            // ---------------------------------------------------------
            // 4. Eternity Soul exclusive effects
            // ---------------------------------------------------------

            if (isEternity && !GCSEConfig.Instance.AlternativeSiblings)
            {
                if (GCSEConfig.Instance.SecretBosses)
                    Apply("CyclonicFin");

                Apply("EternityForce");
            }
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            //just in case
            //no cal inheritance because mutant enrages
            if (item.damage < 100000 && item.damage > 10000 && !GCSEUtils.IsModItem(item, "CalamityInheritance") && !GCSEUtils.IsModItem(item, "SacredTools") && !GCSEUtils.IsModItem(item, "FargowiltasSouls") && !GCSEUtils.IsModItem(item, "ThoriumMod") && !GCSEUtils.IsModItem(item, "CaamityMod"))
            {
                damage *= 0.1f;
            }
            if (item.damage > 100000 && !GCSEUtils.IsModItem(item, "CalamityInheritance") && !GCSEUtils.IsModItem(item, "SacredTools") && !GCSEUtils.IsModItem(item, "FargowiltasSouls") && !GCSEUtils.IsModItem(item, "ThoriumMod") && !GCSEUtils.IsModItem(item, "CaamityMod"))
            {
                damage *= 0.01f;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem is BaseSoul)
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    tooltips[i].Text = Regex.Replace(tooltips[i].Text, "22%", "25%", RegexOptions.IgnoreCase);
                }
            }
            if (item.type == ModContent.ItemType<UniverseSoul>() && ModCompatibility.SacredTools.Loaded && ModCompatibility.Thorium.Loaded && ModCompatibility.Calamity.Loaded)
            {
                tooltips.Add(new TooltipLine(Mod, "balance", $"[c/00A36C:CSE Balance:] Additional 2 minion slots."));
            }
            if (ModCompatibility.FargoCrossmod.Loaded)
            {
                if (item.type == ModContent.ItemType<SlimeRain>() || item.type == ModContent.ItemType<GuardianTome>() || item.type == ModContent.ItemType<TheBiggestSting>() || item.type == ModContent.ItemType<PhantasmalLeashOfCthulhu>() || item.type == ModContent.ItemType<TheBiggestSting>())
                {
                    tooltips.Add(new TooltipLine(Mod, "balance", $"[c/FF0000:CSE Balance:] No."));
                }
            }
            if (item.type == ModContent.ItemType<MutantsCurse>() || item.type == ModContent.ItemType<AbominationnVoodooDoll>())
            {
                //tooltips.Add(new TooltipLine(Mod, "1m", "Mutant max life and damage scales with ammount of supported mods."));
                //tooltips.Add(new TooltipLine(Mod, "2m", $"Points: {Math.Round(CSENpcs.multiplierM, 1)}, Max Life: {10000000 + Math.Round(CSENpcs.multiplierM, 1) * 10000000}, Damage: {500 + Math.Round(CSENpcs.multiplierM, 1) * (ModCompatibility.Calamity.Loaded ? 125 : 100)}"));
                //tooltips.Add(new TooltipLine(Mod, "3m", "Thorium adds 0.9 points. SoA adds 1.3. Calamity 1.8"));
                //tooltips.Add(new TooltipLine(Mod, "4m", "If olnly one of supported mods active Thorium - 1 SoA - 2 Calamity 2.8"));
                //tooltips.Add(new TooltipLine(Mod, "5m", "If Masochist mode enabled, points multiplied by 1.5"));
                if (ModCompatibility.SacredTools.Loaded && GCSEConfig.Instance.ExperimentalContent)
                {
                    tooltips.Add(new TooltipLine(Mod, "7m", "In first phase Mutant has Aura of Supression. After destroying aura second phase will start."));
                    tooltips.Add(new TooltipLine(Mod, "8m", "Aura can be destroyed only with Relic Weapons or Styx Armor set bonus. Mutant immune to damage if aura active."));
                }
            }
            if (ModCompatibility.Thorium.Loaded)
            {
                if (item.type == ModContent.ItemType<NekomiHood>() || item.type == ModContent.ItemType<NekomiHoodie>() || item.type == ModContent.ItemType<NekomiLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 5%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 5 and max inspiration by 10"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 1 and max bard buffs duration"));
                }
                if (item.type == ModContent.ItemType<StyxCrown>() || item.type == ModContent.ItemType<StyxChestplate>() || item.type == ModContent.ItemType<StyxLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 20%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 10 and max inspiration by 30"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 2 and max bard buffs duration"));
                }
                if (item.type == ModContent.ItemType<MutantBody>() || item.type == ModContent.ItemType<MutantMask>() || item.type == ModContent.ItemType<MutantPants>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 100%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 50 and max inspiration by 100"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 2 and max bard buffs duration"));
                }
                if (item.type == ModContent.ItemType<GaiaGreaves>() || item.type == ModContent.ItemType<GaiaHelmet>() || item.type == ModContent.ItemType<GaiaPlate>())
                {
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect1", $"[c/00A36C:CSE Set Bonus:] Increased inspiration regeneration and chance for notes to drop"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased thrower velocity and exhaustion regeneration by 20%"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased healing bonus by 10 and max inspiration by 30"));
                    tooltips.Add(new TooltipLine(Mod, "thoriumeffect2", $"[c/00A36C:CSE Set Bonus:] Increased technique points by 2 and max bard buffs duration"));
                }
            }
            if (ModCompatibility.Calamity.Loaded)
            {
                if (item.type == ModContent.ItemType<NekomiHood>() || item.type == ModContent.ItemType<NekomiHoodie>() || item.type == ModContent.ItemType<NekomiLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 70"));
                }
                if (item.type == ModContent.ItemType<StyxChestplate>() || item.type == ModContent.ItemType<StyxCrown>() || item.type == ModContent.ItemType<StyxLeggings>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 200"));
                }
                if (item.type == ModContent.ItemType<MutantBody>() || item.type == ModContent.ItemType<MutantMask>() || item.type == ModContent.ItemType<MutantPants>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 500"));
                }
                if (item.type == ModContent.ItemType<GaiaPlate>() || item.type == ModContent.ItemType<GaiaHelmet>() || item.type == ModContent.ItemType<GaiaGreaves>())
                {
                    tooltips.Add(new TooltipLine(Mod, "caleffect", $"[c/00A36C:CSE Set Bonus:] Increased stealth by 110"));
                }
            }
        }
        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            if (item.CountsAsClass<ThrowingDamageClass>())
            {
                velocity *= player.CSE().throwerVelocity;
            }
        }
        public override void UpdateEquip(Item Item, Player player)
        {
            if (ModCompatibility.Calamity.Loaded)
            {
                if (Item.type == ModContent.ItemType<StyxCrown>())
                {
                    player.GetCritChance(DamageClass.Generic) += 10f;
                    player.GetDamage(DamageClass.Generic) += 5 / 100f;
                    player.maxMinions += 5;
                }
                if (Item.type == ModContent.ItemType<StyxChestplate>())
                {
                    player.GetDamage(DamageClass.Generic) += 5 / 100f;
                }
                if (Item.type == ModContent.ItemType<StyxLeggings>())
                {
                    player.GetDamage(DamageClass.Generic) += 5 / 100f;
                }
            }
        }
    }
}
