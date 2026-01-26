using FargowiltasSouls.Content.Items.Accessories.Essences;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Weapons.Dreadfire;
using SacredTools.Content.Items.Weapons.Harpy;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Decree;
using SacredTools.Items.Weapons.Special;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Essences
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class StalkerEssence : BaseEssence
    {
        public override Color nameColor => new(100, 233, 155);

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<NinjaEmblem>();

            recipe.AddIngredient<HarpyBoomerang>();
            recipe.AddIngredient<LapisJavelin>();
            recipe.AddIngredient<IceclawShuriken>();
            recipe.AddIngredient<DeathJavelin>();
            recipe.AddIngredient<Featherstorm>();
            recipe.AddIngredient<Pumpnade>(500);
            recipe.AddIngredient<GoldJavelin>(500);

            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}
