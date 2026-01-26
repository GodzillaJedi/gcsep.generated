using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.SoA.Enchantments;
using SacredTools.Content.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Forces
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class GenerationsForce : BaseForce
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
            ModContent.GetInstance<CairoCrusaderEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<EerieEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BismuthEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DreadfireEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MarstechEnchant>().UpdateAccessory(player, hideVisual);
            player.AddEffect<GenerationsEffect>(Item);
        }
        public class GenerationsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<CairoCrusaderEnchant>();
            recipe.AddIngredient<EerieEnchant>();
            recipe.AddIngredient<BismuthEnchant>();
            recipe.AddIngredient<DreadfireEnchant>();
            recipe.AddIngredient<MarstechEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
