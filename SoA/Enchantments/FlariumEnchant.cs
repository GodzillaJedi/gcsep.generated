using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Armor.Dragon;
using SacredTools.Items.Mount;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Weapons.Special;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumEnchant : BaseEnchant
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
            Item.value = 350000;
        }

        public override Color nameColor => new(204, 78, 40);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FlariumHelmEffect>(Item);
        }

        public class FlariumHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlariumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FlariumHelmet>().UpdateArmorSet(player);
                ModContent.GetInstance<FlariumHood>().UpdateArmorSet(player);
                ModContent.GetInstance<FlariumMask>().UpdateArmorSet(player);
                ModContent.GetInstance<FlariumCrown>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddRecipeGroup("gcsep:FlariumHelms");
            recipe.AddIngredient<FlariumChest>();
            recipe.AddIngredient<FlariumLeggings>();
            recipe.AddIngredient<FlariumRocketLauncher>();
            recipe.AddIngredient<SolusKatana>();
            recipe.AddIngredient<SerpentSceptre>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
