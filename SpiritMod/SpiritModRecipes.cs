using CalamityMod.Items.Materials;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using gcsep.SpiritMod.Souls;
using SacredTools.Content.Items.Materials;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.BossLoot.AtlasDrops;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.SpiritMod
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SpiritModRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (!ModCompatibility.Calamity.Loaded && !ModCompatibility.SacredTools.Loaded)
                {
                    if ((recipe.HasResult<UniverseSoul>() ||
                         recipe.HasResult<TerrariaSoul>() ||
                         recipe.HasResult<MasochistSoul>() ||
                         recipe.HasResult<DimensionSoul>()) &&
                        !recipe.HasIngredient<ArcaneGeyser>())
                    {
                        recipe.AddIngredient<ArcaneGeyser>(5);
                    }
                }

                if (recipe.createItem.ModItem is BaseForce &&
                    !recipe.HasIngredient<ArcaneGeyser>())
                {
                    recipe.AddIngredient<ArcaneGeyser>(4);
                }

                if (recipe.HasResult<TrawlerSoul>() &&
                    !recipe.HasIngredient<KoiTotem>())
                {
                    recipe.AddIngredient<KoiTotem>();
                }

                if (recipe.HasResult<EternitySoul>() &&
                    !recipe.HasIngredient<SpiritSoul>())
                {
                    recipe.AddIngredient<SpiritSoul>();
                }
            }
        }
    }
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name, ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name, ModCompatibility.Calamity.Name)]
    public class SpiritCalRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<SpiritSoul>() &&
                    !recipe.HasIngredient<ShadowspecBar>())
                {
                    recipe.AddIngredient<ShadowspecBar>(5);
                }
            }
        }
    }
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name, ModCompatibility.SacredTools.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name, ModCompatibility.SacredTools.Name)]
    public class SpiritSoARecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<SpiritSoul>() &&
                    !recipe.HasIngredient<EmberOfOmen>())
                {
                    recipe.AddIngredient<EmberOfOmen>(5);
                }
            }
        }
    }
}
