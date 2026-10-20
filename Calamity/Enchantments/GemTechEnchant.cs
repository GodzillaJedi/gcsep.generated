using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.GemTech;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Buffs;
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
                player.Calamity().GemTechSet = true;
                player.Calamity().wearingRogueArmor = true;
                if (player.Calamity().GemTechState.IsRedGemActive)
                {
                    player.Calamity().rogueStealthMax += 1.3f;
                }

                if (player.Calamity().GemTechState.IsYellowGemActive)
                {
                    player.GetAttackSpeed<MeleeDamageClass>() += 0.26f;
                }

                player.setBonus = "Mucho Texto";
            }
        }
        public class GemTechSupernovaSkill : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GemTechEnchant>(); // Replace with your true Item Class type
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
    }
}
