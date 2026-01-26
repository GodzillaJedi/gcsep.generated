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
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MarchingBandEnchant : BaseEnchant
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
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MarchingBandEffect>(Item);
            if (player.AddEffect<FullScoreEffect>(Item))
            {
                ModContent.GetInstance<FullScore>().UpdateAccessory(player, hideVisual);
            }
        }

        public class MarchingBandEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarchingBandEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MarchingBandShako>().UpdateArmorSet(player);
            }
        }
        public class FullScoreEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarchingBandEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MarchingBandShako>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandUniform>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandLeggings>());
            recipe.AddIngredient(ModContent.ItemType<FullScore>());
            recipe.AddIngredient(ModContent.ItemType<FrostwindCymbals>());
            recipe.AddIngredient(ModContent.ItemType<ShadowflameWarhorn>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
