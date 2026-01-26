using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Redemption.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Redemption.Forces
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class AdvancementForce : BaseForce
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
            ModContent.GetInstance<CommonGuardEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DragonLeadEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElderWoodEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<HardlightEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LivingWoodEnchantR>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PureIronEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<XeniumEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<XenomiteEnchant>().UpdateAccessory(player, hideVisual);

            player.AddEffect<AdvancementEffect>(Item);
        }
        public class AdvancementEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<CommonGuardEnchant>();
            recipe.AddIngredient<DragonLeadEnchant>();
            recipe.AddIngredient<ElderWoodEnchant>();
            recipe.AddIngredient<HardlightEnchant>();
            recipe.AddIngredient<LivingWoodEnchantR>();
            recipe.AddIngredient<PureIronEnchant>();
            recipe.AddIngredient<XeniumEnchant>();
            recipe.AddIngredient<XenomiteEnchant>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
