using FargowiltasSouls.Content.Items.Summons;
using gcsep.Core;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity;
using NoxusBoss.Core.World.WorldSaving;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name, ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name, ModCompatibility.WrathoftheGods.Name)]
    internal class CalDlcItems : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<MutantsCurse>())
            {
                tooltips.Add(new TooltipLine(Mod, "PostND", $"[c/FF0000:Cross-mod Balance:] Can be used after defeating the Nameless Deity"));
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
