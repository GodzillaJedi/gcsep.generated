using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Sets.FrigidSet;
using SpiritMod.Items.Sets.FrigidSet.FrigidArmor;
using SpiritMod.Items.Sets.FrigidSet.Frostbite;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class FrigidEnchantSp : BaseEnchant
    {
        public override Color nameColor => new Color(98, 100, 255);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 30000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<FrigidAttackSpeed>(Item))
            {
                ModContent.GetInstance<FrigidGloves>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<FrigidIceWall>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FrigidHelm>(1);
            recipe.AddIngredient<FrigidChestplate>(1);
            recipe.AddIngredient<FrigidLegs>(1);
            recipe.AddIngredient<IcySpear>(1);
            recipe.AddIngredient<HowlingScepter>(1);
            recipe.AddIngredient<FrigidGloves>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class FrigidAttackSpeed : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchantSp>();
        }
        public class FrigidIceWall : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchantSp>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FrigidHelm>().UpdateArmorSet(player);
            }
        }
    }
}
