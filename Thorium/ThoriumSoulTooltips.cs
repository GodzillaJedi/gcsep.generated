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

            // Thorium Colossus Soul
            if (item.type == ModContent.ItemType<ColossusSoul>())
                AddTooltip("ThoriumColossusSoul", "ThoriumColossus");

            // Thorium Universe Soul
            if (item.type == ModContent.ItemType<UniverseSoul>())
                AddTooltip("ThoriumUniverseSoul", "ThoriumUniverse");

            // Thorium Dimension Soul
            if (item.type == ModContent.ItemType<DimensionSoul>())
                AddTooltip("ThoriumDimensionSoul", "ThoriumColossus");
        }
    }
}
