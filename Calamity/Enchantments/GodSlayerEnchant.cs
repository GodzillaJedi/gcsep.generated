using CalamityBardHealer;
using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Accessories;
using RagnarokMod.Items.BardItems.Armor;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Utilities;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class GodSlayerEnchant : BaseEnchant
    {
        private Mod calamity;
        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
                calamity = ModLoader.GetMod("CalamityMod");
        }

        public override Color nameColor => new(100, 108, 156);

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
            player.AddEffect<MechwormEffect>(Item);
            player.AddEffect<OmniSpeakerEffect>(Item);
            player.AddEffect<VeneratedEffect>(Item);
            player.AddEffect<UniversalHeadsetEffect>(Item);
            player.AddEffect<DimSoulEffect>(Item);
            player.AddEffect<GodSlayerEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GodSlayerHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerHeadRogue>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<UniversalHeadset>());
                recipe.AddIngredient(ModContent.ItemType<GodSlayerHeadBard>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<GodSlayerDeathsingerCowl>());
                recipe.AddIngredient(ModContent.ItemType<OmniSpeaker>());
            }
            recipe.AddIngredient(ModContent.ItemType<GodSlayerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<GodSlayerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<VeneratedLocket>());
            recipe.AddIngredient(ModContent.ItemType<DimensionalSoulArtifact>());
            recipe.AddIngredient(ModContent.ItemType<StaffoftheMechworm>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class GodSlayerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GodSlayerHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<GodSlayerHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<GodSlayerHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<GodSlayerDeathsingerCowl>().UpdateArmorSet(player);
                ModContent.GetInstance<GodSlayerHeadBard>().UpdateArmorSet(player);
            }
        }
        public class OmniSpeakerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage(ThoriumDamageBase<BardDamage>.Instance) += 0.15f;
                player.GetCritChance(ThoriumDamageBase<BardDamage>.Instance) += 15f;
                player.GetAttackSpeed(ThoriumDamageBase<BardDamage>.Instance) += 0.15f;
                player.GetModPlayer<ThoriumPlayer>().accBrassMute2 = true;
                player.GetModPlayer<ThoriumPlayer>().accWindHoming = true;
                player.GetModPlayer<ThoriumPlayer>().bardBounceBonus += 3;
                player.GetModPlayer<ThorlamityPlayer>().omniSpeaker = true;
                player.GetModPlayer<ThoriumPlayer>().bardRangeBoost += 750;
                player.GetModPlayer<ThoriumPlayer>().bardBuffDuration += 120;
                player.GetModPlayer<ThoriumPlayer>().accPercussionTuner2 = true;
            }
        }
        public class VeneratedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage<ThrowingDamageClass>() += 0.1f;
                player.Calamity().veneratedLocket = true;
            }
        }
        public class MechwormEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                IEntitySource source = player.GetSource_Misc("MechwormEffect");

                // Apply buff if not already active
                if (player.FindBuffIndex(ModContent.BuffType<Mechworm>()) == -1)
                    player.AddBuff(ModContent.BuffType<Mechworm>(), 3600);

                // Spawn head
                int headIndex = -1;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MechwormHead>()] < 1)
                {
                    int damage = player.ApplyArmorAccDamageBonusesTo(140f);
                    var head = Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY,
                        ModContent.ProjectileType<MechwormHead>(), damage, 0f, player.whoAmI);
                    head.originalDamage = damage;
                    headIndex = head.whoAmI;
                }

                // Spawn body segments
                int previousSegmentIndex = headIndex;
                for (int i = 0; i < 15; i++)
                {
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<MechwormBody>()] < 5)
                    {
                        int damage = player.ApplyArmorAccDamageBonusesTo(140f);
                        Vector2 pos = player.Center + new Vector2(0, -20 * (i + 1));
                        var body = Projectile.NewProjectileDirect(source, pos, -Vector2.UnitY,
                            ModContent.ProjectileType<MechwormBody>(), damage, 0f, player.whoAmI);
                        body.originalDamage = damage;
                        body.ai[0] = previousSegmentIndex;
                        previousSegmentIndex = body.whoAmI;
                    }
                }

                // Spawn tail
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MechwormTail>()] < 1)
                {
                    int damage = player.ApplyArmorAccDamageBonusesTo(140f);
                    Vector2 tailPos = player.Center + new Vector2(0, -20 * 16);
                    var tail = Projectile.NewProjectileDirect(source, tailPos, -Vector2.UnitY,
                        ModContent.ProjectileType<MechwormTail>(), damage, 0f, player.whoAmI);
                    tail.originalDamage = damage;
                    tail.ai[0] = previousSegmentIndex;
                }
            }
        }
        public class UniversalHeadsetEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ThoriumPlayer thoriumPlayer = player.GetThoriumPlayer();
                player.GetDamage(ThoriumDamageBase<BardDamage>.Instance) += 0.2f;
                player.GetCritChance(ThoriumDamageBase<BardDamage>.Instance) += 7f;
                player.GetAttackSpeed(ThoriumDamageBase<BardDamage>.Instance) += 0.1f;
                thoriumPlayer.inspirationRegenBonus += 0.1f;
                thoriumPlayer.bardResourceMax2 += 5;
                thoriumPlayer.accHeadset = true;
            }
        }
        public class DimSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GodSlayerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().dArtifact = true;
            }
        }
    }
}
