using BombusApisBee.BeeDamageClass;
using BombusApisBee.Items.Accessories.BeeKeeperDamageClass;
using BombusApisBee.Items.Weapons.BeeKeeperDamageClass;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.ClassSouls.Beekeeper.Souls
{
    [ExtendsFromMod(ModCompatibility.BeekeeperClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
    public class BeekeeperSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Beekeeper;
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
            player.GetDamage<HymenoptraDamageClass>() += 0.22f;
            player.GetCritChance<HymenoptraDamageClass>() += 0.10f;
            player.GetAttackSpeed<HymenoptraDamageClass>() += 0.15f;
            player.GetModPlayer<BeeDamagePlayer>().BeeResourceMax2 += 200;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(null, "BeekeeperEssence");
            recipe.AddIngredient<AbomEnergy>(10);

            recipe.AddIngredient<HoneycombOfTheGalaxies>();
            recipe.AddIngredient<SpectralBeeTome>();
            recipe.AddIngredient<ChlorophyteHoneycomb>();
            recipe.AddIngredient<HymenoptraHandcannon>();
            recipe.AddIngredient<HoneyFlareCannon>();
            recipe.AddIngredient<HymenoptraFlasks>();
            recipe.AddIngredient<MetalPlatedHoneycomb>();
            recipe.AddIngredient<LaserbeemBlaster>();
            recipe.AddIngredient<TheBeeBlade>();
            recipe.AddIngredient<LihzardianHornetRelic>();
            recipe.AddIngredient<HymenoptrianNecklace>();
            recipe.AddIngredient<GlassOfHoney>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
