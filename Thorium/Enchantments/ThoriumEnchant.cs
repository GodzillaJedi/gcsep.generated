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
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Projectiles.Minions;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumEnchant : BaseEnchant
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
            Item.rare = 1;
            Item.value = 40000;
        }
        public static readonly int SetDamage = 10;
        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ThoriumEffect>(Item);
            if (player.AddEffect<BandReplenishmentEffect>(Item))
            {
                ModContent.GetInstance<BandofReplenishment>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CrietzEffect>(Item))
            {
                ModContent.GetInstance<Crietz>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<OdinEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(Item);

                if (player.FindBuffIndex(ModContent.BuffType<EyeofOdinBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<EyeofOdinBuff>(), 3600);
                }

                // Check the same projectile type you spawn
                if (player.ownedProjectileCounts[ModContent.ProjectileType<EyeofOdinPro>()] < 1)
                {
                    int baseDamage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(baseDamage);

                    int projIndex = Projectile.NewProjectile(
                        source_ItemUse,
                        player.Center.X,
                        player.Center.Y,
                        0f,
                        -1f,
                        ModContent.ProjectileType<EyeofOdinPro>(),
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
        }
        public class ThoriumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ThoriumEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ThoriumHelmet>().UpdateArmorSet(player);
            }
        }
        public class BandReplenishmentEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ThoriumEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class CrietzEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ThoriumEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class OdinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ThoriumEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ThoriumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumMail>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Crietz>());
            recipe.AddIngredient(ModContent.ItemType<BandofReplenishment>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
