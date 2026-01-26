using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Content.Items.Accessories;
using gcsep.Core;
using gcsep.Vitality.Enchantments;
using gcsep.Vitality.Forces;
using Terraria;
using Terraria.ModLoader;
using VitalityMod.Items.Souls;

namespace gcsep.Vitality
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    public class VitalityRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SOTS;
        }
        public override void PostAddRecipes()
        {

            for (int i = 0; i < Main.recipe.Length; i++)
            {
                Recipe recipe = Main.recipe[i];

                // Souls → add FragmentOfChaos
                if (!ModCompatibility.Calamity.Loaded &&
                    !ModCompatibility.SacredTools.Loaded &&
                    !recipe.HasIngredient<SoulofVitality>())
                {
                    if (recipe.HasResult<UniverseSoul>() ||
                        recipe.HasResult<TerrariaSoul>() ||
                        recipe.HasResult<MasochistSoul>() ||
                        recipe.HasResult<DimensionSoul>())
                    {
                        recipe.AddIngredient<SoulofVitality>(5);
                    }
                }

                // ScubaEnchant → TrawlerSoul
                if (recipe.HasResult<TrawlerSoul>() &&
                    !recipe.HasIngredient<ScubaEnchant>())
                {
                    recipe.AddIngredient<ScubaEnchant>();
                }

                // BaseForce items → add FragmentOfChaos
                if (recipe.createItem.ModItem is BaseForce &&
                    !recipe.HasIngredient<SoulofVitality>())
                {
                    recipe.AddIngredient<SoulofVitality>(4);
                }

                // MicroverseSoul → ForceOfChaos
                if (recipe.HasResult<MicroverseSoul>() &&
                    !recipe.HasIngredient<ForceOfChaos>())
                {
                    recipe.AddIngredient<ForceOfChaos>();
                }

                // MicroverseSoul → ForceOfEvil
                if (recipe.HasResult<MicroverseSoul>() &&
                    !recipe.HasIngredient<ForceOfEvil>())
                {
                    recipe.AddIngredient<ForceOfEvil>();
                }

                // MicroverseSoul → ForceOfEvil
                if (recipe.HasResult<MicroverseSoul>() &&
                    !recipe.HasIngredient<ForceOfNature>())
                {
                    recipe.AddIngredient<ForceOfNature>();
                }

                // MicroverseSoul → ForceOfOre
                if (recipe.HasResult<MicroverseSoul>() &&
                    !recipe.HasIngredient<ForceOfOre>())
                {
                    recipe.AddIngredient<ForceOfOre>();
                }
            }
        }
    }
}
