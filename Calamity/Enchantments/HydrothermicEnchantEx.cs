using CalamityBardHealer.Items;
using CalamityHunt.Content.Items.Accessories;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Hydrothermic;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class HydrothermicEnchantEx : BaseEnchant
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

        public override Color nameColor => new(248, 182, 89);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ApolloEffect>(Item);
            player.AddEffect<EtherealEffect>(Item);
            player.AddEffect<HydrothermicArmorsEffect>(Item);
            player.AddEffect<HydrothermicVentEffect>(Item);
            ModContent.GetInstance<HydrothermicEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicHeadRogue>());
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<HydrothermicHat>());
                recipe.AddIngredient(ModContent.ItemType<HydrothermicGasMask>());
            }
            recipe.AddIngredient(ModContent.ItemType<HydrothermicArmor>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicSubligar>());
            recipe.AddIngredient(ModContent.ItemType<EtherealExtorter>());
            recipe.AddIngredient(ModContent.ItemType<Apollyon>());
            recipe.AddIngredient(ModContent.ItemType<HydrothermicEnchant>());
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class ApolloEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<HydrothermicEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<Apollyon>().UpdateAccessory(player, true);
            }
        }
        public class EtherealEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<HydrothermicEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<EtherealExtorter>().UpdateAccessory(player, true);
            }
        }
        public class HydrothermicArmorsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<HydrothermicEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HydrothermicHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<HydrothermicHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<HydrothermicHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<HydrothermicHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<HydrothermicHat>().UpdateArmorSet(player);
                ModContent.GetInstance<HydrothermicGasMask>().UpdateArmorSet(player);
            }
        }
        public class HydrothermicVentEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<HydrothermicEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HydrothermicHeadSummon>().UpdateArmorSet(player);
            }
        }
    }
}
