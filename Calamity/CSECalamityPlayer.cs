using CalamityMod;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.SummonItems;
using gcsep.Core;
using gcsep.Content.Buffs;
using gcsep.Items;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.FargoCrossmod.Name)]
    public class CSECalamityPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            for (int i = 0; i < Player.inventory.Length; i++)
            {
                Item item = Player.inventory[i];
                if (item.type == ModContent.ItemType<Terminus>() && item.active)
                {
                    item.SetDefaults(ModContent.ItemType<ShtunTerminus>());
                }
            }
        }
        public override void PostUpdateBuffs()
        {
            if (Player.HasBuff<NihilityPresenceBuff>())
            {
                ModLoader.GetMod("CalamityMod").TryFind("Enraged", out ModBuff enrage);
                Main.LocalPlayer.buffImmune[enrage.Type] = true;
            }
        }
        public static class AuricTeslaShared
        {
            public static void ApplyCommonBonuses(Player player)
            {
                player.thorns += 3f;
                player.ignoreWater = true;
                player.crimsonRegen = true;
            }

            public static void HandleGodSlayerDash(Player player)
            {
                var calamityPlayer = player.Calamity();
                if (calamityPlayer.godSlayerDashHotKeyPressed ||
                    (player.dashDelay != 0 && calamityPlayer.LastUsedDashID == GodslayerArmorDash.ID))
                {
                    calamityPlayer.DeferredDashID = GodslayerArmorDash.ID;
                }
            }
        }
    }
}
