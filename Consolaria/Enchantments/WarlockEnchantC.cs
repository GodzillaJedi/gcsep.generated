using Consolaria.Content.Items.Accessories;
using Consolaria.Content.Items.Armor.Summon;
using Consolaria.Content.Items.Weapons.Summon;
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
    public class WarlockEnchantC : BaseEnchant
    {
        public override Color nameColor => new Color(164, 108, 187);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<WarlockHoodC>(Item);
            ModContent.GetInstance<AncientWarlockEnchant>().UpdateAccessory(player, hideVisual);
            player.lifeRegen += 3;
            player.jumpSpeedBoost += 2.5f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<WarlockHood>();
            recipe.AddIngredient<WarlockRobe>();
            recipe.AddIngredient<WarlockLeggings>();
            recipe.AddIngredient<AncientWarlockEnchant>();
            recipe.AddIngredient<EternityStaff>();
            recipe.AddIngredient<ValentineRing>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class WarlockHoodC : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<WarlockEnchantC>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<WarlockHood>().UpdateArmorSet(player);
            }
        }
    }
}
