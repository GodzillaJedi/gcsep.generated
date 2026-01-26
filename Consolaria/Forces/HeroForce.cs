using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Consolaria.Enchantments;
using gcsep.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace gcsep.Consolaria.Forces
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class HeroForce : BaseForce
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
            ModContent.GetInstance<OstaraEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DragonEnchantC>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TitanEnchantC>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PhantasmalEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<WarlockEnchantC>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OstaraEnchant>();
            recipe.AddIngredient<DragonEnchantC>();
            recipe.AddIngredient<TitanEnchantC>();
            recipe.AddIngredient<PhantasmalEnchant>();
            recipe.AddIngredient<WarlockEnchantC>();
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
