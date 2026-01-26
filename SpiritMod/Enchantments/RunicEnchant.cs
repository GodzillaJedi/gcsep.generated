using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Armor;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.RunicSet;
using SpiritMod.Items.Sets.RunicSet.RunicArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class RunicEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(35, 200, 254);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<RunicSpawn>(Item))
            {
                ModContent.GetInstance<RuneWizardScroll>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<RunicScroll>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RunicHood>(1);
            recipe.AddIngredient<RunicPlate>(1);
            recipe.AddIngredient<RunicGreaves>(1);
            recipe.AddIngredient<SpiritRune>(1);
            recipe.AddIngredient<RuneWizardScroll>(1);
            recipe.AddIngredient<SpiritBiomePainting>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class RunicSpawn : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RunicEnchant>();
        }
        public class RunicScroll : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RunicEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<RunicHood>().UpdateArmorSet(player);
            }
        }
    }
}
