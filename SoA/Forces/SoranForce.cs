using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.SoA.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoranForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
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
            ModContent.GetInstance<BlazingBruteEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<NebulousApprenticeEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CosmicCommanderEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<StellarPriestEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<QuasarEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FallenPrinceEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<BlazingBruteEnchant>();
            recipe.AddIngredient<NebulousApprenticeEnchant>();
            recipe.AddIngredient<CosmicCommanderEnchant>();
            recipe.AddIngredient<StellarPriestEnchant>();
            recipe.AddIngredient<QuasarEnchant>();
            recipe.AddIngredient<FallenPrinceEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
