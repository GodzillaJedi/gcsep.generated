using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Armor.BotanistSet;
using SpiritMod.Items.Sets.BriarChestLoot;
using SpiritMod.Items.Sets.CascadeSet.Armor;
using SpiritMod.Items.Sets.ReefhunterSet;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.BotanistEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class CascadeEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(136, 113, 92);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CascadeBubble>(Item);
            if (player.AddEffect<CascadePendant>(Item))
            {
                ModContent.GetInstance<PendantOfTheOcean>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<CascadeHelmet>(1);
            recipe.AddIngredient<CascadeChestplate>(1);
            recipe.AddIngredient<CascadeLeggings>(1);
            recipe.AddIngredient<ReefSpear>(1);
            recipe.AddIngredient<UrchinStaff>(1);
            recipe.AddIngredient<PendantOfTheOcean>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class CascadeBubble : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CascadeEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CascadeHelmet>().UpdateArmorSet(player);
            }
        }
        public class CascadePendant : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CascadeEnchant>();
        }
    }
}
