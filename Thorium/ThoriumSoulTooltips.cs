using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class ThoriumSoulsTooltips : GlobalItem
    {
        static string ExpandedTooltipLoc(string line) => Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{line}");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.gcsep.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(Mod, "ThoriumColossusSoul", Language.GetTextValue(key + "ThoriumColossus")));
            }

            if (item.type == ModContent.ItemType<UniverseSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "ThoriumUniverseSoul",
                    Language.GetTextValue(key + "ThoriumUniverse")));
            }

            if (item.type == ModContent.ItemType<DimensionSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "ThoriumDimestionSoul",
                    Language.GetTextValue(key + "ThoriumColossus")));
            }
        }
    }
}
