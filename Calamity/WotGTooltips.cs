using gcsep.Core;
using NoxusBoss.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.WrathoftheGods.Name)]
    public class WotGTooltips : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<CheatPermissionSlip>())
            {
                tooltips.Add(new TooltipLine(Mod, "PostMonstrocity", $"[c/FF0000:Cross-Mod Balance:] Can only be used after defeating the Monstrocity"));
            }
        }
    }
}
