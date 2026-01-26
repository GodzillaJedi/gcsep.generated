using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using gcsep.Calamity.Souls;
using gcsep.ClassSouls.Beekeeper.Souls;
using gcsep.ClassSouls.Clicker.Souls;
using gcsep.Core;
using gcsep.Orchid.Souls;
using gcsep.SoA.Souls;
using gcsep.SpiritMod.Souls;
using gcsep.Thorium.Souls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Items.Accessories
{
    public class MacroverseSoul : BaseSoul
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.value = 5000000;
            Item.rare = 11;
            Item.accessory = true;
            Item.defense = 100;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // SacredTools soul effect
            if (ModCompatibility.SacredTools.Loaded)
            {
                ModContent.GetInstance<StalkerSoul>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<SoASoul>().UpdateAccessory(player, hideVisual);
            }
            // Calamity + Fargo crossmod soul effect
            if (ModCompatibility.FargoCrossmod.Loaded && ModCompatibility.Calamity.Loaded)
            {
                ModContent.GetInstance<CalamitySoul>().UpdateAccessory(player, hideVisual);
            }
            // Thorium soul effect
            if (ModCompatibility.Thorium.Loaded)
            {
                ModContent.GetInstance<ThoriumSoul>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<BardSoul>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<GuardianAngelsSoul>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<OlympiansSoul>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.SpiritMod.Loaded)
            {
                ModContent.GetInstance<SpiritSoul>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.BeekeeperClass.Loaded)
            {
                ModContent.GetInstance<BeekeeperSoul>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.ClickerClass.Loaded)
            {
                ModContent.GetInstance<ClickerSoul>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.Orchid.Loaded)
            {
                ModContent.GetInstance<SoulOfDiversity>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            if (ModCompatibility.Calamity.Loaded ||
                ModCompatibility.Thorium.Loaded ||
                ModCompatibility.SacredTools.Loaded ||
                ModCompatibility.BeekeeperClass.Loaded ||
                ModCompatibility.ClickerClass.Loaded ||
                ModCompatibility.Orchid.Loaded || 
                ModCompatibility.SpiritMod.Loaded)

            {
                Recipe recipe = CreateRecipe();

                if (ModCompatibility.Calamity.Loaded && ModCompatibility.FargoCrossmod.Loaded)
                {
                    recipe.AddIngredient<CalamitySoul>();
                }

                if (ModCompatibility.Thorium.Loaded)
                {
                    recipe.AddIngredient<ThoriumSoul>();
                    recipe.AddIngredient<OlympiansSoul>();
                    recipe.AddIngredient<BardSoul>();
                    recipe.AddIngredient<GuardianAngelsSoul>();
                }

                if (ModCompatibility.SacredTools.Loaded)
                {
                    recipe.AddIngredient<SoASoul>();
                    recipe.AddIngredient<StalkerSoul>();
                }

                if (ModCompatibility.BeekeeperClass.Loaded)
                {
                    recipe.AddIngredient<BeekeeperSoul>();
                }

                if (ModCompatibility.ClickerClass.Loaded)
                {
                    recipe.AddIngredient<ClickerSoul>();
                }
                if (ModCompatibility.Orchid.Loaded)
                {
                    recipe.AddIngredient<SoulOfDiversity>();
                }
                if (ModCompatibility.SpiritMod.Loaded)
                {
                    recipe.AddIngredient<SpiritSoul>();
                }

                recipe.AddIngredient<EternalEnergy>(30);
                recipe.AddTile<CrucibleCosmosSheet>();
                recipe.Register();
            }
        }
    }
}
