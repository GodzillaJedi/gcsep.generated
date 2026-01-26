using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory.AceCardsSet;
using SpiritMod.Items.Armor.Masks;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.CryoliteSet.CryoliteArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class CryoliteEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(134, 245, 251);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CryoliteAura>(Item);
            if (player.AddEffect<CryoliteCard>(Item))
            {
                ModContent.GetInstance<FourOfAKind>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<WinterHatEffect>(Item))
            {
                ModContent.GetInstance<WinterHat>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CryoliteHead>(1);
            recipe.AddIngredient<CryoliteBody>(1);
            recipe.AddIngredient<CryoliteLegs>(1);
            recipe.AddIngredient<SlowCodex>(1);
            recipe.AddIngredient<WinterHat>(1);
            recipe.AddIngredient<FourOfAKind>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class CryoliteAura : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryoliteEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CryoliteHead>().UpdateArmorSet(player);
            }
        }
        public class CryoliteCard : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryoliteEnchant>();
        }
        public class WinterHatEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryoliteEnchant>();
        }
    }
}
