using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BossThePrimordials.Dream;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DreamWeaverEnchant : BaseEnchant
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
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DreamWeaverHelmEffect>(Item);
            player.AddEffect<DreamWeaverHoodEffect>(Item);
        }
        public class DreamWeaverHoodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreamWeaverEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DreamWeaversHood>().UpdateArmorSet(player);
            }
        }
        public class DreamWeaverHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DreamWeaverEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DreamWeaversHelmet>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {


            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DreamWeaversHood>());
            recipe.AddIngredient(ModContent.ItemType<DreamWeaversHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DreamWeaversTabard>());
            recipe.AddIngredient(ModContent.ItemType<DreamWeaversTreads>());
            recipe.AddIngredient(ModContent.ItemType<DragonHeartWand>());
            recipe.AddIngredient(ModContent.ItemType<SnackLantern>());
            recipe.AddIngredient(ModContent.ItemType<ChristmasCheer>());


            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
