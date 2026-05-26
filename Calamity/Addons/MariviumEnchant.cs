using CalamityEntropy.Content.Items.Accessories;
using CalamityEntropy.Content.Items.Armor.Marivinium;
using CalamityEntropy.Content.Items.Weapons;
using CalamityEntropy.Content.Rarities;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Entropy.Name)]
    [JITWhenModsEnabled(ModCompatibility.Entropy.Name)]
    public class MariviumEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ModContent.RarityType<AbyssalBlue>();
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HolyMantleEffect>(Item);
            player.AddEffect<WyrmToothEffect>(Item);
            player.AddEffect<MarviumEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MariviniumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<MariviniumBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<MariviniumLeggings>());
            recipe.AddIngredient(ModContent.ItemType<Xytheron>());
            recipe.AddIngredient(ModContent.ItemType<WyrmToothNecklace>());
            recipe.AddIngredient(ModContent.ItemType<HolyMantle>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public class HolyMantleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MariviumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HolyMantle>().UpdateAccessory(player, true);
            }
        }
        public class WyrmToothEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MariviumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<WyrmToothNecklace>().UpdateAccessory(player, true);
            }
        }
        public class MarviumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MariviumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MariviniumHelmet>().UpdateArmorSet(player);
            }
        }
    }
}
