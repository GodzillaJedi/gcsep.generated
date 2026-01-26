using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.BossLoot.ScarabeusDrops.ChitinArmor;
using SpiritMod.Items.BossLoot.ScarabeusDrops.LocustCrook;
using SpiritMod.Items.BossLoot.ScarabeusDrops.RadiantCane;
using SpiritMod.Items.Sets.CascadeSet.Armor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class ChitinEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(141, 163, 239);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ChitinTornadoDash>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ChitinHelmet>(1);
            recipe.AddIngredient<ChitinChestplate>(1);
            recipe.AddIngredient<ChitinLeggings>(1);
            recipe.AddIngredient<LocustCrook>(1);
            recipe.AddIngredient<RadiantCane>(1);
            recipe.AddIngredient<DesertSlab>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class ChitinTornadoDash : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ChitinEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ChitinHelmet>().UpdateArmorSet(player);
            }
        }
    }
}