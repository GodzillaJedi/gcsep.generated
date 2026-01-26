using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Buffs;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ThoriumMod.Items.Cultist;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PyromancerEnchant : BaseEnchant
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
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PyromancerEffect>(Item);
            player.AddEffect<MagmaSeerEffect>(Item);
            if (player.AddEffect<PlasmaEffect>(Item))
            {
                ModContent.GetInstance<PlasmaGenerator>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<PyroEffect>(Item);
        }
        public class PyromancerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PyromancerEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PyromancerCowl>().UpdateArmorSet(player);
            }
        }
        public class MagmaSeerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PyromancerEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MagmaSeersMask>().UpdateArmorSet(player);
            }
        }
        public class PlasmaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PyromancerEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class PyroEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<PyromancerEnchant>();
            public override bool ActiveSkill => true;

            public override void PostUpdateEquips(Player player)
            {
                if (Main.gameMenu) return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                if (modPlayer.pyroCooldown > 0)
                {
                    modPlayer.pyroCooldown--;
                }
            }

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                if (modPlayer.pyroCooldown < 1)
                {
                    player.AddBuff(ModContent.BuffType<PureFlameBuff>(), 1200);
                    modPlayer.pyroCooldown = 3600;
                }
            }

            public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
            {
                if (!player.HasBuff<PureFlameBuff>()) return;

                Vector2 center = player.Center;
                Vector2 direction = Vector2.Normalize(Main.MouseWorld - center);

                if (Main.rand.Next(30) != 0)
                {
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        center,
                        direction.RotatedByRandom(Math.PI) * Main.rand.NextFloat(6f, 10f) * 2,
                        ModContent.ProjectileType<PureFireballProj>(),
                        player.ForceEffect<PyroEffect>() ? 500 : 250,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MagmaSeersMask>());
            recipe.AddIngredient(ModContent.ItemType<PyromancerCowl>());
            recipe.AddIngredient(ModContent.ItemType<PyromancerTabard>());
            recipe.AddIngredient(ModContent.ItemType<PyromancerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<PlasmaGenerator>());
            recipe.AddIngredient(ModContent.ItemType<AncientFlame>());
            recipe.AddIngredient(ModContent.ItemType<AlmanacofAgony>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
