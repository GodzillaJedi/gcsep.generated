using Consolaria.Content.Items.Armor.Magic;
using Consolaria.Content.Items.Armor.Ranged;
using Consolaria.Content.Items.Weapons.Ranged;
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
    public class TitanEnchantC : BaseEnchant
    {
        public override Color nameColor => new Color(107, 135, 135);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TitanRecoil>(Item);
            ModContent.GetInstance<AncientTitanEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<TitanHelmet>();
            recipe.AddIngredient<TitanMail>();
            recipe.AddIngredient<TitanLeggings>();
            recipe.AddIngredient<AncientTitanEnchant>();
            recipe.AddIngredient<SpicySauce>();
            recipe.AddIngredient<VolcanicRepeater>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class TitanRecoil : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanEnchantC>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TitanHelmet>().UpdateArmorSet(player);
            }
        }
    }
}
