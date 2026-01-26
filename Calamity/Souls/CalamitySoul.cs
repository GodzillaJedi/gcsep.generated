using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Calamity.Addons;
using gcsep.Calamity.Forces;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using gcsep.CrossMod.CraftingStations;
using Terraria;
using Terraria.DataStructures;
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
            ModContent.GetInstance<ExplorationForceEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DevastationForceEx>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AnnihilationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DesolationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ExaltationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GaleForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementsForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SalvationForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<TheCommunity>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ShatteredCommunity>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<ElementalArtifact>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BrandoftheBrimstoneWitch>().UpdateAccessory(player, hideVisual);
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
