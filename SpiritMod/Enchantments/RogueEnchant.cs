using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Armor;
using SpiritMod.Items.BossLoot.AtlasDrops;
using SpiritMod.Items.BossLoot.AtlasDrops.PrimalstoneArmor;
using SpiritMod.Items.Weapon.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class RogueEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(195, 154, 92);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<RogueArmorEffect>(Item);
            if (player.AddEffect<RogueCrestEffect>(Item))
            {
                ModContent.GetInstance<RogueCrest>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<RogueRuneEffect>(Item))
            {
                ModContent.GetInstance<SwiftRune>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RogueHood>(1);
            recipe.AddIngredient<RoguePlate>(1);
            recipe.AddIngredient<RoguePants>(1);
            recipe.AddIngredient<Kunai_Throwing>(50);
            recipe.AddIngredient<RogueCrest>(1);
            recipe.AddIngredient<SwiftRune>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class RogueCrestEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RogueEnchant>();
        }
        public class RogueRuneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RogueEnchant>();
        }
        public class RogueArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RogueEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<RogueHood>().UpdateArmorSet(player);
            }
        }
    }
}
