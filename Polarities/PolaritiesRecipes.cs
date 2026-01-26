using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using gcsep.Polarities.Forces;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.ConvectiveArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.FractalArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.SelfsimilarArmor;
using Polarities.Content.Items.Materials.Hardmode;
using SOTS.Items.Fragments;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.Polarities
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class PolaritiesRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Polarities;
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup.RegisterGroup("gcsep:ConvectiveHelms",
                new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Convective Helmet",
                    ModContent.ItemType<ConvectiveHelmetMelee>(),
                    ModContent.ItemType<ConvectiveHelmetMagic>(),
                    ModContent.ItemType<ConvectiveHelmetRanged>(),
                    ModContent.ItemType<ConvectiveHelmetSummon>()));

            RecipeGroup.RegisterGroup("gcsep:SelfsimilarHelms",
                new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Selfsimilar Helmet",
                    ModContent.ItemType<SelfsimilarHelmetSummoner>(),
                    ModContent.ItemType<SelfsimilarHelmetRanger>(),
                    ModContent.ItemType<SelfsimilarHelmetMelee>(),
                    ModContent.ItemType<SelfsimilarHelmetMage>()));

            RecipeGroup.RegisterGroup("gcsep:FractalHelms",
                new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Fractal Helmet",
                    ModContent.ItemType<FractalHelmetSummoner>(),
                    ModContent.ItemType<FractalHelmetRanger>(),
                    ModContent.ItemType<FractalHelmetMelee>(),
                    ModContent.ItemType<FractalHelmetMage>()));
        }

        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // ---------------------------------------------------------
                // 1. Add SmiteSoul to major Souls when BOTH Calamity AND SacredTools are missing
                // ---------------------------------------------------------
                bool noCalamity = !ModCompatibility.Calamity.Loaded;
                bool noSacredTools = !ModCompatibility.SacredTools.Loaded;

                if (noCalamity && noSacredTools)
                {
                    if ((recipe.HasResult<UniverseSoul>() ||
                         recipe.HasResult<TerrariaSoul>() ||
                         recipe.HasResult<MasochistSoul>() ||
                         recipe.HasResult<DimensionSoul>()) &&
                        !recipe.HasIngredient<SmiteSoul>())
                    {
                        recipe.AddIngredient<SmiteSoul>(5);
                    }
                }

                // ---------------------------------------------------------
                // 2. Add SmiteSoul to ALL Forces (no mod gating)
                // ---------------------------------------------------------
                if (recipe.createItem.ModItem is BaseForce &&
                    !recipe.HasIngredient<SmiteSoul>())
                {
                    recipe.AddIngredient<SmiteSoul>(4);
                }

                // ---------------------------------------------------------
                // 3. Terraria Soul requires SpacetimeForce + WildernessForce
                // ---------------------------------------------------------
                if (recipe.HasResult<TerrariaSoul>())
                {
                    if (!recipe.HasIngredient<SpacetimeForce>())
                        recipe.AddIngredient<SpacetimeForce>(1);

                    if (!recipe.HasIngredient<WildernessForce>())
                        recipe.AddIngredient<WildernessForce>(1);
                }
            }
        }
    }
}