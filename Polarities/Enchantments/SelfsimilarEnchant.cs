using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Accessories.ExpertMode.Hardmode;
using Polarities.Content.Items.Armor.Flawless.MechaMayhemArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.SelfsimilarArmor;
using Polarities.Content.Items.Weapons.Melee.Broadswords.Hardmode;
using Polarities.Content.Items.Weapons.Ranged.Bows.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class SelfsimilarEnchant : BaseEnchant
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

        public override Color nameColor => new(154, 94, 181);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SelfsimilarEffect>(Item);
            if (player.AddEffect<SentinelHeartEffect>(Item))
            {
                ModContent.GetInstance<SentinelsHeart>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<SnakescaleEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("gcsep:SelfsimilarHelms");
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SentinelsHeart>());
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarBow>());
            recipe.AddIngredient(ModContent.ItemType<SelfsimilarSlasher>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class SelfsimilarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SelfsimilarEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SelfsimilarHelmetMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<SelfsimilarHelmetMage>().UpdateArmorSet(player);
                ModContent.GetInstance<SelfsimilarHelmetRanger>().UpdateArmorSet(player);
                ModContent.GetInstance<SelfsimilarHelmetSummoner>().UpdateArmorSet(player);
            }
        }
        public class SentinelHeartEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SpacetimeForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SelfsimilarEnchant>();
        }
    }
}
