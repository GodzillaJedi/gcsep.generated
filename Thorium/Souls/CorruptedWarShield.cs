using gcsep.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.NPCItems;

namespace gcsep.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CorruptedWarShield : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<GoblinWarshield>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AstroBeetleHusk>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<GoblinWarshield>();
            recipe.AddIngredient<AstroBeetleHusk>();

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}