using FargowiltasSouls.Content.Items.Summons;
using gcsep.Core;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity;
using NoxusBoss.Core.World.WorldSaving;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name, ModCompatibility.WrathoftheGods.Name)]
    internal class CalDlcItems : GlobalItem
    {
        static string ExpandedTooltipLoc(string key) =>
            Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{key}");

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.social)
                return;

            // Mutant's Curse tooltip
            if (item.type == ModContent.ItemType<MutantsCurse>())
            {
                tooltips.Add(new TooltipLine(Mod, "PostND",
                    ExpandedTooltipLoc("MutantsCurse_PostND")));
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<MutantsCurse>())
                return BossDownedSaveSystem.HasDefeated<NamelessDeityBoss>();

            return true;
        }
    }
}
