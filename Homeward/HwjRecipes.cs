using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Material;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Homeward
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name, ModCompatibility.Calamity.Name)]
    public class HwjRecipes : ModSystem
    {
        private void TryPatch(Recipe recipe, Mod mod, string itemName, Action patch = null)
        {
            if (ModContent.TryFind(mod.Name, itemName, out ModItem item) && recipe.HasResult(item.Type))
            {
                patch?.Invoke();
            }
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // Add EssenceofBright if Calamity and SacredTools are not loaded
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    if ((recipe.HasResult<UniverseSoul>() || recipe.HasResult<TerrariaSoul>() || recipe.HasResult<MasochistSoul>() || recipe.HasResult<DimensionSoul>()) &&
                        !recipe.HasIngredient<EssenceofBright>())
                    {
                        recipe.AddIngredient<EssenceofBright>(5);
                    }
                }

                // Homeward compatibility
                if (ModCompatibility.Homeward.Loaded)
                {
                    var homeward = ModCompatibility.Homeward.Mod;

                    TryPatch(recipe, homeward, "PhilosophersStone", () =>
                    {
                        if (ModCompatibility.SacredTools.Loaded)
                            recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("OblivionBar"), 8);
                    });

                    TryPatch(recipe, homeward, "CommandersGaunlet", () =>
                    {
                        recipe.AddIngredient(ItemID.BerserkerGlove);
                        recipe.RemoveIngredient(ItemID.PowerGlove);
                        if (ModCompatibility.Calamity.Loaded)
                            recipe.AddIngredient(ModCompatibility.Calamity.Mod.Find<ModItem>("LifeAlloy"), 3);
                    });

                    TryPatch(recipe, homeward, "DivineTouch", () =>
                    {
                        recipe.RemoveIngredient(ItemID.FireGauntlet);
                        if (ModCompatibility.SacredTools.Loaded)
                            recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("FloraFist"));
                    });

                    TryPatch(recipe, homeward, "CrossbowScope", () =>
                    {
                        if (ModCompatibility.Redemption.Loaded)
                            recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3);
                        if (ModCompatibility.Thorium.Loaded)
                            recipe.AddIngredient(ModCompatibility.Thorium.Mod.Find<ModItem>("DeathEssence"), 3);
                    });

                    TryPatch(recipe, homeward, "Starflower", () => recipe.AddIngredient(homeward.Find<ModItem>("DoublePlot")));
                    TryPatch(recipe, homeward, "OneGiantLeap", () => recipe.AddIngredient(homeward.Find<ModItem>("MasterShield")));
                    TryPatch(recipe, homeward, "IncitingIncident", () => recipe.AddIngredient(homeward.Find<ModItem>("DivineNecklace")));
                    TryPatch(recipe, homeward, "Timelegcsepiner");
                    TryPatch(recipe, homeward, "ArrowCase", () => recipe.AddIngredient(homeward.Find<ModItem>("Edgewalker")));
                }

                // Calamity compatibility
                if (ModCompatibility.Calamity.Loaded)
                {
                    TryPatch(recipe, ModCompatibility.Calamity.Mod, "EtherealTalisman", () =>
                    {
                        if (ModCompatibility.Homeward.Loaded)
                        {
                            var homeward = ModCompatibility.Homeward.Mod;
                            recipe.AddIngredient(homeward.Find<ModItem>("EruditeBookmark"));
                            recipe.AddIngredient(homeward.Find<ModItem>("RejuvenatedCross"));
                        }
                    });

                    TryPatch(recipe, ModCompatibility.Calamity.Mod, "ElementalGauntlet", () =>
                    {
                        if (ModCompatibility.Homeward.Loaded)
                            recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("DivineTouch"));
                        else if (ModCompatibility.SacredTools.Loaded)
                            recipe.AddIngredient(ModCompatibility.SacredTools.Mod.Find<ModItem>("FloraFist"));

                        recipe.RemoveIngredient(ItemID.FireGauntlet);
                    });
                }

                // SacredTools compatibility
                if (ModCompatibility.SacredTools.Loaded)
                {
                    var sacred = ModCompatibility.SacredTools.Mod;

                    TryPatch(recipe, sacred, "StardustSigil", () =>
                    {
                        if (ModCompatibility.Homeward.Loaded)
                        {
                            recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("CounsellorBadge"));
                            recipe.RemoveIngredient(ItemID.SummonerEmblem);
                        }
                    });

                    TryPatch(recipe, sacred, "VortexSigil", () =>
                    {
                        if (ModCompatibility.Homeward.Loaded)
                        {
                            recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("BullseyeBadge"));
                            recipe.RemoveIngredient(ItemID.RangerEmblem);
                        }
                    });

                    TryPatch(recipe, sacred, "SolarSigil", () =>
                    {
                        if (ModCompatibility.Homeward.Loaded)
                        {
                            recipe.AddIngredient(ModCompatibility.Homeward.Mod.Find<ModItem>("SwordmasterBadge"));
                            recipe.RemoveIngredient(ItemID.WarriorEmblem);
                        }
                    });
                }

                // BaseForce logic
                if (recipe.createItem.ModItem is BaseForce && !recipe.HasIngredient<SolarFlareScoria>())
                {
                    recipe.AddIngredient<SolarFlareScoria>(4);
                }

                // FlightMasterySoul patch
                if (recipe.HasResult<FlightMasterySoul>() && !recipe.HasIngredient<Altitude>())
                {
                    recipe.RemoveIngredient(1131); // Harpy Wings
                    recipe.RemoveIngredient(1871); // Jetpack
                    recipe.RemoveIngredient(822);  // Angel Wings
                    recipe.RemoveIngredient(821);  // Demon Wings
                    recipe.AddIngredient<Altitude>();
                    recipe.AddIngredient<FinalBar>(5);
                }

                // FinalBar patch for multiple souls
                if ((recipe.HasResult<ColossusSoul>() || recipe.HasResult<ArchWizardsSoul>() || recipe.HasResult<BerserkerSoul>() ||
                     recipe.HasResult<SnipersSoul>() || recipe.HasResult<ConjuristsSoul>()) && !recipe.HasIngredient<FinalBar>())
                {
                    recipe.AddIngredient<FinalBar>(5);
                }

                // Remove EssenceofBright from Zenith recipe
                if (recipe.HasResult(ItemID.Zenith) && recipe.HasIngredient<EssenceofBright>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<EssenceofBright>());
                }

                // Horizon and SupersonicSoul patch
                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    if (recipe.HasResult<Horizon>() && !recipe.HasIngredient<AeolusBoots>())
                    {
                        recipe.RemoveIngredient(5000); // Placeholder ID
                        recipe.AddIngredient<AeolusBoots>();
                    }

                    if (recipe.HasResult<SupersonicSoul>() && recipe.HasIngredient<Horizon>())
                    {
                        //recipe.RemoveIngredient<AeolusBoots>();
                        recipe.AddIngredient<Horizon>();
                    }
                }
            }
        }
    }
}
