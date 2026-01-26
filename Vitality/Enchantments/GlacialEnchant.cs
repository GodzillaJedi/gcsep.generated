using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Glacial;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class GlacialEnchant : BaseEnchant
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
        public override Color nameColor => Color.Azure;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GlacialHelmEffect>(Item);
            if (player.AddEffect<IcyShoesEffect>(Item))
            {
                ModContent.GetInstance<IceShoes>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<IcyMedalEffect>(Item))
            {
                ModContent.GetInstance<IceboundMedallion>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<StonyIceEffect>(Item))
            {
                ModContent.GetInstance<IceStone>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<ArcticEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GlacialHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GlacialPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<GlacialGreaves>());
            recipe.AddIngredient(ModContent.ItemType<IceShoes>());
            recipe.AddIngredient(ModContent.ItemType<IceboundMedallion>());
            recipe.AddIngredient(ModContent.ItemType<IceStone>());
            recipe.AddIngredient(ModContent.ItemType<ArcticEnchant>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class GlacialHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GlacialEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GlacialHelmet>().UpdateArmorSet(player);
            }
        }
        public class IcyShoesEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GlacialEnchant>();
        }
        public class IcyMedalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GlacialEnchant>();
        }
        public class StonyIceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GlacialEnchant>();
        }
    }
}
