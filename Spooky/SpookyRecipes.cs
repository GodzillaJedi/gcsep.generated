using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using gcsep.Spooky.Forces;
using SOTS.Items.Fragments;
using SpiritMod.Items.BossLoot.AtlasDrops;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Items.SpookyHell.Armor;
using Spooky.Content.Items.SpookyHell.Misc;
using SpookyBardHealer.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Spooky
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name, ModCompatibility.SpookyBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name, ModCompatibility.SpookyBardHealer.Name)]
    public class SpookyRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Spooky;
        }
        public override void AddRecipeGroups()
        {
            // -------------------------
            // Gore Helmet group
            // -------------------------
            List<int> goreHelmets = new List<int>
            {
                ModContent.ItemType<GoreHoodEye>(),
                ModContent.ItemType<GoreHoodMouth>()
            };

            // Add SpookyBardHealer items if the mod is loaded
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                goreHelmets.Add(ModContent.ItemType<GoreEarVisor>());
                goreHelmets.Add(ModContent.ItemType<GoreWitchHat>());
                goreHelmets.Add(ModContent.ItemType<GoreHandVisage>());
            }

            // Register the group
            RecipeGroup goreGroup = new RecipeGroup(
                () => Lang.misc[37] + " Gore Helmet",
                goreHelmets.ToArray()
            );
            RecipeGroup.RegisterGroup("gcsep:AnyGoreHelmet", goreGroup);
            // -------------------------
            // Gilded Hat group (with conditional items)
            // -------------------------
            List<int> gildedHats = new List<int>
            {
                ModContent.ItemType<WizardGangsterHead>(),
                ModContent.ItemType<WizardGangsterHead2>(),
            };

            // Add this one only if the mod is loaded
            if (ModCompatibility.SpookyBardHealer.Loaded)
                gildedHats.Add(ModContent.ItemType<FiestaMaskara>());

            RecipeGroup group1 = new RecipeGroup(
                () => Lang.misc[37] + " Gilded Hat",
                gildedHats.ToArray()
            );

            RecipeGroup.RegisterGroup("gcsep:AnyGildedHat", group1);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.Thorium.Loaded)
                {
                    if ((recipe.HasResult<UniverseSoul>() ||
                         recipe.HasResult<TerrariaSoul>() ||
                         recipe.HasResult<MasochistSoul>() ||
                         recipe.HasResult<DimensionSoul>()) &&
                        !recipe.HasIngredient<ArteryPiece>())
                    {
                        recipe.AddIngredient<ArteryPiece>(5);
                    }
                }

                if (recipe.createItem.ModItem is BaseForce &&
                    !recipe.HasIngredient<ArteryPiece>())
                {
                    recipe.AddIngredient<ArteryPiece>(4);
                }

                if (recipe.HasResult<TerrariaSoul>() &&
                    !recipe.HasIngredient<HorrorForce>())
                {
                    recipe.AddIngredient<HorrorForce>();
                    recipe.AddIngredient<TerrorForce>();
                }
            }
        }
    }
}