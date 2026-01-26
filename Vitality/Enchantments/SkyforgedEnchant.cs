using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Accessories;
using VitalityMod.Items.StormCloud;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class SkyforgedEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => Color.LightYellow;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SkyforgedEffect>(Item);
            if (player.AddEffect<BlessStormEffect>(Item))
            {
                ModContent.GetInstance<StormsBlessing>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<StalkBloodEffect>(Item))
            {
                ModContent.GetInstance<BloodstalkerCape>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SkyforgedMask>());
            recipe.AddIngredient(ModContent.ItemType<SkyforgedChainmail>());
            recipe.AddIngredient(ModContent.ItemType<SkyforgedLeggings>());
            recipe.AddIngredient(ModContent.ItemType<StormsBlessing>());
            recipe.AddIngredient(ModContent.ItemType<BloodstalkerCape>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class SkyforgedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SkyforgedEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SkyforgedMask>().UpdateArmorSet(player);
            }
        }
        public class BlessStormEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SkyforgedEnchant>();
        }
        public class StalkBloodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SkyforgedEnchant>();
        }
    }
}
