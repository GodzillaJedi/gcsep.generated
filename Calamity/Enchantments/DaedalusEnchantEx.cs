using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Daedalus;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DaedalusEnchantEx : BaseEnchant
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
        public override Color nameColor => new(132, 212, 246);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OrnateEffect>(Item);
            player.AddEffect<FrostFlareEffect>(Item);
            player.AddEffect<DaedalusArmorEffect>(Item);
            player.AddEffect<DaeadlusCrystalEffect>(Item);
            player.AddEffect<DaedalusEffect>(Item);
        }
        public class FrostFlareEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().frostFlare = true;
            }
        }
        public class DaedalusArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DaedalusHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusHeadBard>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusHeadBard>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusCowl>().UpdateArmorSet(player);
                ModContent.GetInstance<DaedalusHat>().UpdateArmorSet(player);
            }
        }
        public class DaeadlusCrystalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DaedalusHeadSummon>().UpdateArmorSet(player);
            }
        }
        public class OrnateEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<DaedalusEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().DashID = OrnateShieldDash.ID;
                player.dashType = 0;
                player.buffImmune[46] = true;
                player.buffImmune[47] = true;
                player.buffImmune[44] = true;
                player.buffImmune[324] = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusLeggings>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<DaedalusHeadBard>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<DaedalusCowl>());
                recipe.AddIngredient(ModContent.ItemType<DaedalusHat>());
            }
            recipe.AddIngredient(ModContent.ItemType<OrnateShield>());
            recipe.AddIngredient(ModContent.ItemType<FrostFlare>());
            recipe.AddIngredient(ModContent.ItemType<DaedalusEnchant>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
