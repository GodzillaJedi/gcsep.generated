using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.gunrightsmod.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.gunrightsmod.Forces
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class RadioactiveForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.TerMerica;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 4896201;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "FaradayEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "PlutoniumEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "UraniumEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "AstatineEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FaradayEnchant>();
            recipe.AddIngredient<PlutoniumEnchant>();
            recipe.AddIngredient<UraniumEnchant>();
            recipe.AddIngredient<AstatineEnchant>();
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
