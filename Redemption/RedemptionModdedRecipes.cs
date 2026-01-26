using CalamityMod.Items.Materials;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using Redemption.Items.Materials.PostML;
using gcsep.ClassSouls.Beekeeper.Essences;
using gcsep.ClassSouls.Beekeeper.Souls;
using gcsep.Redemption.Mutagens;
using gcsep.SoA.Essences;
using gcsep.SoA.Souls;
using gcsep.Thorium.Essences;
using gcsep.Thorium.Souls;
using Terraria;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Souls;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Essences;

namespace gcsep.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    public class RedemptionDlcRecipes : ModSystem
    {
        //public override bool IsLoadingEnabled(Mod mod)
        //{
        //    return !ModLoader.HasMod(ModCompatibility.SacredTools.Name) && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
        //}
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<VagabondsSoul>() && !recipe.HasResult<MutagenThrowingCal>())
                {
                    recipe.AddIngredient<MutagenThrowingCal>();
                    recipe.RemoveIngredient(ModContent.ItemType<OutlawsEssence>());
                }

                if (recipe.HasResult<ShadowspecBar>() && !recipe.HasResult<LifeFragment>())
                {
                    recipe.AddIngredient<LifeFragment>();
                }
                if (recipe.HasResult(ModContent.ItemType<ArchWizardsSoul>()))
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("Petridish"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("MutagenMagic"), 1); recipe.RemoveIngredient(ModContent.ItemType<ApprenticesEssence>()); }
                }
                if (recipe.HasResult(ModContent.ItemType<BerserkerSoul>()))
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("PZGauntlet"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("MutagenMelee"), 1); recipe.RemoveIngredient(ModContent.ItemType<BarbariansEssence>()); }
                }
                if (recipe.HasResult(ModContent.ItemType<ColossusSoul>()))
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("HEVSuit"), 1); }
                }
                if (recipe.HasResult(ModContent.ItemType<ConjuristsSoul>()))
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("PortableHoloProjector"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("CruxCardMossyGoliath"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("MutagenSummon"), 1); recipe.RemoveIngredient(ModContent.ItemType<OccultistsEssence>()); }
                }
                if (recipe.HasResult(ModContent.ItemType<SnipersSoul>()))
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("SwarmerCannon"), 1); recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("MutagenRanged"), 1); recipe.RemoveIngredient(ModContent.ItemType<SharpshootersEssence>()); }
                }
                if (recipe.HasResult(ModContent.ItemType<VagabondsSoul>()))
                {
                    if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModContent.ItemType<MutagenThrowingCal>(), 1); recipe.RemoveIngredient(ModContent.ItemType<OutlawsEssence>()); }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class RedemptionTorRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<BardSoul>() && !recipe.HasResult<MutagenSymphonic>())
                {
                    recipe.AddIngredient<MutagenSymphonic>();
                    recipe.RemoveIngredient(ModContent.ItemType<BardEssence>());
                }
                if (recipe.HasResult<GuardianAngelsSoul>() && !recipe.HasResult<MutagenHealing>())
                {
                    recipe.AddIngredient<MutagenHealing>();
                    recipe.RemoveIngredient(ModContent.ItemType<HealerEssence>());
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Thorium.Name)]
    public class RedemptionTorThrowerRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.SacredTools.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult<OlympiansSoul>() && !recipe.HasResult<MutagenThrowing>())
                {
                    recipe.AddIngredient<MutagenThrowing>();
                    recipe.RemoveIngredient(ModContent.ItemType<SlingerEssence>());
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.SacredTools.Name)]
    public class RedemptionSoARecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ModLoader.HasMod(ModCompatibility.Calamity.Name) && !ModLoader.HasMod(ModCompatibility.Thorium.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<StalkerSoul>() && !recipe.HasResult<MutagenThrowingSoA>())
                {
                    recipe.AddIngredient<MutagenThrowingSoA>();
                    recipe.RemoveIngredient(ModContent.ItemType<StalkerEssence>());
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("NebulaSigil")))
                    {
                        if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                    }
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("SolarSigil")))
                    {
                        if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                    }
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("StardustSigil")))
                    {
                        if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                    }
                }
                if (ModCompatibility.SacredTools.Loaded)
                {
                    if (recipe.HasResult(ModCompatibility.SacredTools.Mod.Find<ModItem>("VortexSigil")))
                    {
                        if (ModCompatibility.Redemption.Loaded) { recipe.AddIngredient(ModCompatibility.Redemption.Mod.Find<ModItem>("XeniumAlloy"), 3); }
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.BeekeeperClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.BeekeeperClass.Name)]
    public class RedemptionBeeRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.HasResult<BeekeeperSoul>() && !recipe.HasResult<MutagenBeekeeper>())
                {
                    recipe.AddIngredient<MutagenBeekeeper>();
                    recipe.RemoveIngredient(ModContent.ItemType<BeekeeperEssence>());
                }
            }
        }
    }

    //[ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.ClikerClass.Name)]
    //[JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.ClikerClass.Name)]
    //public class RedemptionClickerRecipes : ModSystem
    //{
    //    public override void PostAddRecipes()
    //    {
    //        for (int i = 0; i < Recipe.numRecipes; i++)
    //        {
    //            Recipe recipe = Main.recipe[i];
    //            if (recipe.HasResult<ClickerSoul>() && !recipe.HasResult<MutagenClicker>())
    //            {
    //                recipe.AddIngredient<MutagenClicker>();
    //                recipe.RemoveIngredient(ModContent.ItemType<ClickerEssence>());
    //            }
    //        }
    //    }
    //}
}
