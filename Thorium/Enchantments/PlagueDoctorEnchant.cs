using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class PlagueDoctorEnchant : BaseEnchant
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
            player.AddEffect<PlagueEffect>(Item);
        }
        public class PlagueEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlagueDoctorEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PlagueDoctorsMask>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsMask>());
            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsGarb>());
            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorsLeggings>());
            recipe.AddIngredient(ModContent.ItemType<GasContainer>(), 300);
            recipe.AddIngredient(ModContent.ItemType<CombustionFlask>(), 300);
            recipe.AddIngredient(ModContent.ItemType<NitrogenVial>(), 300);

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
