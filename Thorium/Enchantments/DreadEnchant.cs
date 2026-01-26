using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.BossBoreanStrider;
using ThoriumMod.Items.Dread;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DreadEnchant : BaseEnchant
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
            player.AddEffect<DreadEffect>(Item);
            if (player.AddEffect<CrashEffect>(Item))
            {
                ModContent.GetInstance<CrashBoots>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CursedFlailEffect>(Item))
            {
                ModContent.GetInstance<CursedFlailCore>().UpdateAccessory(player, hideVisual);
            }
        }

        public class DreadEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DreadSkull>().UpdateArmorSet(player);
            }
        }
        public class CrashEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class CursedFlailEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreadEnchant>();
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DreadSkull>());
            recipe.AddIngredient(ModContent.ItemType<DreadChestPlate>());
            recipe.AddIngredient(ModContent.ItemType<DreadGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CrashBoots>());
            recipe.AddIngredient(ModContent.ItemType<CursedFlailCore>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
