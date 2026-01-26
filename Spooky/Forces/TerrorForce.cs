using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.Spooky.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Forces
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class HorrorForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Spooky;
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
            ModContent.GetInstance<LivingFleshEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GoreEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<OldWoodEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<RottenGourdEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<LivingFleshEnchant>();
            recipe.AddIngredient<GoreEnchant>();
            recipe.AddIngredient<OldWoodEnchant>();
            recipe.AddIngredient<RottenGourdEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
