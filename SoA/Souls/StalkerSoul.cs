using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using gcsep.Core;
using SacredTools.Content.Items.Weapons.Asthraltite;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Flarium;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Items.Weapons.Oblivion;
using SacredTools.Items.Weapons.Primordia;
using gcsep.SoA.Essences;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.SoA.Souls
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class StalkerSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            player.GetDamage<ThrowingDamageClass>() += 0.22f;
            player.GetCritChance<ThrowingDamageClass>() += 10f;
            player.GetAttackSpeed<ThrowingDamageClass>() += 0.15f;
            player.CSE().throwerVelocity += 0.20f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<StalkerEssence>();

            recipe.AddIngredient<AsthralSaber>();
            recipe.AddIngredient<LunaticsGamble>();
            recipe.AddIngredient<FlariumDisc>();
            recipe.AddIngredient<SinisterKnives>();
            recipe.AddIngredient<FairGame>();
            recipe.AddIngredient<NovaknifePack>();
            recipe.AddIngredient<Ainfijarnar>();
            recipe.AddIngredient<TerraLance>();
            recipe.AddIngredient<TrueDecapitator>();
            recipe.AddIngredient<OrbFlayer>();

            recipe.AddIngredient<AbomEnergy>(10);

            recipe.AddTile<CrucibleCosmosSheet>();
            recipe.Register();
        }
    }
}
