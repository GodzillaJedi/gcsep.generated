using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.SoA
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name, ModCompatibility.Calamity.Name)]
    public class CalTorTooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key = "Mods.gcsep.Items.AddedEffects.";

            if (item.type == ModContent.ItemType<ColossusSoul>() && !item.social)
            {
                tooltips.Insert(9, new TooltipLine(Mod, "CalTerrariumDefender", Language.GetTextValue(key + "CalDefender")));
            }
        }
    }
}
