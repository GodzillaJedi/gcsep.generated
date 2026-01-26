using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.Plaguebringer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class PlaguebringerEnchant : BaseEnchant
    {
        private Mod calamity;

        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
                calamity = ModLoader.GetMod("CalamityMod");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(0, 255, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<VriliEffect>(Item);
            player.AddEffect<TheBeeEffect>(Item);
            player.AddEffect<PlaguebringerEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerVisor>());
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerCarapace>());
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerPistons>());
            recipe.AddIngredient(ModContent.ItemType<InfectedRemote>());
            recipe.AddIngredient(ModContent.ItemType<TheBee>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PlaguebringerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.setBonus = ModContent.GetInstance<PlaguebringerVisor>().GetLocalization("SetBonus").Format(this);
                player.Calamity().plaguebringerPatronSet = true;
                player.Calamity().DashID = PlaguebringerArmorDash.ID;
                player.dashType = 0;
                player.maxMinions += 3;
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_ItemUse(EffectItem(player));
                    if (player.FindBuffIndex(ModContent.BuffType<LilPlaguebringerBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<LilPlaguebringerBuff>(), 3600);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<PlaguebringerSummon>()] < 1)
                    {
                        int num = player.ApplyArmorAccDamageBonusesTo(25f);
                        int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(num);
                        int num2 = Projectile.NewProjectile(source_ItemUse, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<PlaguebringerSummon>(), damage, 0f, player.whoAmI);
                        if (Main.projectile.IndexInRange(num2))
                        {
                            Main.projectile[num2].originalDamage = num;
                        }
                    }
                }

                Lighting.AddLight(player.Center, 0f, 0.39f, 0.24f);
            }
        }
        public class TheBeeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().theBee = true;
            }
        }
        public class VriliEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                int buffType = ModContent.BuffType<ViriliBuff>();
                if (player.FindBuffIndex(buffType) == -1)
                {
                    player.AddBuff(buffType, 3600);
                }

                // Only spawn projectile on local player
                if (player.whoAmI != Main.myPlayer)
                    return;

                int projType = ModContent.ProjectileType<PlaguePrincess>();
                if (player.ownedProjectileCounts[projType] < 1)
                {
                    int damage = player.ApplyArmorAccDamageBonusesTo(140f);
                    var source = player.GetSource_Misc("VriliEffect");
                    var proj = Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, projType, damage, 0f, player.whoAmI);
                    proj.originalDamage = damage;
                }
            }
        }
    }
}
