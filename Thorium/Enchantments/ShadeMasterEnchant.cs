using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ShadeMasterEnchant : BaseEnchant
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
            player.AddEffect<ShadeMasterEffect>(Item);
            if (player.AddEffect<ShinobiEffect>(Item))
            {
                ModContent.GetInstance<ShinobiSigil>().UpdateAccessory(player, hideVisual);
            }
        }
        public class ShadeMasterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShadeMasterEnchant>();
            public override bool MutantsPresenceAffects => true; 
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ShadeMasterMask>().UpdateArmorSet(player);
            }
        }
        public class ShinobiEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ShadeMasterEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ShadeMasterMask>());
            recipe.AddIngredient(ModContent.ItemType<ShadeMasterGarb>());
            recipe.AddIngredient(ModContent.ItemType<ShadeMasterTreads>());
            recipe.AddIngredient(ModContent.ItemType<ShinobiSigil>());
            recipe.AddIngredient(ModContent.ItemType<ShadeKunai>(), 300);
            recipe.AddIngredient(ModContent.ItemType<TechniqueShadowDance>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
