using Consolaria.Content.Items.Armor.Melee;
using Consolaria.Content.Items.Armor.Misc;
using Consolaria.Content.Items.Weapons.Melee;
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
    public class DragonEnchantC : BaseEnchant
    {
        public override Color nameColor => new Color(151, 191, 241);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DragonBurst>(Item);
            ModContent.GetInstance<AncientDragonEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DragonMask>();
            recipe.AddIngredient<DragonBreastplate>();
            recipe.AddIngredient<DragonGreaves>();
            recipe.AddIngredient<AncientDragonEnchant>();
            recipe.AddIngredient<Tonbogiri>();
            recipe.AddIngredient<Tizona>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class DragonBurst : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<DragonEnchantC>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DragonMask>().UpdateArmorSet(player);
            }
        }
    }
}
