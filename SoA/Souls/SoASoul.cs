using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using gcsep.Core;
using gcsep.SoA.Enchantments;
using gcsep.SoA.Forces;
using SacredTools;
using SacredTools.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Souls
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoASoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationHorizontal(60, 6));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.defense = 25;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<FoundationsForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GenerationsForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SoranForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SyranForce>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FoundationsForce>();
            recipe.AddIngredient<GenerationsForce>();
            recipe.AddIngredient<SoranForce>();
            recipe.AddIngredient<SyranForce>();
            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<EmberOfOmen>(5);
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
