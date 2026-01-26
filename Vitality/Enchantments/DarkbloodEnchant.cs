using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Accessories;
using VitalityMod.BloodHunter.Armor;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class DarkbloodEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkRed;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DarkbloodEffect>(Item);
            if (player.AddEffect<DarkbloodMagnetEffect>(Item))
            {
                ModContent.GetInstance<DarkbloodMagnet>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GuideToBlood>(Item))
            {
                ModContent.GetInstance<GuidetoBloodChanneling>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BloodEmblemEffect>(Item))
            {
                ModContent.GetInstance<BloodHunterEmblem>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkbloodHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DarkbloodBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DarkbloodLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DarkbloodMagnet>());
            recipe.AddIngredient(ModContent.ItemType<GuidetoBloodChanneling>());
            recipe.AddIngredient(ModContent.ItemType<BloodHunterEmblem>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class DarkbloodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DarkbloodEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DarkbloodHelmet>().UpdateArmorSet(player);
            }
        }
        public class DarkbloodMagnetEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DarkbloodEnchant>();
        }
        public class GuideToBlood : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DarkbloodEnchant>();
        }
        public class BloodEmblemEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DarkbloodEnchant>();
        }
    }
}
