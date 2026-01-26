using gcsep.Core;
using SacredTools;
using SacredTools.Common.Players;
using SacredTools.Content.Items.Accessories;
using System;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class PostSetupContentSoA
    {
        public static void PostSetupContent_Thorium()
        {
            int bossdmgItem = ModContent.ItemType<RageSuppressor>();
            int accuracyItem = ModContent.ItemType<CasterArcanum>();
            Func<string> bardDamage = () => $"Boss Damage: {Main.LocalPlayer.GetModPlayer<MiscEffectsPlayer>().bossDamage / 100}%";
            Func<string> bardCrit = () => $"Accuracy: {Main.LocalPlayer.GetModPlayer<ModdedPlayer>().accuracy}";
            ModCompatibility.MutantMod.Mod.Call("AddStat", bossdmgItem, bardDamage);
            ModCompatibility.MutantMod.Mod.Call("AddStat", accuracyItem, bardCrit);
        }
    }
}
