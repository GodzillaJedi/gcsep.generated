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
using ThoriumMod.Buffs;
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Valadium;
using ThoriumMod.Projectiles.Minions;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ValadiumEnchant : BaseEnchant
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
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<ValadiumEffect>(Item))
            {
                player.gravControl = true;
                if (player.gravDir == -1f)
                {
                    player.AddBuff(ModContent.BuffType<ValadiumSetBuff>(), 60);
                }
            }
            if (player.AddEffect<BeholderGazeEffect>(Item))
            {
                ModContent.GetInstance<BeholderGaze>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<BeholderEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
                if (player.FindBuffIndex(ModContent.BuffType<BeholderStaffBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<BeholderStaffBuff>(), 3600);
                }

                if (player.ownedProjectileCounts[ModContent.ProjectileType<BeholderStaffPro>()] < 1)
                {
                    int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(num);
                    int num2 = Projectile.NewProjectile(source_ItemUse, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<BeholderStaffPro>(), damage, 0f, player.whoAmI);
                    if (Main.projectile.IndexInRange(num2))
                    {
                        Main.projectile[num2].originalDamage = num;
                    }
                }
            }
        }
        public class ValadiumEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ValadiumEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class BeholderGazeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ValadiumEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class BeholderEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ValadiumEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ValadiumHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ValadiumBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<ValadiumGreaves>());
            recipe.AddIngredient(ModContent.ItemType<BeholderGaze>());
            recipe.AddIngredient(ModContent.ItemType<BeholderStaff>());
            recipe.AddIngredient(ModContent.ItemType<TommyGun>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
