using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using gcsep.Thorium.Enchantments;
using Microsoft.Xna.Framework;
using Redemption.Items.Armor.PreHM.DragonLead;
using Redemption.Items.Armor.PreHM.LivingWood;
using Redemption.Items.Weapons.PreHM.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class LivingWoodEnchantR : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(206, 182, 95);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[20] = true;
            player.GetDamage(DamageClass.Summon) += 0.03f;
            player.AddEffect<LivingWoodEffectRHelmEffect>(Item);
            player.AddEffect<LivingWoodEffectR>(Item);
        }
        public class LivingWoodEffectRHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DragonLeadSkull>().UpdateArmorSet(player);
            }
        }
        public class LivingWoodEffectR : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingWoodEnchantR>();

            private int twigTimer;
            public override void PostUpdate(Player player)
            {
                twigTimer++;
                if (twigTimer >= 1200)
                {
                    twigTimer = 0;
                    DropTwig(player);
                }
            }

            private void DropTwig(Player player)
            {
                if (player.whoAmI != Main.myPlayer) return;

                if (player.ownedProjectileCounts[ModContent.ProjectileType<TwigProj>()] < 5)
                {
                    Vector2 position = player.Center + new Vector2(Main.rand.Next(-20, 20), 20);
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        position,
                        Vector2.Zero,
                        ModContent.ProjectileType<TwigProj>(),
                        20,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<LivingWoodHelmet>();
            recipe.AddIngredient<LivingWoodBody>();
            recipe.AddIngredient<LivingWoodLeggings>();
            recipe.AddIngredient(4281);
            recipe.AddIngredient<LogStaff>();
            recipe.AddIngredient(2196);
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
