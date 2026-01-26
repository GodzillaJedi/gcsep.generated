using CalamityMod.Items.Materials;
using gcsep.Core;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.Content.Items.Materials;
using SacredTools.Content.Items.Weapons.Relic;
using gcsep.Calamity.Souls;
using gcsep.SoA.Souls;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class CalSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<CalamitySoul>() && !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }
                if (recipe.HasResult<SoASoul>() && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                }
                if ((recipe.HasResult<PaleRuin>() ||
                    recipe.HasResult<AshenWake>() ||
                    recipe.HasResult<CeruleanCyclone>() ||
                    recipe.HasResult<Malevolence>() ||
                    recipe.HasResult<NightTerror>() ||
                    recipe.HasResult<RogueWave>() ||
                    recipe.HasResult<Sharpshooter>() ||
                    recipe.HasResult<SwordOfGreed>()) && !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(1);
                }
                if ((recipe.HasResult<AsthraltiteHelmetRevenant>() ||
                    recipe.HasResult<AsthralRanged>() ||
                    recipe.HasResult<AsthralMelee>() ||
                    recipe.HasResult<AsthralChest>() ||
                    recipe.HasResult<AsthralMage>() ||
                    recipe.HasResult<AsthralLegs>() ||
                    recipe.HasResult<AsthralSummon>()) && !recipe.HasIngredient<AuricBar>())
                {
                    recipe.AddIngredient<AuricBar>(1);
                }
            }
        }
    }
}
