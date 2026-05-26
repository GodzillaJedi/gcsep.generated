using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name, ModCompatibility.Calamity.Name)]
    public class SoACalTooltips : GlobalItem
    {
        static string ExpandedTooltipLoc(string line) =>
            Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{line}");

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<MasochistSoul>() && !item.social)
            {
                // Safely append the tooltip
                tooltips.Add(new TooltipLine(Mod, "SoARampartDeities",
                    ExpandedTooltipLoc("SoARampart")));
            }
        }
    }
}
