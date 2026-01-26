using gcsep.Core;
using gunrightsmod.Content.Items;
using gunrightsmod.Content.Items.Armor;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.gunrightsmod
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class gunrightsmodRecipes : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.TerMerica;
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup rec = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Kevlar Helmet", ModContent.ItemType<KevlarBeret>(), ModContent.ItemType<KevlarFedora>(), ModContent.ItemType<KevlarHelmet>(), ModContent.ItemType<KevlarMask>(), ModContent.ItemType<KevlarVisor>());
            RecipeGroup.RegisterGroup("gcsep:KevlarHelms", rec);
        }
    }
}
