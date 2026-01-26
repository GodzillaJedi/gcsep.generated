using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Thorium.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Thorium.Enchantments.DarksteelEnchant;
using static gcsep.Thorium.Enchantments.DurasteelEnchant;
using static gcsep.Thorium.Enchantments.GeodeEnchant;
using static gcsep.Thorium.Enchantments.IllumiteEnchant;
using static gcsep.Thorium.Enchantments.LodestoneEnchant;
using static gcsep.Thorium.Enchantments.SteelEnchant;
using static gcsep.Thorium.Enchantments.TerrariumEnchant;
using static gcsep.Thorium.Enchantments.ThoriumEnchant;
using static gcsep.Thorium.Enchantments.ValadiumEnchant;

namespace gcsep.Thorium.Forces
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MidgardForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }


        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<GeodeEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DurasteelEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LodestoneEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ValadiumEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<IllumiteEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TerrariumEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<GeodeEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelEnchant>());
            recipe.AddIngredient(ModContent.ItemType<LodestoneEnchant>());
            recipe.AddIngredient(ModContent.ItemType<IllumiteEnchant>());
            recipe.AddIngredient(ModContent.ItemType<ValadiumEnchant>());
            recipe.AddIngredient(ModContent.ItemType<TerrariumEnchant>());

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
