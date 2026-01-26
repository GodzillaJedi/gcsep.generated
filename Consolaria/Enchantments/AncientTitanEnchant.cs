using Consolaria.Content.Items.Armor.Ranged;
using Consolaria.Content.Items.Weapons.Ranged;
using Consolaria.Content.Items.Weapons.Throwing;
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
    public class AncientTitanEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(60, 75, 100);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Ranged) += 8f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AncientTitanHelmet>();
            recipe.AddIngredient<AncientTitanMail>();
            recipe.AddIngredient<AncientTitanLeggings>();
            recipe.AddIngredient<Sharanga>();
            recipe.AddIngredient<DragonBreath>();
            recipe.AddIngredient<HolyHandgrenade>(5);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
