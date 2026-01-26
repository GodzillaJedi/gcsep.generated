using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Armor;
using VitalityMod.Items.Accessories;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class EbonwitchEnchant : BaseEnchant
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
        public override Color nameColor => Color.Indigo;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<EbonwitchEffect>(Item);
            if (player.AddEffect<ObsidianSpearEffect>(Item))
            {
                ModContent.GetInstance<ObsidianSpearhead>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PutridEyesEffect>(Item))
            {
                ModContent.GetInstance<PutridEye>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EbonwitchHat>());
            recipe.AddIngredient(ModContent.ItemType<EbonwitchCoat>());
            recipe.AddIngredient(ModContent.ItemType<EbonwitchLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ObsidianSpearhead>());
            recipe.AddIngredient(ModContent.ItemType<PutridEye>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class EbonwitchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EbonwoodEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<EbonwitchHat>().UpdateArmorSet(player);
            }
        }
        public class ObsidianSpearEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EbonwoodEnchant>();
        }
        public class PutridEyesEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EbonwoodEnchant>();
        }
    }
}
