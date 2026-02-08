using CalamityMod;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.BossThePrimordials.Rhapsodist;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Projectiles.Healer;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.NoviceClericEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SacredEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SacredEffect>(Item);
            if (player.AddEffect<KarmicEffect>(Item))
            {
                ModContent.GetInstance<KarmicHolder>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PaganEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(Item);

                if (player.FindBuffIndex(ModContent.BuffType<BloodyPaganStaffBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<BloodyPaganStaffBuff>(), 3600);
                }

                // Check the same projectile type you spawn
                if (player.ownedProjectileCounts[ModContent.ProjectileType<BloodyPaganStaffPro>()] < 1)
                {
                    int baseDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);

                    int projIndex = Projectile.NewProjectile(
                        source_ItemUse,
                        player.Center.X,
                        player.Center.Y,
                        0f,
                        -1f,
                        ModContent.ProjectileType<BloodyPaganStaffPro>(),
                        damage,
                        0f,
                        player.whoAmI
                    );

                    if (Main.projectile.IndexInRange(projIndex))
                    {
                        Main.projectile[projIndex].originalDamage = baseDamage;
                    }
                }
            }
            ModContent.GetInstance<NoviceClericEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class SacredEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SacredEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SacredHelmet>().UpdateArmorSet(player);
            }
        }
        public class KarmicEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SacredEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class PaganEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SacredEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SacredBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<SacredHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SacredLeggings>());
            recipe.AddIngredient(ModContent.ItemType<NoviceClericEnchant>());
            recipe.AddIngredient(ModContent.ItemType<KarmicHolder>());
            recipe.AddIngredient(ModContent.ItemType<Liberation>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
