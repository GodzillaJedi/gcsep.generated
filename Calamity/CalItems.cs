using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using gcsep.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    public class CalItems : GlobalItem
    {
        static string ExpandedTooltipLoc(string key) =>
            Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{key}");

        public override void SetDefaults(Item item)
        {
            // Sylvestaff: 70% nerf
            if (item.type == ModContent.ItemType<Sylvestaff>())
                item.damage = (int)(item.damage * 0.3f);

            // Voidragon: 10% nerf
            if (item.type == ModContent.ItemType<Voidragon>())
                item.damage = (int)(item.damage * 0.9f);

            // Orderbringer: 30% buff
            if (item.type == ModContent.ItemType<Orderbringer>())
                item.damage = (int)(item.damage * 1.3f);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.social)
                return;

            void AddTooltip(string internalName, string locKey)
            {
                tooltips.Add(new TooltipLine(Mod, internalName, ExpandedTooltipLoc(locKey)));
            }

            // Orderbringer
            if (item.type == ModContent.ItemType<Orderbringer>())
                AddTooltip("CSE_Orderbringer", "Orderbringer_Rebalance");

            // Voidragon
            if (item.type == ModContent.ItemType<Voidragon>())
                AddTooltip("CSE_Voidragon", "Voidragon_Rebalance");

            // Sylvestaff
            if (item.type == ModContent.ItemType<Sylvestaff>())
            {
                AddTooltip("CSE_Sylvestaff_1", "Sylvestaff_No");
                AddTooltip("CSE_Sylvestaff_2", "Sylvestaff_DamageNerf");
            }
        }
    }
}
