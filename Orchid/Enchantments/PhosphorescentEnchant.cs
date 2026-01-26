using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Alchemist.Accessories;
using OrchidMod.Content.Alchemist.Armors.Mushroom;
using OrchidMod.Content.Guardian.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class PhosphorescentEnchant : BaseEnchant
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
        public override Color nameColor => Color.Azure;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PhosphorescentEffect>(Item);
            if (player.AddEffect<ReactiveVialEffect>(Item))
            {
                ModContent.GetInstance<ReactiveVials>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpikeSproutEffect>(Item))
            {
                ModContent.GetInstance<DungeonSpike>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MushroomBandana>());
            recipe.AddIngredient(ModContent.ItemType<MushroomTunic>());
            recipe.AddIngredient(ModContent.ItemType<MushroomLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ReactiveVials>());
            recipe.AddIngredient(ModContent.ItemType<DungeonSpike>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class PhosphorescentEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhosphorescentEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MushroomBandana>().UpdateArmorSet(player);
            }
        }
        public class ReactiveVialEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhosphorescentEnchant>();
        }
        public class SpikeSproutEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhosphorescentEnchant>();
        }
    }
}
