using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Content.Items.Accessories;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PostML;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
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
            if (Item.type == ModContent.ItemType<ConjuristsSoul>() || Item.type == ModContent.ItemType<UniverseSoul>() || Item.type == ModContent.ItemType<EternitySoul>() || Item.type == ModContent.ItemType<StargateSoul>())
            {
                if (ModCompatibility.Redemption.Loaded)
                {
                    player.AddEffect<PortableHoloProjectorEffect>(Item);
                }
            }
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
        [ExtendsFromMod(ModCompatibility.Redemption.Name)]
        public class PortableHoloProjectorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
            public override int ToggleItemType => ModContent.ItemType<InfectionShield>();

            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_ItemUse(EffectItem(player));
                    if (player.FindBuffIndex(ModContent.BuffType<HoloMinionBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<HoloMinionBuff>(), 3600);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<HoloProjector>()] < 1)
                    {
                        int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(140f);
                        Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HoloProjector>(), num, 0f, player.whoAmI).originalDamage = num;
                    }
                }
            }
        }
    }
}
