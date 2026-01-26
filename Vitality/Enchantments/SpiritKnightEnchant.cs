using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.PaladinSpirit;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class SpiritKnightEnchant : BaseEnchant
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
        public override Color nameColor => Color.Gray;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SpiritKnightEffect>(Item);
            if (player.AddEffect<SpiritShieldEffect>(Item))
            {
                ModContent.GetInstance<SpectralShield>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GiantEyeEffect>(Item))
            {
                ModContent.GetInstance<GiantLens>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SoulPouchEffect>(Item))
            {
                ModContent.GetInstance<SpiritPouch>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SpiritKnightHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SpiritKnightBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<SpiritKnightLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SpectralShield>());
            recipe.AddIngredient(ModContent.ItemType<GiantLens>());
            recipe.AddIngredient(ModContent.ItemType<SpiritPouch>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class SpiritKnightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritKnightEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SpiritKnightHelmet>().UpdateArmorSet(player);
            }
        }
        public class SpiritShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritKnightEnchant>();
        }
        public class GiantEyeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritKnightEnchant>();
        }
        public class SoulPouchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<OreForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritKnightEnchant>();
        }
    }
}
