using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory.ElectricGuitar;
using SpiritMod.Items.Armor.Masks;
using SpiritMod.Items.BossLoot.DuskingDrops.DuskArmor;
using SpiritMod.Items.DonatorItems;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.CryoliteSet.CryoliteArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.CryoliteEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class DuskEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(132, 77, 244);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DuskRunes>(Item);
            if (player.AddEffect<DuskGuitar>(Item))
            {
                ModContent.GetInstance<ElectricGuitar>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DuskHood>(1);
            recipe.AddIngredient<DuskPlate>(1);
            recipe.AddIngredient<DuskLeggings>(1);
            recipe.AddIngredient<BladeofYouKai>(1);
            recipe.AddIngredient<ElectricGuitar>(1);
            recipe.AddIngredient<DuskingPainting>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class DuskRunes : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DuskEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DuskHood>().UpdateArmorSet(player);
            }
        }
        public class DuskGuitar : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DuskEnchant>();
        }
    }
}