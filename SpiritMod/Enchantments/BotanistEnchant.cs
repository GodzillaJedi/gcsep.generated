using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory.TalismanTree.GrislyTongue;
using SpiritMod.Items.Armor.BotanistSet;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.BloodcourtSet.BloodCourt;
using SpiritMod.Items.Sets.BriarChestLoot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.BloodcourtEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class BotanistEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(206, 182, 95);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BotanistHerbEffect>(Item);
            if (player.AddEffect<ReachBroochEffect>(Item))
            {
                ModContent.GetInstance<ReachBrooch>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BotanistHat>(1);
            recipe.AddIngredient<BotanistBody>(1);
            recipe.AddIngredient<BotanistLegs>(1);
            recipe.AddIngredient<ReachBrooch>(1);
            recipe.AddIngredient<ForagerTableItem>(1);
            recipe.AddIngredient<SunPot>(5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class BotanistHerbEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BotanistEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BotanistHat>().UpdateArmorSet(player);
            }
        }
        public class ReachBroochEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdventurerForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BotanistEnchant>();
        }
    }
}
