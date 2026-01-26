using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class YewWoodEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<YewEffect>(Item))
            {
                player.GetThoriumPlayer().yewCharging = true;
            }
            if (player.AddEffect<ThumbRingEffect>(Item))
            {
                ModContent.GetInstance<ThumbRing>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<YewWoodEffect>(Item);
        }
        public class YewWoodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideHunterEnchant>();
            public override bool ExtraAttackEffect => true;

            public override void PostUpdate(Player player)
            {
                if (Main.gameMenu) return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                if (modPlayer.yewArrowCooldown > 1)
                    modPlayer.yewArrowCooldown--;
            }
            public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
            {
                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                if (modPlayer.yewArrowCooldown < 0)
                {
                    Vector2 center = player.Center;
                    Vector2 vector = Vector2.Normalize(Main.MouseWorld - center);

                    if (Main.rand.Next(player.ForceEffect<YewWoodEffect>() ? 75 : 100) != 0)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            player.Center,
                            vector,
                            ModContent.ProjectileType<VileArrow>(),
                            1,
                            0f,
                            player.whoAmI
                        );
                        modPlayer.yewArrowCooldown = 60;
                    }
                }
            }
        }
        public class YewEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<YewWoodEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ThumbRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<YewWoodEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<YewWoodHelmet>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodBreastguard>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ThumbRing>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodBow>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodFlintlock>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
