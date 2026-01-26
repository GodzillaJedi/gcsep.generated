using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.OmegaBlue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OmegaBlueEnchant : BaseEnchant
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
        public override Color nameColor => new(31, 79, 156);
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TruffleEffect>(Item);
            player.AddEffect<OmegaBlueEffect>(Item);
            player.AddEffect<ReaperEffect>(Item);
            player.AddEffect<OldScaleEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueHelmet>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueChestplate>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueTentacles>());
            recipe.AddIngredient(ModContent.ItemType<OldDukeScales>());
            recipe.AddIngredient(ModContent.ItemType<ReaperToothNecklace>());
            recipe.AddIngredient(ModContent.ItemType<MutatedTruffle>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class TruffleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                int buffType = ModContent.BuffType<MutatedTruffleBuff>();
                if (player.FindBuffIndex(buffType) == -1)
                {
                    player.AddBuff(buffType, 3600);
                }

                // Only spawn projectile on local player
                if (player.whoAmI != Main.myPlayer)
                    return;

                int projType = ModContent.ProjectileType<MutatedTruffleMinion>();
                if (player.ownedProjectileCounts[projType] < 1)
                {
                    int damage = player.ApplyArmorAccDamageBonusesTo(140f);
                    var source = player.GetSource_Misc("TruffleEffect");
                    var proj = Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, projType, damage, 0f, player.whoAmI);
                    proj.originalDamage = damage;
                }
            }
        }
        public class ReaperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage<GenericDamageClass>() += 0.15f;
                player.GetArmorPenetration<GenericDamageClass>() += 15f;
            }
        }
        public class OldScaleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<OldDukeScalesPlayer>().OldDukeScalesOn = true;
            }
        }
        public class OmegaBlueEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                // Grab Calamity's player data
                CalamityPlayer calamityPlayer = player.Calamity();
                // Apply stat bonuses
                player.GetArmorPenetration<GenericDamageClass>() += 15f;
                player.maxMinions += 2;
                // Set Calamity flags
                calamityPlayer.wearingRogueArmor = true;
                calamityPlayer.omegaBlueSet = true;
                calamityPlayer.WearingPostMLSummonerSet = true;
                // Visual dust effect when cooldown is active
                if (calamityPlayer.cooldowns.TryGetValue(global::CalamityMod.Cooldowns.OmegaBlue.ID, out var cooldown)
                    && cooldown.timeLeft > 1500)
                {
                    int dustIndex = Dust.NewDust(player.position, player.width, player.height,
                                                 DustID.PurificationPowder, 0f, 0f, 100,
                                                 Color.Transparent, 1.6f);
                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].noLight = true;
                    Main.dust[dustIndex].fadeIn = 1f;
                    Main.dust[dustIndex].velocity *= 3f;
                }
            }
        }
    }
}
