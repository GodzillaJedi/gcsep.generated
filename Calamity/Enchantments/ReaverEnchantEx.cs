using CalamityMod;
using CalamityMod.Buffs.Pets;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Reaver;
using CalamityMod.Projectiles.Typeless;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using gcsep.Content.Buffs;
using gcsep.Content.Projectiles.Enchantments;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class ReaverEnchantEx : BaseEnchant
    {
        public override Color nameColor => new(145, 203, 102);

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<VexationEffect>(Item);
            player.AddEffect<DewEffect>(Item);
            player.AddEffect<ReaverArmorEffect>(Item);
            player.AddEffect<ReaverOrbEffect>(Item);
            player.AddEffect<ReaverEffect>(Item);
        }
        public class ReaverArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>(); // Or your appropriate header
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>(); // Replace with your toggle item

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ReaverHeadExplore>().UpdateArmorSet(player);
                ModContent.GetInstance<ReaverHeadTank>().UpdateArmorSet(player);
                ModContent.GetInstance<ReaverHeadMobility>().UpdateArmorSet(player);
            }
        }
        public class ReaverOrbEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>(); // Or your appropriate header
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>(); // Replace with your toggle item

            public override void PostUpdateEquips(Player player)
            {
                var cp = player.Calamity();
                cp.reaverExplore = true;
                cp.wearingRogueArmor = true;

                player.setBonus = ModContent.GetInstance<ReaverEnchantEx>().GetLocalizedValue("SetBonus");
                player.findTreasure = true;
                player.blockRange += 4;
                player.aggro -= 200;

                if (player.whoAmI == Main.myPlayer)
                {
                    int buffType = ModContent.BuffType<ReaverOrbBuff>();
                    if (player.FindBuffIndex(buffType) == -1)
                    {
                        player.AddBuff(buffType, 3600);
                    }

                    int projType = ModContent.ProjectileType<ReaverOrb>();
                    if (player.ownedProjectileCounts[projType] < 1)
                    {
                        var source = player.GetSource_Misc("ReaverExploreEffect");
                        Projectile.NewProjectile(source, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
                    }
                }
            }
        }
        public class VexationEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().vexation = true;
            }
        }
        public class DewEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.statLifeMax2 += 50;
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.alwaysHoneyRegen = true;
                calamityPlayer.honeyTurboRegen = true;
                calamityPlayer.honeyDewHalveDebuffs = true;
                calamityPlayer.livingDewHalveDebuffs = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ReaverHeadExplore>());
            recipe.AddIngredient(ModContent.ItemType<ReaverHeadTank>());
            recipe.AddIngredient(ModContent.ItemType<ReaverHeadMobility>());
            recipe.AddIngredient(ModContent.ItemType<ReaverScaleMail>());
            recipe.AddIngredient(ModContent.ItemType<ReaverCuisses>());
            recipe.AddIngredient(ModContent.ItemType<NecklaceofVexation>());
            recipe.AddIngredient(ModContent.ItemType<LivingDew>());
            recipe.AddIngredient(ModContent.ItemType<ReaverEnchant>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}

