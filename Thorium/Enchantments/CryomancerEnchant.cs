using CalamityMod;
using CalamityMod.Items.Armor.Aerospec;
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
using ThoriumMod.Buffs;
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.Blizzard;
using ThoriumMod.Items.BossBoreanStrider;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Projectiles;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.IcyEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CryomancerEnchant : BaseEnchant
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
            Item.rare = ItemRarityID.Lime;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CryomancerEffect>(Item);
            if (player.AddEffect<IcyFairyEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
                if (player.FindBuffIndex(ModContent.BuffType<IceFairyStaffBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<IceFairyStaffBuff>(), 3600);
                }

                if (player.ownedProjectileCounts[ModContent.ProjectileType<IceFairyStaffPro>()] < 1)
                {
                    int num = player.ApplyArmorAccDamageBonusesTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(num);
                    int num2 = Projectile.NewProjectile(source_ItemUse, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<IceFairyStaffPro>(), damage, 0f, player.whoAmI);
                    if (Main.projectile.IndexInRange(num2))
                    {
                        Main.projectile[num2].originalDamage = num;
                    }
                }
            }
            if (player.AddEffect<StriderHideEffect>(Item))
            {
                ModContent.GetInstance<IceBoundStriderHide>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<IcyEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class CryomancerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryomancerEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CryomancersCrown>().UpdateArmorSet(player);
            }
        }
        public class StriderHideEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryomancerEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class IcyFairyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CryomancerEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CryomancersCrown>());
            recipe.AddIngredient(ModContent.ItemType<CryomancersTabard>());
            recipe.AddIngredient(ModContent.ItemType<CryomancersLeggings>());
            recipe.AddIngredient(ModContent.ItemType<IcyEnchant>());
            recipe.AddIngredient(ModContent.ItemType<IceBoundStriderHide>());
            recipe.AddIngredient(ModContent.ItemType<IceFairyStaff>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
