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
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Arctic;
using static gcsep.Vitality.Enchantments.ArcticEnchant;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class BloodkeeperEnchant : BaseEnchant
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
        public override Color nameColor => Color.Crimson;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BloodkeeperEffect>(Item);
            if (player.AddEffect<PinHeartEffect>(Item))
            {
                ModContent.GetInstance<PinnedHeart>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BloodMagnetEffect>(Item))
            {
                ModContent.GetInstance<BloodMagnet>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodkeeperHelmet>());
            recipe.AddIngredient(ModContent.ItemType<BloodkeeperChestplate>());
            recipe.AddIngredient(ModContent.ItemType<BloodkeeperLeggings>());
            recipe.AddIngredient(ModContent.ItemType<PinnedHeart>());
            recipe.AddIngredient(ModContent.ItemType<BloodMagnet>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class BloodkeeperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodkeeperEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BloodkeeperHelmet>().UpdateArmorSet(player);
            }
        }
        public class PinHeartEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodkeeperEnchant>();
        }
        public class BloodMagnetEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodkeeperEnchant>();
        }
    }
}
