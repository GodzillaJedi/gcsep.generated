using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Calamity.Addons;
using gcsep.Calamity.Enchantments;
using gcsep.Calamity.Forces;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using gcsep.CrossMod.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace gcsep.Calamity.Souls
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    public class CalamitySoul : BaseSoul
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationRectangularV(6, 6, 10));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(10, 0, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.defense = 30;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //AnnihilationForce
            ModContent.GetInstance<EmpyreanEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PlagueReaperEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PlaguebringerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FearfallenEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BrimflameEnchant>().UpdateAccessory(player, hideVisual);
            //DesolationForce
            ModContent.GetInstance<MolluskEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<OmegaBlueEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FathomSwarmerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<UmbraphileEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AstralEnchant>().UpdateAccessory(player, hideVisual);
            //DevastationForceEX
            ModContent.GetInstance<HydrothermicEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TitanHeartEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DaedalusEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ReaverEnchantEx>().UpdateAccessory(player, hideVisual);
            //ExaltationForce
            ModContent.GetInstance<TarragonEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BloodflareEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GodSlayerEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SilvaEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AuricTeslaEnchant>().UpdateAccessory(player, hideVisual);
            //ExplorationForceEX
            ModContent.GetInstance<WulfrumEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AerospecEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DesertProwlerEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MarniteEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<VictideEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SulphurousEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<StatigelEnchantEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SnowRuffianEnchantEx>().UpdateAccessory(player, hideVisual);
            //SalvationForce
            ModContent.GetInstance<DemonShadeEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<LunicCorpEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GemTechEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PrismaticEnchant>().UpdateAccessory(player, hideVisual);
            //FargoCrossMod
            ModContent.GetInstance<GaleForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementsForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BrandoftheBrimstoneWitch>().UpdateAccessory(player, hideVisual);
            //CalamityMod
            ModContent.GetInstance<TheCommunity>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShatteredCommunity>().UpdateAccessory(player, hideVisual);
            //LesserSouls
            ModContent.GetInstance<ElementalArtifact>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PotJT>().UpdateAccessory(player, hideVisual);
            if (ModCompatibility.Catalyst.Loaded &&
                ModCompatibility.Goozma.Loaded &&
                ModCompatibility.Clamity.Loaded &&
                ModCompatibility.WrathoftheGods.Loaded &&
                ModCompatibility.Entropy.Loaded)
            {
                ModContent.GetInstance<AddonsForce>().UpdateAccessory(player, hideVisual);
            }
            // safer buff lookup
            if (ModLoader.TryGetMod("FargoCross", out var fargoCross) &&
                fargoCross.TryFind("CalamitousPresenceBuff", out ModBuff calamBuff))
            {
                player.buffImmune[calamBuff.Type] = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ShatteredCommunity>());
            recipe.AddIngredient(ModContent.ItemType<TheCommunity>());
            recipe.AddIngredient(ModContent.ItemType<ExplorationForceEx>());
            recipe.AddIngredient(ModContent.ItemType<DevastationForceEx>());
            recipe.AddIngredient(ModContent.ItemType<DesolationForce>());
            recipe.AddIngredient(ModContent.ItemType<AnnihilationForce>());
            recipe.AddIngredient(ModContent.ItemType<ExaltationForce>());
            recipe.AddIngredient(ModContent.ItemType<SalvationForce>());
            recipe.AddIngredient(ModContent.ItemType<GaleForce>());
            recipe.AddIngredient(ModContent.ItemType<ElementsForce>());
            recipe.AddIngredient(ModContent.ItemType<BrandoftheBrimstoneWitch>());
            recipe.AddIngredient(ModContent.ItemType<PotJT>());
            if (ModCompatibility.Catalyst.Loaded &&
                ModCompatibility.Goozma.Loaded &&
                ModCompatibility.Clamity.Loaded &&
                ModCompatibility.WrathoftheGods.Loaded &&
                ModCompatibility.Entropy.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<AddonsForce>());
            }

            recipe.AddIngredient(ModContent.ItemType<AbomEnergy>(), 10);
            recipe.AddIngredient(ModContent.ItemType<ShadowspecBar>(), 5);
            recipe.AddTile(ModContent.TileType<DemonshadeWorkbenchTile>());

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        [ExtendsFromMod(ModCompatibility.Calamity.Name)]
        public abstract class CalamitySoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<CalamitySoulHeader>();
        }
    }
}
