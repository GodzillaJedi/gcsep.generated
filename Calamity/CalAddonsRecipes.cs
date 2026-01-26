using CatalystMod.Items.Armor.Intergelactic;
using gcsep.Core;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name)]
    public class CatalystRecipes : ModSystem
    {
        public override void AddRecipeGroups()
        {
            RecipeGroup rec99 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Intergelactic Helmet", ModContent.ItemType<IntergelacticHeadMagic>(), ModContent.ItemType<IntergelacticHeadMelee>(), ModContent.ItemType<IntergelacticHeadSummon>(), ModContent.ItemType<IntergelacticHeadRanged>(), ModContent.ItemType<IntergelacticHeadRogue>());
            RecipeGroup.RegisterGroup("gcsep:IntergelacticHelmet", rec99);

        }
    }
}


