using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.Terrarium;

namespace gcsep.Crossmod.Boots
{
    /*
        * Progression look like this:
        * terraspark
        * zephyr boots
        * angel treads
        * aeolus boots
        * terrarium particle sprinters
        * celestial treads
        * elysean tracers
        * seraph tracers.
    */

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class TorCalBootsRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.SacredTools.Name);
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // zephyr to treads (if no dlc)
                if (recipe.HasResult(ModContent.ItemType<AngelTreads>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                    recipe.AddIngredient<ZephyrBoots>(1);
                }
                //treads to aeolus (if no cal dlc)
                if (recipe.HasResult(ModContent.ItemType<AeolusBoots>()) && recipe.HasIngredient<ZephyrBoots>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<ZephyrBoots>());
                    recipe.AddIngredient<AngelTreads>(1);
                }
                //aeolus to sprinters
                if (recipe.HasResult(ModContent.ItemType<TerrariumParticleSprinters>()) && !recipe.HasIngredient<AeolusBoots>())
                {
                    recipe.AddIngredient<AeolusBoots>(1);
                }
                if (recipe.HasResult(ModContent.ItemType<TerrariumParticleSprinters>()) && recipe.HasIngredient(5000))
                {
                    recipe.RemoveIngredient(5000);
                }
                //sprinters to celestial
                if (recipe.HasResult(ModContent.ItemType<MoonWalkers>()) && !recipe.HasIngredient<TerrariumParticleSprinters>())
                {
                    recipe.AddIngredient<TerrariumParticleSprinters>(1);
                }
                if (recipe.HasResult(ModContent.ItemType<MoonWalkers>()) && recipe.HasIngredient<AeolusBoots>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<AeolusBoots>());
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Thorium.Name)]
    public class TorCalBootsEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Boots && !ModLoader.HasMod(ModCompatibility.SacredTools.Name);
        }
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<AngelTreads>()
                || Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<MoonWalkers>()
                || Item.type == ModContent.ItemType<VoidStriders>()
                || Item.type == ModContent.ItemType<SeraphTracers>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "ZephyrBoots").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<TerrariumParticleSprinters>()
                || Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<MoonWalkers>()
                || Item.type == ModContent.ItemType<VoidStriders>()
                || Item.type == ModContent.ItemType<SeraphTracers>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Calamity.Name, "AngelTreads").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<AeolusBoots>()
                || Item.type == ModContent.ItemType<MoonWalkers>()
                || Item.type == ModContent.ItemType<VoidStriders>()
                || Item.type == ModContent.ItemType<SeraphTracers>())
            {
                ModContent.Find<ModItem>(ModCompatibility.Thorium.Name, "TerrariumParticleSprinters").UpdateAccessory(player, false);
            }

            if (Item.type == ModContent.ItemType<MoonWalkers>()
                || Item.type == ModContent.ItemType<VoidStriders>()
                || Item.type == ModContent.ItemType<SeraphTracers>())
            {
                ModContent.Find<ModItem>(ModCompatibility.SoulsMod.Name, "AeolusBoots").UpdateAccessory(player, false);
            }
        }
    }

}

