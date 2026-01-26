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
    public class HurricaneForce : BaseForce
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
            ModContent.GetInstance<RogueEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ChitinEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ApostleEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MarbleChunkEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AstraliteEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SeraphEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<RunicEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RogueEnchant>();
            recipe.AddIngredient<ChitinEnchant>();
            recipe.AddIngredient<ApostleEnchant>();
            recipe.AddIngredient<MarbleChunkEnchant>();
            recipe.AddIngredient<AstraliteEnchant>();
            recipe.AddIngredient<SeraphEnchant>();
            recipe.AddIngredient<RunicEnchant>();
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
