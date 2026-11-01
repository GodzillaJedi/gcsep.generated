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
using gcsep.Content.Projectiles;
using gcsep.Content.Projectiles.Minions;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
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

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<GCSEPlayer>().AresSummon = true;

                int buffType = ModContent.BuffType<AresMinionBuff>();
                int minionType = ModContent.ProjectileType<AresMinion>();

                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.ownedProjectileCounts[minionType] < 1 && player.whoAmI == Main.myPlayer && !player.dead)
                {
                    var source = GetSource_EffectItem(player);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(1500);

                    Projectile.NewProjectile(source, player.Center, -Vector2.UnitY * 5f, minionType, damage, 3f, player.whoAmI);
                }
            }

            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer) return;

                if (!player.HasEffect<GemTechAres>())
                {
                    player.GetModPlayer<GCSEPlayer>().AresSummon = false;

                    int buffType = ModContent.BuffType<AresMinionBuff>();
                    int minionType = ModContent.ProjectileType<AresMinion>();

                    int buffIndex = player.FindBuffIndex(buffType);
                    if (buffIndex != -1) player.DelBuff(buffIndex);

                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == player.whoAmI && p.type == minionType)
                            p.Kill();
                    }
                }
            }
        }

        public class GemTechTwins : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
            public override bool MinionEffect => true;

            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<GCSEPlayer>().TwinsSummon = true;

                int buffType = ModContent.BuffType<ExoTwinsMinionBuff>();
                int minionType = ModContent.ProjectileType<ExoTwinsMinion>();

                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.ownedProjectileCounts[minionType] < 1 && player.whoAmI == Main.myPlayer && !player.dead)
                {
                    var source = GetSource_EffectItem(player);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(1100);

                    Projectile.NewProjectile(source, player.Center, -Vector2.UnitY * 5f, minionType, damage, 2f, player.whoAmI);
                }
            }

            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer) return;

                if (!player.HasEffect<GemTechTwins>())
                {
                    player.GetModPlayer<GCSEPlayer>().TwinsSummon = false;

                    int buffType = ModContent.BuffType<ExoTwinsMinionBuff>();
                    int minionType = ModContent.ProjectileType<ExoTwinsMinion>();

                    int buffIndex = player.FindBuffIndex(buffType);
                    if (buffIndex != -1) player.DelBuff(buffIndex);

                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == player.whoAmI && p.type == minionType)
                            p.Kill();
                    }
                }
            }
        }
        public class GemTechThanatos : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>();
            public override bool MinionEffect => true;
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<GCSEPlayer>().ThanatosSummon = true;

                int buffType = ModContent.BuffType<ThanatosMinionBuff>();
                int minionType = ModContent.ProjectileType<ThanatosMinion>();

                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.ownedProjectileCounts[minionType] < 1 && player.whoAmI == Main.myPlayer && !player.dead)
                {
                    var source = GetSource_EffectItem(player);

                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(1200);

                    Projectile.NewProjectile(source, player.Center, -Vector2.UnitY * 5f, minionType, damage, 2f, player.whoAmI);
                }
            }
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<GemTechThanatos>())
                {
                    player.GetModPlayer<GCSEPlayer>().ThanatosSummon = false;

                    int buffType = ModContent.BuffType<ThanatosMinionBuff>();
                    int minionType = ModContent.ProjectileType<ThanatosMinion>();

                    int buffIndex = player.FindBuffIndex(buffType);
                    if (buffIndex != -1)
                        player.DelBuff(buffIndex);

                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == player.whoAmI && p.type == minionType)
                            p.Kill();
                    }
                }
            }
        }
    }
}