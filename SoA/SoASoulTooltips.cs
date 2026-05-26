using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    public class SoASoulsTooltips : GlobalItem
    {
        static string ExpandedTooltipLoc(string key) =>
            Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{key}");

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            void AddTooltip(string internalName, string locKey)
            {
                tooltips.Add(new TooltipLine(Mod, internalName, ExpandedTooltipLoc(locKey)));
            }

            if (item.social)
                return;

            if (item.type == ModContent.ItemType<ColossusSoul>())
                AddTooltip("SoAColossusSoul", "SoAColossus");

            if (item.type == ModContent.ItemType<SupersonicSoul>())
                AddTooltip("SoASupersonicSoul", "SoASupersonic");

            if (item.type == ModContent.ItemType<BerserkerSoul>())
                AddTooltip("SoABerserkerSoul", "SoABerserker");

            if (item.type == ModContent.ItemType<MasochistSoul>())
                AddTooltip("SoAMasoSoul", "SoAMaso");

            if (item.type == ModContent.ItemType<UniverseSoul>())
            {
                tooltips.Add(new TooltipLine(Mod, "SoAUniverseSoul",
                    ExpandedTooltipLoc("SoABerserker")
                // + "\n" + ExpandedTooltipLoc("SoASniper")
                // + "\n" + ExpandedTooltipLoc("SoAWizard")
                // + "\n" + ExpandedTooltipLoc("SoAConjurist")
                ));
            }

            if (item.type == ModContent.ItemType<DimensionSoul>())
            {
                tooltips.Add(new TooltipLine(Mod, "SoADimensionSoul",
                    // ExpandedTooltipLoc("SoATrawler") + "\n" +
                    ExpandedTooltipLoc("SoASupersonic") + "\n" +
                    ExpandedTooltipLoc("SoAColossus") + "\n" +
                    ExpandedTooltipLoc("SoAWorldshaper")
                ));
            }
        }
    }
}
