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
        static string ExpandedTooltipLoc(string line) => Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{line}");
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.gcsep.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<MasochistSoul>() && !item.social)
            {
                tooltips.Insert(9, new TooltipLine(Mod, "SoARampartDeities", Language.GetTextValue(key + "SoARampart")));
            }
        }
    }
}
