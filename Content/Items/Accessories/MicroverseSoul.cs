using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core;
using gcsep.Consolaria.Forces;
using gcsep.Core;
using gcsep.Polarities.Forces;
using gcsep.Redemption.Forces;
using gcsep.SOTS.Forces;
using gcsep.SpiritMod.Forces;
using gcsep.Spooky.Forces;
using gcsep.Vitality.Forces;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Items.Accessories
{
    public class MicroverseSoul : BaseSoul
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationRectangularV(6, 8, 10));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.value = 5000000;
            Item.rare = 11;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ModCompatibility.Vitality.Loaded)
            {
                ModContent.GetInstance<ForceOfChaos>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<ForceOfEvil>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<ForceOfNature>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<ForceOfOre>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Spooky.Loaded)
            {
                ModContent.GetInstance<HorrorForce>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<TerrorForce>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.SOTS.Loaded)
            {
                ModContent.GetInstance<SecretsForce>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<ShadowsForce>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Polarities.Loaded)
            {
                ModContent.GetInstance<SpacetimeForce>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<WildernessForce>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Redemption.Loaded)
            {
                ModContent.GetInstance<AdvancementForce>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Consolaria.Loaded)
            {
                ModContent.GetInstance<HeroForce>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            if (ModCompatibility.Redemption.Loaded || ModCompatibility.Polarities.Loaded ||
                ModCompatibility.Spooky.Loaded || ModCompatibility.Consolaria.Loaded ||
                ModCompatibility.SOTS.Loaded || ModCompatibility.Vitality.Loaded)
            {
                Recipe recipe = CreateRecipe(1);

                if (ModCompatibility.Spooky.Loaded)
                {
                    recipe.AddIngredient<HorrorForce>(1);
                    recipe.AddIngredient<TerrorForce>(1); // fixed typo
                }
                if (ModCompatibility.Vitality.Loaded)
                {
                    recipe.AddIngredient<ForceOfChaos>(1);
                    recipe.AddIngredient<ForceOfEvil>(1);
                    recipe.AddIngredient<ForceOfNature>(1);
                    recipe.AddIngredient<ForceOfOre>(1);
                }
                if (ModCompatibility.SOTS.Loaded)
                {
                    recipe.AddIngredient<SecretsForce>(1);
                    recipe.AddIngredient<ShadowsForce>(1);
                }
                if (ModCompatibility.Polarities.Loaded)
                {
                    recipe.AddIngredient<WildernessForce>(1);
                    recipe.AddIngredient<SpacetimeForce>(1);
                }
                if (ModCompatibility.Redemption.Loaded)
                {
                    recipe.AddIngredient<AdvancementForce>(1);
                }

                recipe.AddIngredient<AbomEnergy>(10);
                recipe.AddTile<CrucibleCosmosSheet>();
                recipe.Register();
            }
        }
    }
}
