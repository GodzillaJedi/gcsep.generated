using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.DesertProwler;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Weapons.Ranged;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;

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
            }
        }
        public class DimeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DesertProwlerEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<GoldieBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<GoldieBuff>(), 3600, true, false);
                    }
                    const int damage = 100;
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<GoldiePet>()] < 1)
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<GoldiePet>(), damage, 8f, player.whoAmI);
                }
            }
        }
    }
}