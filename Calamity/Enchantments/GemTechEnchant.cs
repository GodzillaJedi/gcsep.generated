using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.GemTech;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Buffs;
using gcsep.Content.Buffs.Minions;
using gcsep.Content.Projectiles.Minions;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class GemTechEnchant : BaseEnchant
    {
        private Mod calamity;

        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
                calamity = ModLoader.GetMod("CalamityMod");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ModContent.RarityType<ExoticRainbow>();
            Item.value = 50000000;
        }

        public override Color nameColor => new(244, 25, 255);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GemTechSupernovaSkill>(Item);
            player.AddEffect<GemArmorEffect>(Item);
            player.AddEffect<ShadowFlameEffect>(Item);
            player.AddEffect<GemTechAres>(Item);
            player.AddEffect<GemTechTwins>(Item);
            player.AddEffect<GemTechThanatos>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GemTechHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<GemTechBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<GemTechSchynbaulds>());
            recipe.AddIngredient(ModContent.ItemType<TheFirstShadowflame>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class GemArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                var cp = player.Calamity();
                cp.GemTechSet = true; 
                cp.wearingRogueArmor = true;

                if (cp.GemTechState.IsRedGemActive) 
                {
                    cp.rogueStealthMax += 1.3f;
                }

                if (cp.GemTechState.IsYellowGemActive) 
                {
                    player.GetAttackSpeed<MeleeDamageClass>() += 0.26f;
                }

                player.setBonus = "Mucho Texto";
            }
        }

        public class ShadowFlameEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().shadowMinions = true;
                player.buffImmune[ModContent.BuffType<Shadowflame>()] = true;
            }
        }

        public class GemTechSupernovaSkill : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
            public override bool ActiveSkill => true;

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (stunned)
                    return;

                if (player.whoAmI == Main.myPlayer)
                {
                    int buff = ModContent.BuffType<GojiPrice>();
                    if (!player.HasBuff(buff))
                    {
                        player.AddBuff(buff, 120);
                    }

                    Vector2 position = player.Center;
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 10f;
                    int type = ModContent.ProjectileType<SupernovaStealthBoom>();
                    int damage = 100;
                    float knockBack = 2f;

                    var source = GetSource_EffectItem(player);

                    Projectile.NewProjectile(source, position, velocity, type, damage, knockBack, player.whoAmI);
                }
            }
        }

        public class GemTechAres : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
            public override bool MinionEffect => true;
            public static void Maintain(Player player, IEntitySource source, int buffType, int minionType, int baseDamage, float knockback)
            {
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.whoAmI != Main.myPlayer || player.dead || player.ownedProjectileCounts[minionType] >= 1)
                    return;

                int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);
                Projectile.NewProjectile(source, player.Center, -Vector2.UnitY * 5f, minionType, damage, knockback, player.whoAmI);
            }

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<GCSEPlayer>().AresSummon = true;

                Maintain(player, GetSource_EffectItem(player),
                    ModContent.BuffType<AresMinionBuff>(), ModContent.ProjectileType<AresMinion>(), 1500, 3f);
            }
        }

        public class GemTechTwins : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
            public override bool MinionEffect => true;
            public static void Maintain(Player player, IEntitySource source, int buffType, int minionType, int baseDamage, float knockback)
            {
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                // Only the owner spawns it, and only if there isn't one already
                if (player.whoAmI != Main.myPlayer || player.dead || player.ownedProjectileCounts[minionType] >= 1)
                    return;

                int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);
                Projectile.NewProjectile(source, player.Center, -Vector2.UnitY * 5f, minionType, damage, knockback, player.whoAmI);
            }

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<GCSEPlayer>().TwinsSummon = true;

                Maintain(player, GetSource_EffectItem(player),
                    ModContent.BuffType<ExoTwinsMinionBuff>(), ModContent.ProjectileType<ExoTwinsMinion>(), 1100, 2f);
            }
        }

        public class GemTechThanatos : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
            public override bool MinionEffect => true;
            public static void Maintain(Player player, IEntitySource source, int buffType, int minionType, int baseDamage, float knockback)
            {
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.whoAmI != Main.myPlayer || player.dead || player.ownedProjectileCounts[minionType] >= 1)
                    return;

                int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);
                Projectile.NewProjectile(source, player.Center, -Vector2.UnitY * 5f, minionType, damage, knockback, player.whoAmI);
            }

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<GCSEPlayer>().ThanatosSummon = true;

                Maintain(player, GetSource_EffectItem(player),
                    ModContent.BuffType<ThanatosMinionBuff>(), ModContent.ProjectileType<ThanatosMinion>(), 1200, 2f);
            }
        }

    }
}