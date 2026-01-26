using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core;
using gcsep.Core;
using gcsep.Orchid.Forces;
using OrchidMod.Content.Alchemist.Accessories;
using OrchidMod.Content.Gambler.Accessories;
using OrchidMod.Content.Guardian.Accessories;
using OrchidMod.Content.Guardian.Misc;
using OrchidMod.Content.Shapeshifter.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Souls
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class SoulOfDiversity : BaseSoul
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationRectangularV(6, 5, 8));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.value = 5000000;
            Item.defense = 15;
            Item.rare = -12;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<ForceOfAlchemy>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ForceOfShamanist>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GuardianEmblem>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShapeshifterEmblem>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AlchemistEmblem>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AlchemistTest>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GamblerTest>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GuardianTest>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ForceOfAlchemy>();
            recipe.AddIngredient<ForceOfShamanist>();
            recipe.AddIngredient<GuardianEmblem>();
            recipe.AddIngredient<ShapeshifterEmblem>();
            recipe.AddIngredient<AlchemistEmblem>();
            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient<HorizonFragment>(5);
            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
