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
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ShootingStarEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }

        public static readonly int SetEmpowermentDuration = 6;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 250000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ShootingStarEffect>(Item);
            if (player.AddEffect<HeadsetEffect>(Item))
            {
                ModContent.GetInstance<Headset>().UpdateAccessory(player, hideVisual);
            }
        }
        public class ShootingStarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShootingStarEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ShootingStarHat>().UpdateArmorSet(player);
            }
        }
        public class HeadsetEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShootingStarEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ShootingStarHat>());
            recipe.AddIngredient(ModContent.ItemType<ShootingStarShirt>());
            recipe.AddIngredient(ModContent.ItemType<ShootingStarBoots>());
            recipe.AddIngredient(ModContent.ItemType<Headset>());
            recipe.AddIngredient(ModContent.ItemType<AcousticGuitar>());
            recipe.AddIngredient(ModContent.ItemType<SunflareGuitar>());


            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
