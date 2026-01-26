using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS.Items.Chaos;
using SOTS.Items.CritBonus;
using SOTS.Items.Invidia;
using SOTS.Items.Planetarium.FromChests;
using SOTS.Items.Pyramid;
using SOTS.Void;
using SOTSBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class VesperaEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 40000;
        }
        public override Color nameColor => new(255, 128, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<DarkEyeEffect>(Item))
            {
                ModContent.GetInstance<TheDarkEye>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<VoidCharmEffect>(Item))
            {
                ModContent.GetInstance<VoidCharm>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ParticleEffect>(Item))
            {
                ModContent.GetInstance<ParticleRelocator>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<RestRingEffect>(Item))
            {
                ModContent.GetInstance<RingofRest>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<VesperaEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VesperaMask>());
            recipe.AddIngredient(ModContent.ItemType<VesperaBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<VesperaLeggings>());
            recipe.AddIngredient(ModContent.ItemType<TheDarkEye>());
            recipe.AddIngredient(ModContent.ItemType<VoidCharm>());
            recipe.AddIngredient(ModContent.ItemType<ParticleRelocator>());
            if (ModCompatibility.SOTSBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<RingofRest>());
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class VesperaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VesperaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VesperaMask>().UpdateArmorSet(player);
            }
        }
        public class DarkEyeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VesperaEnchant>();
        }
        public class VoidCharmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VesperaEnchant>();
        }
        public class ParticleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VesperaEnchant>();
        }
        public class RestRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VesperaEnchant>();
        }
    }
}
