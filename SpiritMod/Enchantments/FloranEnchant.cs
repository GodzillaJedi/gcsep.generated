using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory.ElectricGuitar;
using SpiritMod.Items.BossLoot.DuskingDrops.DuskArmor;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.FloranSet;
using SpiritMod.Items.Sets.FloranSet.FloranArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.DuskEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class FloranEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(131, 180, 0);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FloranWellFed>(Item);
            if (player.AddEffect<FloranGrassEffect>(Item))
            {
                ModContent.GetInstance<FloranCharm>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FHelmet>(1);
            recipe.AddIngredient<FPlate>(1);
            recipe.AddIngredient<FLegs>(1);
            recipe.AddIngredient<StaminaCodex>(1);
            recipe.AddIngredient<FloranBludgeon>(1);
            recipe.AddIngredient<FloranCharm>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public class FloranWellFed : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FloranEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FHelmet>().UpdateArmorSet(player);
            }
        }
        public class FloranGrassEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FloranEnchant>();
        }
    }
}
