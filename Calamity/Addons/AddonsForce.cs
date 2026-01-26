using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AddonsForce : BaseForce
    {
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
            if (ModCompatibility.Clamity.Loaded)
            {
                ModContent.GetInstance<ClamitasEnchant>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<FrozenEnchant>().UpdateAccessory(player, hideVisual);
            }

            if (ModCompatibility.Catalyst.Loaded)
            {
                ModContent.GetInstance<IntergelacticEnchant>().UpdateAccessory(player, false);
            }

            if (ModCompatibility.Goozma.Loaded)
            {
                ModContent.GetInstance<ShogunEnchant>().UpdateAccessory(player, false);
            }

            if (ModCompatibility.Entropy.Loaded)
            {
                ModContent.GetInstance<MariviumEnchant>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<VoidFaquirEnchant>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                ModContent.GetInstance<SolynsSigil>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (ModCompatibility.Clamity.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<FrozenEnchant>());
                recipe.AddIngredient(ModContent.ItemType<ClamitasEnchant>());
            }

            if (ModCompatibility.Catalyst.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<IntergelacticEnchant>());
            }

            if (ModCompatibility.Goozma.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<ShogunEnchant>());
            }

            if (ModCompatibility.Entropy.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<VoidFaquirEnchant>());
                recipe.AddIngredient(ModContent.ItemType<MariviumEnchant>());
            }

            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<SolynsSigil>());
            }

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
            recipe.Register();
        }
    }
}
