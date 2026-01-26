using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.IridescentEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LifeBinderEnchant : BaseEnchant
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
            player.AddEffect<LifeBinderEffect>(Item);
            if (player.AddEffect<DewCollectorEffect>(Item))
            {
                ModContent.GetInstance<DewCollector>().UpdateAccessory(player, hideVisual);

            }
            ModContent.GetInstance<IridescentEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class LifeBinderEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LifeBinderEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LifeBinderMask>().UpdateArmorSet(player);
            }
        }
        public class DewCollectorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LifeBinderEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LifeBinderMask>());
            recipe.AddIngredient(ModContent.ItemType<LifeBinderBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<LifeBinderGreaves>());
            recipe.AddIngredient(ModContent.ItemType<IridescentEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DewCollector>());
            recipe.AddIngredient(ModContent.ItemType<SunrayStaff>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
