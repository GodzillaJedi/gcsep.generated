using FargowiltasSouls.Content.Items.Accessories.Souls;
using gcsep.Core;
using SOTS.Items.Wings;
using gcsep.Content.Items.Accessories;
using gcsep.SOTS.Souls;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace gcsep.SOTS
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name)]
    public class SOTSEternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        private static readonly Dictionary<int, List<Action<Player, bool>>> AccessoryEffects = new()
    {
        { ModContent.ItemType<EternitySoul>(), new() { ApplyVoidShield } },
        { ModContent.ItemType<ColossusSoul>(), new() { ApplyVoidShield } },
        { ModContent.ItemType<DimensionSoul>(), new() { ApplyVoidShield } },
        { ModContent.ItemType<StargateSoul>(), new() { ApplyVoidShield } },
        { ModContent.ItemType<FlightMasterySoul>(), new() { ApplyGildedBladeWings } }
    };

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (AccessoryEffects.TryGetValue(item.type, out var effects))
            {
                foreach (var effect in effects)
                    effect(player, hideVisual);
            }
        }

        private static void ApplyVoidShield(Player player, bool hideVisual)
        {
            ModContent.GetInstance<VoidShield>().UpdateAccessory(player, hideVisual);
        }

        private static void ApplyGildedBladeWings(Player player, bool hideVisual)
        {
            ModContent.GetInstance<GildedBladeWings>().UpdateAccessory(player, hideVisual);
        }
    }
}
