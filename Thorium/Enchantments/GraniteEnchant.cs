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
using ThoriumMod.Buffs;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossGraniteEnergyStorm;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Granite;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GraniteEnchant : BaseEnchant
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
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.GetModPlayer<CSEThoriumPlayer>().ThoriumSoul)
            {
                player.moveSpeed -= 0.5f;
                player.maxRunSpeed = 4f;
            }
            if (player.AddEffect<ShockAbsorberEffect>(Item))
            {
                ModContent.GetInstance<ShockAbsorber>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<HeartStoneEffect>(Item))
            {
                ModContent.GetInstance<HeartOfStone>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<GraniteEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<GraniteHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GraniteChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<GraniteGreaves>());
            recipe.AddIngredient(ModContent.ItemType<HeartOfStone>());
            recipe.AddIngredient(ModContent.ItemType<ShockAbsorber>());
            recipe.AddIngredient(ModContent.ItemType<ObsidianStriker>(), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class GraniteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GraniteHelmet>().UpdateArmorSet(player);
            }
        }
        public class HeartStoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ShockAbsorberEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
    }
}
