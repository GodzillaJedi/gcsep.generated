using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Accessories;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.Armor;
using VitalityMod.Items.Olympian;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class PaladinEnchant : BaseEnchant
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
        public override Color nameColor => Color.Beige;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PaladinEffect>(Item);
            if (player.AddEffect<SheriffEffect>(Item))
            {
                ModContent.GetInstance<SheriffBadge>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpecstoReadEffects>(Item))
            {
                ModContent.GetInstance<ReadingSpecs>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PaladinHelm>());
            recipe.AddIngredient(ModContent.ItemType<PaladinBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<PaladinLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SheriffBadge>());
            recipe.AddIngredient(ModContent.ItemType<ReadingSpecs>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class PaladinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PaladinEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PaladinHelm>().UpdateArmorSet(player);
            }
        }
        public class SheriffEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PaladinEnchant>();
        }
        public class SpecstoReadEffects : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PaladinEnchant>();
        }
    }
}
