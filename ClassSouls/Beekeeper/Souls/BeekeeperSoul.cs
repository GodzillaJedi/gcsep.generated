using BombusApisBee.Content.Dungeon.Items.SpectralBeeTome;
using BombusApisBee.Content.Eclipse.Items.BeeFlasks;
using BombusApisBee.Content.Forest.Items.GlassOfHoney;
using BombusApisBee.Content.Forest.Items.HoneyFlareCannon;
using BombusApisBee.Content.Forest.Items.HymenoptraHandcannon;
using BombusApisBee.Content.Forest.Items.HymenoptrianNecklace;
using BombusApisBee.Content.Forest.Items.TheBeeBlade;
using BombusApisBee.Content.Jungle.Items.ChlorophyteHoneycomb;
using BombusApisBee.Content.Jungle.Items.LihzardianHornetRelic;
using BombusApisBee.Content.Martian.Items.LazerbeemBlaster;
using BombusApisBee.Content.MechBoss.Items.MetalPlatedHoneycomb;
using BombusApisBee.Content.Pillars.Items.GalaxyHoneycomb;
using BombusApisBee.Core.BeekeeperClass;
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
            player.GetDamage<BeekeeperDamageClass>() += 0.22f;
            player.GetCritChance<BeekeeperDamageClass>() += 0.10f;
            player.GetAttackSpeed<BeekeeperDamageClass>() += 0.15f;
            player.GetModPlayer<BeekeeperPlayer>().BeeResourceMax2 += 200;
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
