using Consolaria.Content.Items.Armor.Magic;
using Consolaria.Content.Items.Consumables;
using Consolaria.Content.Items.Weapons.Magic;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class AncientPhantasmalEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(147, 112, 204);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Magic) += 8f;
            player.GetDamage(DamageClass.Magic) += 0.12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AncientPhantasmalHeadgear>();
            recipe.AddIngredient<AncientPhantasmalRobe>();
            recipe.AddIngredient<AncientPhantasmalSubligar>();
            recipe.AddIngredient<OcramsEye>();
            recipe.AddIngredient<FeatherStorm>();
            recipe.AddIngredient<Wiesnbrau>(5);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
