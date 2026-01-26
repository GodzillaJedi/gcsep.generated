using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Weapons.FinalUpgrades;
using gcsep.Core;
using gcsep.Content.Items.Accessories;
using gcsep.Content.Items.Consumables;
using Terraria;
using Terraria.ModLoader;
using gcsep.CrossMod.CraftingStations;
using Fargowiltas.Items.Tiles;

namespace gcsep
{
    [ExtendsFromMod(ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.FargoCrossmod.Name)]
    public class GCSERecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // SacredTools soul upgrades
                if (!ModCompatibility.FargoCrossmod.Loaded &&
                    ModCompatibility.SacredTools.Loaded &&
                    (recipe.HasResult<ArchWizardsSoul>() ||
                     recipe.HasResult<BerserkerSoul>() ||
                     recipe.HasResult<ConjuristsSoul>() ||
                     recipe.HasResult<ColossusSoul>()) &&
                    !recipe.HasResult<AbomEnergy>())
                {
                    recipe.AddIngredient<AbomEnergy>(10);
                }

                // Alternative siblings weapons
                if ((recipe.HasResult(ModContent.ItemType<Penetrator>()) ||
                     recipe.HasResult(ModContent.ItemType<StyxGazer>()) ||
                     recipe.HasResult(ModContent.ItemType<SparklingLove>())) &&
                    !recipe.HasIngredient(ModContent.ItemType<Sadism>()) &&
                    GCSEConfig.Instance.AlternativeSiblings)
                {
                    recipe.AddIngredient<Sadism>(30);
                }

                // Eternity Soul crafting station swap
                if (recipe.HasResult(ModContent.ItemType<EternitySoul>()) &&
                    recipe.HasTile<CrucibleCosmosSheet>())
                {
                    recipe.RemoveTile(ModContent.TileType<CrucibleCosmosSheet>());
                    recipe.AddTile<MutantsForgeTile>();
                }

                // Remove AbomEnergy if Calamity is loaded
                if (ModCompatibility.Calamity.Loaded)
                {
                    if ((recipe.HasResult<UniverseSoul>() ||
                         recipe.HasResult<TerrariaSoul>() ||
                         recipe.HasResult<MasochistSoul>() ||
                         recipe.HasResult<DimensionSoul>()) &&
                        recipe.HasIngredient<AbomEnergy>())
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<AbomEnergy>());
                    }
                }

                // Eternity Soul requires EternityForce (unless AlternativeSiblings)
                if (recipe.HasResult(ModContent.ItemType<EternitySoul>()) &&
                    !recipe.HasIngredient<EternityForce>() &&
                    !GCSEConfig.Instance.AlternativeSiblings)
                {
                    if (GCSEConfig.Instance.SecretBosses)
                    {
                        recipe.AddIngredient<CyclonicFin>(1);
                    }
                    recipe.AddIngredient<EternityForce>(1);
                }

                // Remove AbomEnergy from Trawler Soul
                if (recipe.HasResult(ModContent.ItemType<TrawlerSoul>()) &&
                    recipe.HasIngredient(ModContent.ItemType<AbomEnergy>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AbomEnergy>());
                }
            }
        }
    }
}