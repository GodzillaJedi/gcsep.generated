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
        static string ExpandedTooltipLoc(string key) =>
            Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{key}");

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.social)
                return;

            void AddTooltip(string internalName, string locKey)
            {
                tooltips.Add(new TooltipLine(Mod, internalName, ExpandedTooltipLoc(locKey)));
            }

            // Colossus Soul
            if (item.type == ModContent.ItemType<ColossusSoul>())
            {
                AddTooltip("SOTSColossusSoul", "SOTSColossus");
            }
            //Flight Mastey Soul
            if (item.type == ModContent.ItemType<FlightMasterySoul>())
            {
                AddTooltip("SOTSFlightMasterySoul", "SOTSFlightMastery");
            }
            // Dimension Soul
            if (item.type == ModContent.ItemType<DimensionSoul>())
            {
                tooltips.Add(new TooltipLine(Mod, "SOTSDimensionSoul",
                    ExpandedTooltipLoc("SOTSFlightMastery") + "\n" +
                    ExpandedTooltipLoc("SOTSColossus")
                ));
            }
        }
    }
}
