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
    public class FoundationsForce : BaseForce
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
            ModContent.GetInstance<PrairieEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LapisEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FrosthunterEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BlightboneEnchant>().UpdateAccessory(player, hideVisual);

            player.AddEffect<FoundationsEffect>(Item);
        }
        public class FoundationsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<PrairieEnchant>();
            recipe.AddIngredient<LapisEnchant>();
            recipe.AddIngredient<FrosthunterEnchant>();
            recipe.AddIngredient<BlightboneEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}