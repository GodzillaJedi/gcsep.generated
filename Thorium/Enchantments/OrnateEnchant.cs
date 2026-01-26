using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OrnateEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OrnateHatEffect>(Item);
            if (player.AddEffect<ConcertEffect>(Item))
            {
                ModContent.GetInstance<ConcertTickets>().UpdateAccessory(player, hideVisual);
            }
        }
        public class OrnateHatEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OrnateEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OrnateHat>().UpdateArmorSet(player);
            }
        }
        public class ConcertEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OrnateEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<OrnateHat>());
            recipe.AddIngredient(ModContent.ItemType<OrnateJerkin>());
            recipe.AddIngredient(ModContent.ItemType<OrnateLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ConcertTickets>());
            recipe.AddIngredient(ModContent.ItemType<OrichalcumSlideWhistle>());
            //recipe.AddIngredient(ModContent.ItemType<GreenTambourine>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
