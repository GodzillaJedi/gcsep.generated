using CalamityMod;
using CalamityMod.Buffs.Pets;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.DesertProwler;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Pets;
using CalamityMod.Projectiles.Typeless;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
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
    public class DesertProwlerEnchantEx : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(102, 89, 54);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Register your modular effects
            player.AddEffect<DesertProwlerEffect>(Item);
            player.AddEffect<LuxorEffect>(Item);
            //player.AddEffect<DimeEffect>(Item);
            player.AddEffect<DesertProwlerCloakEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerHat>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerShirt>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerPants>());
            recipe.AddIngredient(ModContent.ItemType<LuxorsGift>());
            recipe.AddIngredient(ModContent.ItemType<ThiefsDime>());
            recipe.AddIngredient(ModContent.ItemType<CrackshotColt>());
            recipe.AddIngredient(ModContent.ItemType<DesertProwlerEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class DesertProwlerCloakEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<DesertProwlerPlayer>().desertProwlerSet = true;
            }
        }
        public class LuxorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().luxorsGift = true;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<Luxor>()] < 1 && !player.dead)
                {
                    Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<Luxor>(), 0, 0f, player.whoAmI);
                }
            }
        }
        public class DimeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                // Apply buff once, without resetting timer every tick
                if (!player.HasBuff(ModContent.BuffType<GoldieBuff>()))
                    player.AddBuff(ModContent.BuffType<GoldieBuff>(), 2); // 2 ticks, auto-refreshes

                // Proper minion damage scaling
                int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(20);

                // Spawn minion only if missing
                if (player.ownedProjectileCounts[ModContent.ProjectileType<GoldiePet>()] <= 0)
                {
                    Projectile.NewProjectileDirect(
                        player.GetSource_FromThis(),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<GoldiePet>(),
                        damage,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }
    }
}