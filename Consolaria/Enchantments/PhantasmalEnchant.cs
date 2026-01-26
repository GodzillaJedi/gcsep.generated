using Consolaria.Content.Items.Accessories;
using Consolaria.Content.Items.Armor.Magic;
using Consolaria.Content.Items.Armor.Misc;
using Consolaria.Content.Items.Weapons.Magic;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class PhantasmalEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(207, 142, 231);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PhantasmalAura>(Item);
            if (player.AddEffect<PhantasmalJump>(Item))
            {
                ModContent.GetInstance<ShadowboundExoskeleton>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<AncientPhantasmalEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<PhantasmalHeadgear>();
            recipe.AddIngredient<PhantasmalRobe>();
            recipe.AddIngredient<PhantasmalSubligar>();
            recipe.AddIngredient<AncientPhantasmalEnchant>();
            recipe.AddIngredient<RomanCandle>();
            recipe.AddIngredient<ShadowboundExoskeleton>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PhantasmalAura : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhantasmalEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PhantasmalHeadgear>().UpdateArmorSet(player);
            }
        }
        public class PhantasmalJump : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhantasmalEnchant>();
        }
    }
}
