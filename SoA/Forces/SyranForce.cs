using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.SoA.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SyranForce : BaseForce
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<VoidWardenEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<VulcanReaperEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FlariumEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ExitumLuxEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AsthraltiteEnchant>().UpdateAccessory(player, hideVisual);
            player.AddEffect<SyranEffect>(Item);
        }
        public class SyranEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<VoidWardenEnchant>();
            recipe.AddIngredient<VulcanReaperEnchant>();
            recipe.AddIngredient<FlariumEnchant>();
            recipe.AddIngredient<ExitumLuxEnchant>();
            recipe.AddIngredient<AsthraltiteEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
