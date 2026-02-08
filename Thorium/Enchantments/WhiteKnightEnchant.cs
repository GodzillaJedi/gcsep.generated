using CalamityMod;
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
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class WhiteKnightEnchant : BaseEnchant
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
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<WhiteMaskEffect>(Item))
            {
                player.GetThoriumPlayer().setWhiteKnight = true;
            }
            if (player.AddEffect<MurkyCatalystEffect>(Item))
            {
                ModContent.GetInstance<MurkyCatalyst>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ValkyrieBladeEffect>(Item))
            {
                // Ensure the buff is applied if not already present
                if (!player.HasBuff<ValkyrieBladeBuff>())
                {
                    player.AddBuff(ModContent.BuffType<ValkyrieBladeBuff>(), 3600);
                }

                // Spawn the projectile only if the player doesn't already own one
                if (player.ownedProjectileCounts[ModContent.ProjectileType<ValkyrieBladePro>()] < 1)
                {
                    IEntitySource source = player.GetSource_ItemUse(Item);

                    int baseDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(25f);
                    int totalDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);

                    int projIndex = Projectile.NewProjectile(
                        source,
                        player.Center,
                        new Vector2(0f, -1f),
                        ModContent.ProjectileType<ValkyrieBladePro>(),
                        totalDamage,
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
        public class WhiteMaskEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WhiteKnightEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<WhiteKnightMask>().UpdateArmorSet(player);
            }
        }
        public class MurkyCatalystEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WhiteKnightEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ValkyrieBladeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WhiteKnightEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WhiteKnightMask>());
            recipe.AddIngredient(ModContent.ItemType<WhiteKnightTabard>());
            recipe.AddIngredient(ModContent.ItemType<WhiteKnightLeggings>());
            recipe.AddIngredient(ModContent.ItemType<MurkyCatalyst>());
            recipe.AddIngredient(ModContent.ItemType<PrismiteGemLock>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}