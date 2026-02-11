using CalamityBardHealer;
using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Armor.Statigel;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using RagnarokMod.Utils;
using gcsep.Content.SoulToggles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class SilvaEnchant : BaseEnchant
    {
        private Mod calamity;
        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
                calamity = ModLoader.GetMod("CalamityMod");
        }
        public override Color nameColor => new(176, 112, 70);

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
            player.AddEffect<TreeAmuletEffect>(Item);
            player.AddEffect<ElementalEffect>(Item);
            player.AddEffect<AbsorberEffect>(Item);
            player.AddEffect<DynamoEffect>(Item);
            player.AddEffect<BlunderBoostEffect>(Item);
            player.AddEffect<SilvaArmorEffect>(Item);
            player.AddEffect<SilvaCrystalEffect>(Item);
        }
        public class SilvaCrystalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SilvaHeadSummon>().UpdateArmorSet(player);
            }
        }
        public class SilvaArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>(); // Or your appropriate header
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>(); // Replace with your toggle item
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SilvaHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<SilvaHeadHealer>().UpdateArmorSet(player);
                ModContent.GetInstance<SilvaGuardianHelmet>().UpdateArmorSet(player);
            }
        }
        public class TreeAmuletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage(ThoriumDamageBase<BardDamage>.Instance) += 0.12f;
                player.GetCritChance(ThoriumDamageBase<BardDamage>.Instance) += 12f;
                player.GetAttackSpeed(ThoriumDamageBase<BardDamage>.Instance) += 0.12f;
                player.GetModPlayer<ThoriumPlayer>().accBrassMute2 = true;
                player.GetModPlayer<ThoriumPlayer>().accWindHoming = true;
                player.GetModPlayer<ThoriumPlayer>().bardBounceBonus += 2;
                player.GetModPlayer<ThoriumPlayer>().accPercussionTuner2 = true;
            }
        }
        public class ElementalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                // Flat stat bonuses
                player.statLifeMax2 += 40;
                player.statManaMax2 += 20;

                // Thorium players
                var thorium = player.GetModPlayer<ThoriumPlayer>();
                var thorlamity = player.GetModPlayer<ThorlamityPlayer>();

                // Healer tool attack speed
                player.GetAttackSpeed(ThoriumDamageBase<HealerTool>.Instance) += 0.15f;

                // Healer bonuses
                thorium.healBonus += 2;
                thorium.accHexingTalisman = true;

                // Custom Thorlamity effect
                thorlamity.elementalBloom = true;

                // Healer damage bonuses
                player.GetDamage(ThoriumDamageBase<HealerDamage>.Instance) += 0.20f;
                player.GetAttackSpeed(ThoriumDamageBase<HealerDamage>.Instance) += 0.10f;
                player.GetCritChance(ThoriumDamageBase<HealerDamage>.Instance) += 12f;
            }
        }
        public class AbsorberEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                player.noKnockback = true;
                calamityPlayer.absorber = true;
            }
        }
        public class DynamoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().dynamoStemCells = true;
                player.GetDamage<RangedDamageClass>() += 0.1f;
                player.moveSpeed += 0.1f;
            }
        }
        public class BlunderBoostEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilvaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().hasJetpack = true;
                player.GetDamage<ThrowingDamageClass>() += 0.12f;
                player.Calamity().rogueVelocity += 0.15f;
                player.Calamity().blunderBooster = true;
                player.Calamity().stealthGenStandstill += 0.1f;
                player.Calamity().stealthGenMoving += 0.1f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SilvaHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<SilvaHeadMagic>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<SilvaHeadHealer>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<SilvaGuardianHelmet>());
                recipe.AddIngredient(ModContent.ItemType<ElementalBloom>());
                recipe.AddIngredient(ModContent.ItemType<TreeWhispererAmulet>());
            }
            recipe.AddIngredient(ModContent.ItemType<SilvaArmor>());
            recipe.AddIngredient(ModContent.ItemType<SilvaLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DynamoStemCells>());
            recipe.AddIngredient(ModContent.ItemType<BlunderBooster>());
            recipe.AddIngredient(ModContent.ItemType<TheAbsorber>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
    }
}
