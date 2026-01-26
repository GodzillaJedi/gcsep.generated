using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.SeraphSet.SeraphArmor;
using SpiritMod.Items.Sets.SlagSet;
using SpiritMod.Items.Sets.SlagSet.FieryArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.SeraphEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SlagTyrantEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(211, 61, 8);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 30000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<SlagTyrantSummon>(Item))
            {
                ModContent.GetInstance<CimmerianScepter>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<SlagTyrantBurst>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ObsidiusHelm>();
            recipe.AddIngredient<ObsidiusGreaves>();
            recipe.AddIngredient<ObsidiusPlate>();
            recipe.AddIngredient<Blasphemer>();
            recipe.AddIngredient<FierySummonStaff>();
            recipe.AddIngredient<CimmerianScepter>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class SlagTyrantBurst : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SlagTyrantEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ObsidiusHelm>().UpdateArmorSet(player);
            }
        }
        public class SlagTyrantSummon : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SlagTyrantEnchant>();
        }
    }
}