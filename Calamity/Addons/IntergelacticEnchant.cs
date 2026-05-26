using CatalystMod.Items;
using CatalystMod.Items.Accessories;
using CatalystMod.Items.Armor.Intergelactic;
using CatalystMod.Items.Tools.GrapplingHooks;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name)]
    public class IntergelacticEnchant : BaseEnchant
    {
        public int CurrentRockDamage;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = CatalystItem.RaritySuperboss;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<IntergelacticArmorsEffect>(Item);
            player.AddEffect<IntergelacticSummonEffect>(Item);
            player.AddEffect<InfluxEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddRecipeGroup("gcsep:IntergelacticHelmet");
            recipe.AddIngredient(ModContent.ItemType<IntergelacticBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<IntergelacticGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CatalystMod.Items.Weapons.Typeless.ScytheoftheAbandonedGod>());
            recipe.AddIngredient(ModContent.ItemType<AstralLash>());
            recipe.AddIngredient(ModContent.ItemType<InfluxCluster>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class IntergelacticArmorsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IntergelacticEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<IntergelacticHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<IntergelacticHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<IntergelacticHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<IntergelacticHeadRogue>().UpdateArmorSet(player);
            }
        }
        public class IntergelacticSummonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IntergelacticEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<IntergelacticHeadSummon>().UpdateArmorSet(player);
            }
        }

        public class InfluxEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IntergelacticEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<InfluxCluster>().UpdateAccessory(player, true);
            }
        }
    }
}
