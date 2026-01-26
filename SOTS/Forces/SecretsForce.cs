using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.SOTS.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SOTS.Forces
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name)]
    public class SecretsForce : BaseForce
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 600000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<WormwoodEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementalEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FrostArtifactEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FrigidEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TwilightAssassinEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<WormwoodEnchant>();
            recipe.AddIngredient<ElementalEnchant>();
            recipe.AddIngredient<FrostArtifactEnchant>();
            recipe.AddIngredient<FrigidEnchant>();
            recipe.AddIngredient<TwilightAssassinEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
