using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Armor.LeatherArmor;
using SpiritMod.Items.Sets.MarbleSet.MarbleArmor;
using SpiritMod.Items.Weapon.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class MarksmanEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(206, 182, 95);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 4f;
            player.AddEffect<MarksmanCrit>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<LeatherHood>(1);
            recipe.AddIngredient<LeatherPlate>(1);
            recipe.AddIngredient<LeatherLegs>(1);
            recipe.AddIngredient<Kunai_Throwing>(50);
            recipe.AddIngredient<Dartboard>(1);
            recipe.AddIngredient<MagnifyingGlass>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class MarksmanCrit : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarksmanEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LeatherHood>().UpdateArmorSet(player);
            }
        }
    }
}