using Consolaria.Content.Items.Armor.Summon;
using Consolaria.Content.Items.Pets;
using Consolaria.Content.Items.Weapons.Summon;
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
    public class AncientWarlockEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(101, 25, 179);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 2;
            player.GetDamage(DamageClass.Summon) += 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AncientWarlockHood>();
            recipe.AddIngredient<AncientWarlockRobe>();
            recipe.AddIngredient<AncientWarlockLeggings>();
            recipe.AddIngredient<TurkeyFeather>();
            recipe.AddIngredient<MysteriousPackage>();
            recipe.AddIngredient<TurkeyStuff>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}

