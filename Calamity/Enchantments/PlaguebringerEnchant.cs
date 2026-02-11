using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.Mollusk;
using CalamityMod.Items.Armor.Plaguebringer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
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
            player.AddEffect<ViriliEffect>(Item);
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
            public static int BeeMinionDamage = 25;

            public override void PostUpdateEquips(Player player)
            {
                var cal = player.Calamity();

                // Enable Calamity set bonus
                cal.plaguebringerPatronSet = true;

                // Calamity handles dash logic internally — do NOT touch player.dashType
                cal.DashID = PlaguebringerArmorDash.ID;

                // Only local player handles buff + minion
                if (player.whoAmI != Main.myPlayer)
                    return;

                // Apply buff without resetting timer every tick
                int buffType = ModContent.BuffType<LilPlaguebringerBuff>();
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 2);

                // Spawn minion if missing
                int projType = ModContent.ProjectileType<PlaguebringerSummon>();
                if (player.ownedProjectileCounts[projType] <= 0)
                {
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(BeeMinionDamage);

                    var proj = Projectile.NewProjectileDirect(
                        player.GetSource_FromThis(),
                        player.Center,
                        new Vector2(0f, -1f),
                        projType,
                        damage,
                        0f,
                        player.whoAmI
                    );

                    proj.originalDamage = BeeMinionDamage;
                }

                // Lighting effect is fine
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
        public class ViriliEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                int buffType = ModContent.BuffType<ViriliBuff>();

                // Apply buff without resetting timer every tick
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 2);

                // Only spawn projectile on local player
                if (player.whoAmI != Main.myPlayer)
                    return;

                int projType = ModContent.ProjectileType<PlaguePrincess>();

                if (player.ownedProjectileCounts[projType] <= 0)
                {
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(140f);

                    var proj = Projectile.NewProjectileDirect(
                        player.GetSource_FromThis(),
                        player.Center,
                        -Vector2.UnitY,
                        projType,
                        damage,
                        0f,
                        player.whoAmI
                    );

                    proj.originalDamage = damage;
                }
            }
        }
    }
}
