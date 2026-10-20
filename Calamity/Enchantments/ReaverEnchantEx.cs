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
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>();
            public override bool MinionEffect => true; 
            public override void PostUpdateEquips(Player player)
            {
                var cp = player.Calamity();
                var cse = player.CSE();

                cse.rOrb = true;

                if (!cp.reaverExplore && !cse.rOrb)
                    return;

                int buffType = ModContent.BuffType<ReaverOrbBuff>();
                int projType = ModContent.ProjectileType<ReaverOrb>();

                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.whoAmI != Main.myPlayer)
                    return;

                if (player.ownedProjectileCounts[projType] <= 0 && !player.dead)
                {
                    var source = GetSource_EffectItem(player);

                    Projectile.NewProjectileDirect(
                        source,
                        player.Center,
                        Vector2.Zero,
                        projType,
                        0,
                        0f,
                        player.whoAmI
                    );
                }
            }

            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<ReaverOrbEffect>())
                {
                    player.CSE().rOrb = false;

                    int buffType = ModContent.BuffType<ReaverOrbBuff>();
                    int projType = ModContent.ProjectileType<ReaverOrb>();

                    int buffIndex = player.FindBuffIndex(buffType);
                    if (buffIndex != -1)
                    {
                        player.DelBuff(buffIndex);
                    }

                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == player.whoAmI && p.type == projType)
                        {
                            p.Kill();
                        }
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
                ModContent.GetInstance<NecklaceofVexation>().UpdateAccessory(player, true);
            }
        }
        public class DewEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<ReaverEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LivingDew>().UpdateAccessory(player, true);
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

