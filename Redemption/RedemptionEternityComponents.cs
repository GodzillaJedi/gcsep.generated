using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using gcsep.Content.Items.Accessories;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Redemption
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionEternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item Item, Player player, bool hideVisual)
        {
            if (Item.type == ModContent.ItemType<ColossusSoul>() || Item.type == ModContent.ItemType<DimensionSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>())
            {
                if (ModCompatibility.Redemption.Loaded)
                {
                    player.AddEffect<HEVEffect>(Item);
                }
            }
            if (Item.type == ModContent.ItemType<SupersonicSoul>() || Item.type == ModContent.ItemType<DimensionSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>())
            {
                if (ModCompatibility.Redemption.Loaded)
                {
                    player.AddEffect<InfectionShieldEffect>(Item);
                }
            }
        }
        [ExtendsFromMod(ModCompatibility.Redemption.Name)]
        public class HEVEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<HEVSuit>();
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Redemption.Mod.Find<ModItem>("HEVSuit").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Redemption.Name)]
        public class InfectionShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
            public override int ToggleItemType => ModContent.ItemType<InfectionShield>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Redemption.Mod.Find<ModItem>("InfectionShield").UpdateAccessory(player, true);
            }
        }
    }
}
