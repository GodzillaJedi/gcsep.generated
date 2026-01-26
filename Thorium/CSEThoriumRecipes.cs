using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Summons;
using gcsep.Core;
using gcsep;
using gcsep.Thorium.Souls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;
using ThoriumMod.Items.BossThePrimordials.Slag;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.Titan;

namespace gcsep.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    class CSEThoriumRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            //jester mask
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Jester Mask", ModContent.ItemType<JestersMask>(), ModContent.ItemType<JestersMask2>());
            RecipeGroup.RegisterGroup("gcsep:AnyJesterMask", group);
            //jester shirt
            group = new RecipeGroup(() => Lang.misc[37] + " Jester Shirt", ModContent.ItemType<JestersShirt>(), ModContent.ItemType<JestersShirt2>());
            RecipeGroup.RegisterGroup("gcsep:AnyJesterShirt", group);
            //jester legging
            group = new RecipeGroup(() => Lang.misc[37] + " Jester Leggings", ModContent.ItemType<JestersLeggings>(), ModContent.ItemType<JestersLeggings2>());
            RecipeGroup.RegisterGroup("gcsep:AnyJesterLeggings", group);
            //evil wood tambourine
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood Tambourine", ModContent.ItemType<EbonWoodTambourine>(), ModContent.ItemType<ShadeWoodTambourine>());
            RecipeGroup.RegisterGroup("gcsep:AnyTambourine", group);
            //fan letter
            group = new RecipeGroup(() => Lang.misc[37] + " Fan Letter", ModContent.ItemType<FanLetter>(), ModContent.ItemType<FanLetter2>());
            RecipeGroup.RegisterGroup("gcsep:AnyLetter", group);
            //bugle horn
            group = new RecipeGroup(() => Lang.misc[37] + " Bugle Horn", ModContent.ItemType<GoldBugleHorn>(), ModContent.ItemType<PlatinumBugleHorn>());
            RecipeGroup.RegisterGroup("gcsep:AnyBugleHorn", group);
            //titan 
            group = new RecipeGroup(() => Lang.misc[37] + " Titan Headgear", ModContent.ItemType<TitanHelmet>(), ModContent.ItemType<TitanMask>(), ModContent.ItemType<TitanHeadgear>());
            RecipeGroup.RegisterGroup("gcsep:AnyTitanHelmet", group);
            //any gem
            group = new RecipeGroup(() => Lang.misc[37] + " Gem", ModContent.ItemType<Opal>(), ModContent.ItemType<Aquamarine>());
            RecipeGroup.RegisterGroup("gcsep:AnyThoriumGem", group);
            // rhapsodist
            group = new RecipeGroup(() => Lang.misc[37] + " Rhapsodist Helmet", ModContent.ItemType<SoloistHat>(), ModContent.ItemType<InspiratorsHelmet>());
            RecipeGroup.RegisterGroup("gcsep:AnyRhapsodistHelmet", group);
        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<CoffinSummon>())
                .AddIngredient(ItemID.ClayBlock, 15)
                .AddIngredient(ItemID.FossilOre, 8)
                .AddRecipeGroup("gcsep:AnyThoriumGem", 4)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // Helpers (no naming conflicts)
                bool ResultIs<T>() where T : ModItem => recipe.HasResult<T>();
                bool IngredientIs<T>() where T : ModItem => recipe.HasIngredient<T>();
                bool IngredientIsID(int id) => recipe.HasIngredient(id);
                void AddIng<T>(int stack = 1) where T : ModItem => recipe.AddIngredient<T>(stack);

                // Thorium → Eternity Soul
                if (GCSEConfig.Instance.Thorium &&
                    ResultIs<EternitySoul>() &&
                    !IngredientIs<ThoriumSoul>())
                {
                    AddIng<ThoriumSoul>();
                }

                // Universe Soul → Bard + Guardian Angels
                if (ResultIs<UniverseSoul>() && !IngredientIs<BardSoul>())
                {
                    AddIng<GuardianAngelsSoul>();
                    AddIng<BardSoul>();
                }

                // Universe Soul → Olympians (no Calamity)
                if (!ModCompatibility.Calamity.Loaded &&
                    ResultIs<UniverseSoul>() &&
                    !IngredientIs<OlympiansSoul>())
                {
                    AddIng<OlympiansSoul>();
                }

                // Colossus Soul → Blast Shield
                if (ResultIs<ColossusSoul>() && !IngredientIs<GuardianAngelsSoul>())
                    AddIng<BlastShield>();

                // Terrarium Defender → Corrupted War Shield
                if (ResultIs<TerrariumDefender>() && !IngredientIs<CorruptedWarShield>())
                    AddIng<CorruptedWarShield>();

                // Hungering Blossom → Replace Mana Flower with Nature's Gift
                if (ResultIs<HungeringBlossom>() && !IngredientIsID(ItemID.NaturesGift))
                {
                    recipe.RemoveIngredient(ItemID.ManaFlower);
                    recipe.AddIngredient(ItemID.NaturesGift);
                }

                // Cosmo Force → Essences
                if (ResultIs<CosmoForce>() && !IngredientIs<OceanEssence>())
                {
                    AddIng<OceanEssence>(2);
                    AddIng<InfernoEssence>(2);
                    AddIng<DeathEssence>(2);
                }

                // Styx Armor → Replace vanilla mats with essences
                if (ResultIs<StyxCrown>() && IngredientIsID(549))
                {
                    recipe.RemoveIngredient(549);
                    recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 10);
                }

                if (ResultIs<StyxLeggings>() && IngredientIsID(547))
                {
                    recipe.RemoveIngredient(547);
                    recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 10);
                }

                if (ResultIs<StyxChestplate>() &&  IngredientIsID(548))
                {
                    recipe.RemoveIngredient(548);
                    recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 10);
                }

                // Gaia Armor → Add materials
                if (ResultIs<GaiaHelmet>() && !IngredientIs<DarkMatter>())
                {
                    recipe.AddIngredient(ModContent.ItemType<HolyKnightsAlloy>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<BloomWeave>(), 6);
                }

                if (ResultIs<GaiaGreaves>() && !IngredientIs<DarkMatter>())
                {
                    recipe.AddIngredient(ModContent.ItemType<HolyKnightsAlloy>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 6);
                    recipe.AddIngredient(ModContent.ItemType<BloomWeave>(), 6);
                }

                if (ResultIs<GaiaPlate>() && !IngredientIs<DarkMatter>())
                {
                    recipe.AddIngredient(ModContent.ItemType<HolyKnightsAlloy>(), 9);
                    recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 9);
                    recipe.AddIngredient(ModContent.ItemType<BloomWeave>(), 9);
                }

                // Soul Tier → Add essences
                if ((ResultIs<ColossusSoul>() ||
                     ResultIs<FlightMasterySoul>() ||
                     ResultIs<ArchWizardsSoul>() ||
                     ResultIs<BerserkerSoul>() ||
                     ResultIs<SnipersSoul>() ||
                     ResultIs<ConjuristsSoul>()) &&
                     !IngredientIs<OceanEssence>())
                {
                    AddIng<OceanEssence>(5);
                    AddIng<InfernoEssence>(5);
                    AddIng<DeathEssence>(5);
                }

                // DCU → Replace bars with Terrarium Core
                if (recipe.HasResult(ItemID.DrillContainmentUnit) &&
                    !IngredientIs<TerrariumCore>())
                {
                    recipe.RemoveIngredient(ItemID.MeteoriteBar);
                    recipe.RemoveIngredient(ItemID.HellstoneBar);
                    recipe.RemoveIngredient(ItemID.ShroomiteBar);
                    recipe.RemoveIngredient(ItemID.SpectreBar);
                    recipe.RemoveIngredient(ItemID.ChlorophyteBar);
                    AddIng<TerrariumCore>(40);
                }
            }
        }
    }
}
