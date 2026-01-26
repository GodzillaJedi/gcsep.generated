using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Buffs;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories.ChallengeItems;
using SacredTools.Content.Items.Armor.Dreadfire;
using SacredTools.Content.Items.Armor.Lapis;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Special;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SoA.Enchantments.FrosthunterEnchant;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class LapisEnchant : BaseEnchant
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

        public override Color nameColor => new(46, 66, 163);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<LapisEffect>(Item);
            player.AddEffect<LapisSpeedEffect>(Item);
            player.AddEffect<LapisDefenseEffect>(Item);
            if (player.AddEffect<LapisCharmEffect>(Item))
            {
                ModContent.GetInstance<LapisPendant>().UpdateAccessory(player, hideVisual);
            }
        }

        public class LapisEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LapisEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LapisHelmet>().UpdateArmorSet(player);
            }
        }
        public class LapisCharmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LapisEnchant>();
        }
        public class LapisSpeedEffect : AccessoryEffect
        {
            public int lapisSpeedTimer;
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LapisEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                if (lapisSpeedTimer > 0)
                {
                    lapisSpeedTimer--;
                    player.moveSpeed += 0.25f;
                }
                player.moveSpeed += player.ForceEffect<LapisSpeedEffect>() ? 0.2f : 0.1f;
            }

            public override void OnHurt(Player player, Player.HurtInfo info)
            {
                if (!player.dead)
                {
                    lapisSpeedTimer = 300;
                }
            }
        }

        public class LapisDefenseEffect : AccessoryEffect
        {
            private int attackCounter = 0;
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LapisEnchant>();

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                attackCounter++;

                if (attackCounter >= (player.ForceEffect<LapisDefenseEffect>() ? 3 : 5))
                {
                    attackCounter = 0;
                    player.AddLeveledBuff<LapisShieldBuff>(600);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<LapisHelmet>();
            recipe.AddIngredient<LapisChest>();
            recipe.AddIngredient<LapisLegs>();
            recipe.AddIngredient<LapisPendant>();
            recipe.AddIngredient<LapisStaff>();
            recipe.AddIngredient<Haven>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
