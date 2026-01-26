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
using VitalityMod.Buffs;
using VitalityMod.Items.GlowingMoss;
using VitalityMod.Items.LivingWood;
using VitalityMod.Items.Martian;
using VitalityMod.Projectiles.Minions;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class MartianGunnerEnchant : BaseEnchant
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
        public override Color nameColor => Color.AliceBlue;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MartianGunnerEffect>(Item);
            if (player.AddEffect<XenoniteCluster>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(Item);

                if (player.FindBuffIndex(ModContent.BuffType<XenoniteStaffBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<XenoniteStaffBuff>(), 3600);
                }

                // Check the same projectile type you spawn
                if (player.ownedProjectileCounts[ModContent.ProjectileType<XenoniteStaffProj>()] < 1)
                {
                    int baseDamage = player.ApplyArmorAccDamageBonusesTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);

                    int projIndex = Projectile.NewProjectile(
                        source_ItemUse,
                        player.Center.X,
                        player.Center.Y,
                        0f,
                        -1f,
                        ModContent.ProjectileType<XenoniteStaffProj>(),
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
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MartianGunnerHelmet>());
            recipe.AddIngredient(ModContent.ItemType<MartianGunnerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<MartianGunnerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<XenoniteClusterStaff>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class MartianGunnerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MartianGunnerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LivingWoodHood>().UpdateArmorSet(player);
            }
        }
        public class XenoniteCluster : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MartianGunnerEnchant>();
            public override bool MinionEffect => true;
        }
    }
}
