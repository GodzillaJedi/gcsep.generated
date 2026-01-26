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
    public class FrostburnForce : BaseForce
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
            ModContent.GetInstance<BloodcourtEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CryoliteEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DuskEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FrigidEnchantSp>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MarksmanEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PainMongerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SlagTyrantEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BloodcourtEnchant>();
            recipe.AddIngredient<CryoliteEnchant>();
            recipe.AddIngredient<DuskEnchant>();
            recipe.AddIngredient<FrigidEnchantSp>();
            recipe.AddIngredient<MarksmanEnchant>();
            recipe.AddIngredient<PainMongerEnchant>();
            recipe.AddIngredient<SlagTyrantEnchant>();
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
