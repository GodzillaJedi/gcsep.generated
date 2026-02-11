using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Souls
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    public class ElementalArtifact : ModItem
    {
        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = ItemRarityID.Red;
            this.Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ChaosStoneEffect>(Item);
            player.AddEffect<CryoStoneEffect>(Item);
            player.AddEffect<AeroStoneEffect>(Item);
            player.AddEffect<BloomStoneEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            recipe.AddIngredient<AeroStone>();
            recipe.AddIngredient<CryoStone>();
            recipe.AddIngredient<ChaosStone>();
            recipe.AddIngredient<BloomStone>();
            recipe.AddIngredient<GalacticaSingularity>(5);

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register();
        }

        [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
        [ExtendsFromMod(ModCompatibility.Calamity.Name)]
        public class ChaosStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<ChaosStone>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().ChaosStone = true;
            }
        }
        public class CryoStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<CryoStone>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().CryoStone = true;
                player.Calamity().ColdDebuffMultiplier += 0.5f;
            }
        }
        public class AeroStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<AeroStone>();
            public static int FlightTimeBoostFlat = 50;
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().aeroStone = true;
                player.wingTimeMax += FlightTimeBoostFlat;
            }
        }
        public class BloomStoneEffect : CalamitySoulEffect
        {
            public override int ToggleItemType => ModContent.ItemType<BloomStone>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.25f, 0.4f, 0.2f);
                calamityPlayer.healingPotionMultiplier += 0.5f;
                calamityPlayer.bloomStone = true;
                calamityPlayer.bloomStoneHookVisuals = true;
            }
        }
    }
}
