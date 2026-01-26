using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LivingWoodEnchant : BaseEnchant
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
            if (player.AddEffect<LivingWoodEffect>(Item))
            {
                player.GetThoriumPlayer().setLivingWood = true;
                IEntitySource source_ItemUse = player.GetSource_ItemUse(Item);

                if (player.FindBuffIndex(ModContent.BuffType<LivingWoodAcornBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<LivingWoodAcornBuff>(), 3600);
                }

                // Check the same projectile type you spawn
                if (player.ownedProjectileCounts[ModContent.ProjectileType<LivingWoodAcornPro>()] < 1)
                {
                    int baseDamage = player.ApplyArmorAccDamageBonusesTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);

                    int projIndex = Projectile.NewProjectile(
                        source_ItemUse,
                        player.Center.X,
                        player.Center.Y,
                        0f,
                        -1f,
                        ModContent.ProjectileType<LivingWoodAcornPro>(),
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
        public class LivingWoodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingWoodEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LivingWoodMask>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodChestguard>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodBoots>());
            recipe.AddIngredient(ModContent.ItemType<LivingLeaf>(), 39);
            recipe.AddIngredient(ModContent.ItemType<LivingWoodAcorn>());
            recipe.AddIngredient(ModContent.ItemType<ChiTea>(), 5);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
