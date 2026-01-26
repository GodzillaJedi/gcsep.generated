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
    public class IdeocracyForce : BaseForce
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
            Item.value = 1183376;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "SuperCeramicEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "RockSaltEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "PurifiedSaltEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "PlasticEnchant").UpdateAccessory(player, hideVisual);
            ModContent.Find<ModItem>(((ModType)this).Mod.Name, "KevlarEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<SuperCeramicEnchant>();
            recipe.AddIngredient<RockSaltEnchant>();
            recipe.AddIngredient<PurifiedSaltEnchant>();
            recipe.AddIngredient<PlasticEnchant>();
            recipe.AddIngredient<KevlarEnchant>();
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
