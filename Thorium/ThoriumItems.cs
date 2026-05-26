using gcsep.Core;
using gcsep.Thorium.Buffs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Terrarium;

namespace gcsep.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumItems : GlobalItem
    {
        static string ExpandedTooltipLoc(string key) =>
            Language.GetTextValue($"Mods.gcsep.ExpandedTooltips.{key}");

        public override void SetDefaults(Item item)
        {
            // Terrarium Defender: defense buff
            if (item.type == ModContent.ItemType<TerrariumDefender>())
                item.defense = 8;

            // Omni Cannon: 15% damage nerf
            if (item.type == ModContent.ItemType<OmniCannon>())
                item.damage = (int)(item.damage * 0.85f);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
        {
            // Omni Cannon: +60% projectile velocity
            if (item.type == ModContent.ItemType<OmniCannon>())
                velocity *= 1.6f;

            // Quasar’s Flare: +70% projectile velocity
            if (item.type == ModContent.ItemType<QuasarsFlare>())
                velocity *= 1.7f;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.social)
                return;

            void AddTooltip(string internalName, string locKey)
            {
                tooltips.Add(new TooltipLine(Mod, internalName, ExpandedTooltipLoc(locKey)));
            }

            // Omni Cannon
            if (item.type == ModContent.ItemType<OmniCannon>())
                AddTooltip("Thorium_OmniCannon", "OmniCannon_Rebalance");

            // Quasar’s Flare
            if (item.type == ModContent.ItemType<QuasarsFlare>())
                AddTooltip("Thorium_QuasarsFlare", "QuasarsFlare_Rebalance");
        }

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            var thorium = player.GetModPlayer<CSEThoriumPlayer>();

            // Thunder Talon Eternity: apply Bone Javelin debuff
            if (thorium.ThunderTalonEternity)
                target.AddBuff(BuffID.BoneJavelin, 300);

            // Darkened Cloak: 25% chance to gain Soul Strength
            if (thorium.DarkenedCloak && Main.rand.NextBool(4))
                player.AddBuff(ModContent.BuffType<SoulStrength>(), 120);
        }
    }
}
