using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Accessories.ExpertMode.PreHardmode;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.SelfsimilarArmor;
using Polarities.Content.Items.Armor.Summon.PreHardmode.StormcloudArmor;
using Polarities.Content.Items.Weapons.Magic.Flawless;
using Polarities.Content.Items.Weapons.Magic.Guns.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class StormcloudEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Polarities;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(42, 56, 66);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<StormcloudEffect>(Item);
            if (player.AddEffect<StormScaleEffect>(Item))
            {
                ModContent.GetInstance<StormScales>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<StormcloudMask>());
            recipe.AddIngredient(ModContent.ItemType<StormcloudArmor>());
            recipe.AddIngredient(ModContent.ItemType<StormcloudGreaves>());
            recipe.AddIngredient(ModContent.ItemType<StormScales>());
            recipe.AddIngredient(ModContent.ItemType<EyeOfTheStormfish>());
            recipe.AddIngredient(ModContent.ItemType<Flopper>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class StormcloudEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StormcloudEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<StormcloudMask>().UpdateArmorSet(player);
            }
        }
        public class StormScaleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StormcloudEnchant>();
        }
    }
}
