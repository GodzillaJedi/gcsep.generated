using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Buffs.Healer;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Bronze;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Projectiles.Bard;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BulbEnchant : BaseEnchant
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
            player.AddEffect<BloomingEffect>(Item);
            if (player.AddEffect<BloomingShieldEffect>(Item))
            {
                ModContent.GetInstance<BloomingShield>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BulbEffect>(Item))
            {
                ModContent.GetInstance<KickPetal>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FragrantEffect>(Item))
            {
                ModContent.GetInstance<FragrantCorsage>().UpdateAccessory(player, hideVisual);
            }
        }
        public class BloomingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BulbEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BloomingCrown>().UpdateArmorSet(player);
            }
        }
        public class BulbEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BulbEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class BloomingShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BulbEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class FragrantEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BulbEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {


            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BloomingCrown>());
            recipe.AddIngredient(ModContent.ItemType<BloomingTabard>());
            recipe.AddIngredient(ModContent.ItemType<BloomingLeggings>());
            recipe.AddIngredient(ModContent.ItemType<BloomingShield>());
            recipe.AddIngredient(ModContent.ItemType<FragrantCorsage>());
            recipe.AddIngredient(ModContent.ItemType<KickPetal>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
