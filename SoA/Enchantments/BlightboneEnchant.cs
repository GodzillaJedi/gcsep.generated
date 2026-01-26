using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories.Sigils;
using SacredTools.Content.Items.Armor.Blightbone;
using SacredTools.Content.Items.Armor.Lunar.Solar;
using SacredTools.Content.Items.Weapons.Dreadfire;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SoA.Enchantments.AsthraltiteEnchant;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class BlightboneEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");


        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 3;
            Item.value = 100000;
        }

        public override Color nameColor => new(124, 10, 10);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BlightMaskEffect>(Item);
            player.AddEffect<BlightboneEffect>(Item);
            if (player.AddEffect<PumpkinAmuletEffect>(Item))
            {
                ModContent.GetInstance<PumpkinAmulet>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<HairpinEffect>(Item))
            {
                ModContent.GetInstance<FeatherHairpin>().UpdateAccessory(player, hideVisual);
            }
        }

        public class BlightMaskEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BlightboneEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BlightMask>().UpdateArmorSet(player);
            }
        }
        public class PumpkinAmuletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BlightboneEnchant>();
        }
        public class HairpinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BlightboneEnchant>();
        }
        public class BlightboneEffect : AccessoryEffect
        {
            public int boneCD;
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BlightboneEnchant>();
            public override bool ExtraAttackEffect => true;
            public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
            {
                if (boneCD > 0)
                {
                    return;
                }

                boneCD = 30;
                float num = 50f;
                Vector2 center = player.Center;
                Vector2 vector = Vector2.Normalize(Main.MouseWorld - center);
                for (int i = 0; i < (player.ForceEffect<BlightboneEffect>() ? 3 : 1); i++)
                {
                    Projectile.NewProjectile(GetSource_EffectItem(player), center, vector.RotatedByRandom(Math.PI / 6.0) * Main.rand.NextFloat(6f, 10f) * 2, ModContent.ProjectileType<Blightbone>(), (int)(num * player.ActualClassDamage(DamageClass.Throwing)), 9f, player.whoAmI);
                }
            }
            public override void PostUpdateEquips(Player player)
            {
                if (boneCD > 0)
                {
                    boneCD--;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<BlightMask>();
            recipe.AddIngredient<BlightChest>();
            recipe.AddIngredient<BlightLegs>();
            recipe.AddIngredient<PumpkinAmulet>();
            recipe.AddIngredient<FeatherHairpin>();
            recipe.AddIngredient<PumpGlove>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
