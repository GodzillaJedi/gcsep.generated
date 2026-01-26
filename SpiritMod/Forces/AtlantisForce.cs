using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.SpiritMod.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace gcsep.SpiritMod.Forces
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class AtlantisForce : BaseForce
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
            ModContent.GetInstance<BismiteEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CascadeEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GraniteChunkEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<StreamSurferEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SpiritEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PrimalstoneEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismiteEnchant>();
            recipe.AddIngredient<CascadeEnchant>();
            recipe.AddIngredient<GraniteChunkEnchant>();
            recipe.AddIngredient<StreamSurferEnchant>();
            recipe.AddIngredient<SpiritEnchant>();
            recipe.AddIngredient<PrimalstoneEnchant>();
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
