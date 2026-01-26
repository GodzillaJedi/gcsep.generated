using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SOTS
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name)]
    public class SOTSTooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.gcsep.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                tooltips.Insert(8, new TooltipLine(Mod, "SOTSColossusSoul", Language.GetTextValue(key + "SOTSColossus")));
                tooltips.Insert(4, new TooltipLine(Mod, "SOTSFlightMasterySoul", Language.GetTextValue(key + "SOTSFlightMastery")));
            }

            if (item.type == ModContent.ItemType<DimensionSoul>() && !item.social)
            {
                tooltips.Insert(15, new TooltipLine(Mod, "SOTSDimensionSoul", Language.GetTextValue(key + "SOTSFlightMastery") + "\n" + Language.GetTextValue(key + "SOTSColossus")));
            }
        }
    }
}
