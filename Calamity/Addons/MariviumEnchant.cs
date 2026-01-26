using CalamityEntropy;
using CalamityEntropy.Common;
using CalamityEntropy.Content.Buffs;
using CalamityEntropy.Content.Items.Accessories;
using CalamityEntropy.Content.Items.Armor.Marivinium;
using CalamityEntropy.Content.Items.Weapons;
using CalamityEntropy.Content.Rarities;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Entropy.Name)]
    [JITWhenModsEnabled(ModCompatibility.Entropy.Name)]
    public class MariviumEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ModContent.RarityType<AbyssalBlue>();
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HolyMantleEffect>(Item);
            player.AddEffect<WyrmToothEffect>(Item);
            player.AddEffect<MarviumEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MariviniumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<MariviniumBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<MariviniumLeggings>());
            recipe.AddIngredient(ModContent.ItemType<Xytheron>());
            recipe.AddIngredient(ModContent.ItemType<WyrmToothNecklace>());
            recipe.AddIngredient(ModContent.ItemType<HolyMantle>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public class HolyMantleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MariviumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Entropy().holyMantle = true;
            }
        }
        public class WyrmToothEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MariviumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage<GenericDamageClass>() += 0.24f;
                player.GetArmorPenetration<GenericDamageClass>() += 100f;
                player.GetCritChance(DamageClass.Generic) += 14f;
            }
        }
        public class MarviumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MariviumEnchant>();

            private static readonly int[] VanillaBuffs = new[]
            {
                BuffID.OnFire, BuffID.Frostburn, BuffID.Burning, BuffID.Weak,
                BuffID.Slow, BuffID.Bleeding, BuffID.Confused, BuffID.CursedInferno
            };

            private static readonly int[] ModdedBuffs = new[]
            {
                ModContent.BuffType<VulnerabilityHex>(),
                ModContent.BuffType<MiracleBlight>(),
                ModContent.BuffType<Dragonfire>(),
                ModContent.BuffType<GodSlayerInferno>(),
                ModContent.BuffType<VoidTouch>(),
                ModContent.BuffType<Plague>(),
                ModContent.BuffType<VoidVirus>(),
                ModContent.BuffType<Deceive>(),
                ModContent.BuffType<SulphuricPoisoning>(),
                ModContent.BuffType<Irradiated>(),
                ModContent.BuffType<Nightwither>(),
                ModContent.BuffType<HolyFlames>(),
                ModContent.BuffType<GlacialState>(),
                ModContent.BuffType<ArmorCrunch>(),
                ModContent.BuffType<BrimstoneFlames>(),
                ModContent.BuffType<Shadowflame>(),
                ModContent.BuffType<MaliciousCode>(),
                ModContent.BuffType<CrushDepth>()
            };

            private static void ApplyBuffImmunities(Player player)
            {
                foreach (int buffID in VanillaBuffs)
                    player.buffImmune[buffID] = true;

                foreach (int buffID in ModdedBuffs)
                    player.buffImmune[buffID] = true;

                if (Main.zenithWorld)
                    player.buffImmune[ModContent.BuffType<NOU>()] = true;
            }

            public override void PostUpdateEquips(Player player)
            {
                // Core bonuses
                player.Entropy().meleeDamageReduce += 0.2f;
                player.Entropy().damageReduce += 0.1f;
                player.Entropy().MariviniumSet = true;
                player.maxMinions += 10;
                player.GetDamage(DamageClass.Summon) += 1f;
                player.GetAttackSpeed(DamageClass.Summon) += 0.2f;
                player.whipRangeMultiplier += 0.2f;
                player.Entropy().summonCrit += 5;
                player.GetArmorPenetration(DamageClass.Generic) += 50f;

                // Rogue stealth if allowed
                if (!ModContent.GetInstance<Config>().MariviumArmorSetOnlyProvideStealthBarWhenHoldingRogueWeapons ||
                    (player.HeldItem != null && player.HeldItem.DamageType.CountsAsClass(CEUtils.RogueDC)))
                {
                    player.Calamity().wearingRogueArmor = true;
                    player.Calamity().rogueStealthMax += 1.35f;
                }

                // Stationary regen bonus
                if (player.velocity.Length() < 1f)
                {
                    player.lifeRegen += 45;
                    player.Entropy().lifeRegenPerSec += 10;
                }

                // True melee bonus
                if (player.HeldItem != null && player.HeldItem.DamageType.CountsAsClass(ModContent.GetInstance<TrueMeleeDamageClass>()))
                {
                    player.Entropy().damageReduce += 0.15f;
                    player.statDefense += 25;
                }

                ApplyBuffImmunities(player);
            }
        }
    }
}
