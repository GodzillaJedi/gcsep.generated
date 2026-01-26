using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs.Healer;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Steel;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TemplarEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TemplarEffect>(Item);
        }
        public class TemplarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TemplarEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TemplarsCirclet>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TemplarsCirclet>());
            recipe.AddIngredient(ModContent.ItemType<TemplarsTabard>());
            recipe.AddIngredient(ModContent.ItemType<TemplarsLeggings>());
            recipe.AddIngredient(ModContent.ItemType<LifesGift>());
            recipe.AddIngredient(ModContent.ItemType<TemplarsGrace>());
            recipe.AddIngredient(ModContent.ItemType<Prophecy>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
