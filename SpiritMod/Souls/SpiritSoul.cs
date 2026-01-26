using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.SpiritMod.Forces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Souls
{
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    public class SpiritSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SpiritMod;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationRectangularV(8, 5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 29;
            Item.height = 38;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.value = 5000000;
            Item.defense = 20;
            Item.rare = ItemRarityID.Expert;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<AdventurerForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AtlantisForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FrostburnForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<HurricaneForce>().UpdateAccessory(player, hideVisual);

            player.AddEffect<SpiritSoulEffect>(Item);
        }

        public class SpiritSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (!ModCompatibility.Calamity.Loaded) { recipe.AddIngredient<AbomEnergy>(10); }
            recipe.AddIngredient(ModContent.ItemType<AdventurerForce>());
            recipe.AddIngredient(ModContent.ItemType<AtlantisForce>());
            recipe.AddIngredient(ModContent.ItemType<FrostburnForce>());
            recipe.AddIngredient(ModContent.ItemType<HurricaneForce>());

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}