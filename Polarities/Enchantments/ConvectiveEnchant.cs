using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Accessories.ExpertMode.Hardmode;
using Polarities.Content.Items.Armor.Classless.PreHardmode.AerogelArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.ConvectiveArmor;
using Polarities.Content.Items.Weapons.Magic.Books.Hardmode;
using Polarities.Content.Items.Weapons.Magic.Guns.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SOTS.Enchantments.VibrantEnchant;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class ConvectiveEnchant : BaseEnchant
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
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 119, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ConvectiveHelmsEffect>(Item);
            if (player.AddEffect<BuildEruptEffect>(Item))
            {
                ModContent.GetInstance<BuildingEruption>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddRecipeGroup("gcsep:ConvectiveHelms");
            recipe.AddIngredient(ModContent.ItemType<ConvectiveArmor>());
            recipe.AddIngredient(ModContent.ItemType<ConvectiveLeggings>());
            recipe.AddIngredient(ModContent.ItemType<BuildingEruption>());
            recipe.AddIngredient(ModContent.ItemType<WormSpewer>());
            recipe.AddIngredient(ModContent.ItemType<HadeanUpwelling>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class ConvectiveHelmsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ConvectiveEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ConvectiveHelmetMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<ConvectiveHelmetMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<ConvectiveHelmetRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<ConvectiveHelmetSummon>().UpdateArmorSet(player);
            }
        }
        public class BuildEruptEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ConvectiveEnchant>();
        }
    }
}
