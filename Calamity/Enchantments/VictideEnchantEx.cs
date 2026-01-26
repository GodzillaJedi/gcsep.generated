using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Victide;
using CalamityMod.Projectiles.Typeless;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class VictideEnchantEx : BaseEnchant
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

        public override Color nameColor => new(255, 233, 197);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FungalEffect>(Item);
            player.AddEffect<ClumpEffect>(Item);
            player.AddEffect<SeaShieldEffect>(Item);
            player.AddEffect<VictideArmorEffect>(Item);
            player.AddEffect<VictideEffect>(Item);
            player.AddEffect<SeaSnailEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VictideHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<VictideHeadRogue>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<VictideHeadBard>());
                recipe.AddIngredient(ModContent.ItemType<VictideHeadHealer>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<VictideAmmoniteHat>());
            }
            recipe.AddIngredient(ModContent.ItemType<VictideEnchant>());
            recipe.AddIngredient(ModContent.ItemType<VictideBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<ShieldoftheOcean>());
            recipe.AddIngredient(ModContent.ItemType<VictideGreaves>());
            recipe.AddIngredient(ModContent.ItemType<FungalClump>());
            recipe.AddIngredient(ModContent.ItemType<FungalSymbiote>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class VictideArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VictideHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<VictideHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<VictideHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<VictideHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<VictideAmmoniteHat>().UpdateArmorSet(player);
                ModContent.GetInstance<VictideHeadBard>().UpdateArmorSet(player);
                ModContent.GetInstance<VictideHeadHealer>().UpdateArmorSet(player);
            }   
        }
        public class SeaSnailEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VictideHeadSummon>().UpdateArmorSet(player);
            }
        }
        public class FungalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().fungalSymbiote = true;
                int num = (int)player.Center.X / 16;
                int num2 = (int)(player.Bottom.Y - 1f) / 16;
                Tile tile = CalamityUtils.ParanoidTileRetrieval(num, num2 + 1);
                if (player.whoAmI != Main.myPlayer || player.velocity.Y != 0f || player.grappling[0] != -1)
                {
                    return;
                }

                Tile tile2 = CalamityUtils.ParanoidTileRetrieval(num, num2);
                if (tile2.HasTile || tile2.LiquidAmount != 0 || !(tile != null) || !WorldGen.SolidTile(tile))
                {
                    return;
                }

                tile2.TileFrameY = 0;
                tile2.Get<TileWallWireStateData>().Slope = SlopeType.Solid;
                tile2.Get<TileWallWireStateData>().IsHalfBlock = false;
                if (tile.TileType == 0)
                {
                    if (Main.rand.NextBool(1000))
                    {
                        tile2.Get<TileWallWireStateData>().HasTile = true;
                        tile2.TileType = TileID.DyePlants;
                        tile2.TileFrameX = (short)((!Main.rand.NextBool()) ? 34 : 0);
                    }

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendTileSquare(-1, num, num2, 1);
                    }
                }
                else if (tile.TileType == 2)
                {
                    tile2.Get<TileWallWireStateData>().HasTile = true;
                    tile2.TileType = TileID.Plants;
                    tile2.TileFrameX = 144;
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendTileSquare(-1, num, num2, 1);
                    }
                }
                else if (tile.TileType == 109)
                {
                    tile2.Get<TileWallWireStateData>().HasTile = true;
                    tile2.TileType = TileID.HallowedPlants;
                    tile2.TileFrameX = 144;
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendTileSquare(-1, num, num2, 1);
                    }
                }
                else if (tile.TileType == 23)
                {
                    tile2.Get<TileWallWireStateData>().HasTile = true;
                    tile2.TileType = TileID.CorruptPlants;
                    tile2.TileFrameX = 144;
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendTileSquare(-1, num, num2, 1);
                    }
                }
                else if (tile.TileType == 199)
                {
                    tile2.Get<TileWallWireStateData>().HasTile = true;
                    tile2.TileType = TileID.CrimsonPlants;
                    tile2.TileFrameX = 270;
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendTileSquare(-1, num, num2, 1);
                    }
                }
                else if (tile.TileType == 70)
                {
                    tile2.Get<TileWallWireStateData>().HasTile = true;
                    tile2.TileType = TileID.MushroomPlants;
                    tile2.TileFrameX = (short)(Main.rand.Next(5) * 18);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendTileSquare(-1, num, num2, 1);
                    }
                }
            }
        }
        public class ClumpEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().fungalClump = true;
                if (player.whoAmI != Main.myPlayer)
                {
                    return;
                }

                if (player.FindBuffIndex(ModContent.BuffType<FungalClumpBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<FungalClumpBuff>(), 3600);
                }

                if (player.ownedProjectileCounts[ModContent.ProjectileType<FungalClumpMinion>()] < 1)
                {
                    IEntitySource source_Accessory = player.GetSource_Misc("ClumpEffect");
                    int num = player.ApplyArmorAccDamageBonusesTo(10f);
                    int num2 = Projectile.NewProjectile(Damage: (int)player.GetBestClassDamage().ApplyTo(num), spawnSource: source_Accessory, X: player.Center.X, Y: player.Center.Y, SpeedX: 0f, SpeedY: -1f, Type: ModContent.ProjectileType<FungalClumpMinion>(), KnockBack: 1f, Owner: player.whoAmI);
                    if (Main.projectile.IndexInRange(num2))
                    {
                        Main.projectile[num2].originalDamage = num;
                    }
                }
            }
        }
        public class SeaShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<VictideEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
                {
                    player.statDefense += 5;
                }

                if ((player.armor[0].type == ModContent.ItemType<VictideHeadMagic>() || player.armor[0].type == ModContent.ItemType<VictideHeadSummon>() || player.armor[0].type == ModContent.ItemType<VictideHeadMelee>() || player.armor[0].type == ModContent.ItemType<VictideHeadRanged>() || player.armor[0].type == ModContent.ItemType<VictideHeadRogue>()) && player.armor[1].type == ModContent.ItemType<VictideBreastplate>() && player.armor[2].type == ModContent.ItemType<VictideGreaves>())
                {
                    player.moveSpeed += 0.1f;
                    player.lifeRegen += 2;
                }
            }
        }
    }
}