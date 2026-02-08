using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Aerospec;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AerospecEnchantEx : BaseEnchant
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
        public override Color nameColor => new(153, 200, 193);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<GladiatorsLocketEffect>(Item))
            {
                float num = 0.2f;
                float num2 = 0.2f;
                player.Calamity().gladiatorSword = true;
                player.GetDamage<GenericDamageClass>() += num - num * (float)player.statLife / (float)player.statLifeMax2;
                player.moveSpeed += num2 - num2 * (float)player.statLife / (float)player.statLifeMax2;
            }
            if (player.AddEffect<UnstableGraniteEffect>(Item))
            {
                player.Calamity().unstableGraniteCore = true;
            }
            if (player.AddEffect<FeatherCrownEffect>(Item))
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.rogueVelocity += 0.15f;
                calamityPlayer.featherCrown = true;
            }
            player.AddEffect<AerospecArmorEffects>(Item);
            player.AddEffect<ValkyrieMinionEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AerospecHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<AerospecHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<AerospecBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<AerospecLeggings>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<AerospecBard>());
                recipe.AddIngredient(ModContent.ItemType<AerospecHealer>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<AerospecBiretta>());
                recipe.AddIngredient(ModContent.ItemType<AerospecHeadphones>());
            }
            recipe.AddIngredient(ModContent.ItemType<FeatherCrown>());
            recipe.AddIngredient(ModContent.ItemType<GladiatorsLocket>());
            recipe.AddIngredient(ModContent.ItemType<UnstableGraniteCore>());
            recipe.AddIngredient(ModContent.ItemType<AerospecEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class ValkyrieMinionEffect : AccessoryEffect 
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AerospecHeadSummon>().UpdateArmorSet(player);
            }
        }
        public class AerospecArmorEffects : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AerospecHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<AerospecHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<AerospecHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<AerospecHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<AerospecBiretta>().UpdateArmorSet(player);
                ModContent.GetInstance<AerospecHeadphones>().UpdateArmorSet(player);
                ModContent.GetInstance<AerospecBard>().UpdateArmorSet(player);
                ModContent.GetInstance<AerospecHealer>().UpdateArmorSet(player);
            }
        }
        public class GladiatorsLocketEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
        }
        public class UnstableGraniteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
        }
        public class FeatherCrownEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<AerospecEnchantEx>();
        }
    }
}