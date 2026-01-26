using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Aerospec;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Buffs;
using ThoriumMod.Buffs.Pet;
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.BossQueenJellyfish;
using ThoriumMod.Items.Coral;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Projectiles.LightPets;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CoralEnchant : BaseEnchant
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
            Item.rare = 1;
            Item.value = 40000;
        }
        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CoralEffect>(Item);
            player.AddEffect<CoralHelmEffect>(Item);
            if (player.AddEffect<SeaBreezeEffect>(Item))
            {
                ModContent.GetInstance<SeaBreezePendant>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BubbleEffect>(Item))
            {
                ModContent.GetInstance<BubbleMagnet>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DavyJonesEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(Item);

                if (player.FindBuffIndex(ModContent.BuffType<DavyJonesLockBoxBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<DavyJonesLockBoxBuff>(), 3600);
                }

                // Check the same projectile type you spawn
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DavyJonesLockBoxPro>()] < 1)
                {
                    int baseDamage = player.ApplyArmorAccDamageBonusesTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);

                    int projIndex = Projectile.NewProjectile(
                        source_ItemUse,
                        player.Center.X,
                        player.Center.Y,
                        0f,
                        -1f,
                        ModContent.ProjectileType<DavyJonesLockBoxPro>(),
                        damage,
                        0f,
                        player.whoAmI
                    );

                    if (Main.projectile.IndexInRange(projIndex))
                    {
                        Main.projectile[projIndex].originalDamage = baseDamage;
                    }
                }
            }
        }
        public class CoralEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CoralEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdate(Player player)
            {
                if (Main.gameMenu) return;

                player.wet = true;
                player.wetCount = 10;
                player.dripping = true;
            }
        }
        public class CoralHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CoralEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CoralHelmet>().UpdateArmorSet(player);
            }
        }
        public class SeaBreezeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CoralEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class BubbleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CoralEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class DavyJonesEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CoralEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CoralHelmet>());
            recipe.AddIngredient(ModContent.ItemType<CoralChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<CoralGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SeaBreezePendant>());
            recipe.AddIngredient(ModContent.ItemType<BubbleMagnet>());
            recipe.AddIngredient(ItemID.Swordfish);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
