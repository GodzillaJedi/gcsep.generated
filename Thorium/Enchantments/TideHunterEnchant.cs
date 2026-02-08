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
using ThoriumMod.Buffs.Pet;
using ThoriumMod.Items.Depths;
using ThoriumMod.Projectiles.LightPets;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TideHunterEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TideHunterEffect>(Item);
            if (player.AddEffect<AnglerBowlEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
                if (player.FindBuffIndex(ModContent.BuffType<AnglerBowlBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<AnglerBowlBuff>(), 3600);
                }

                if (player.ownedProjectileCounts[ModContent.ProjectileType<AnglerBowlPro>()] < 1)
                {
                    int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(num);
                    int num2 = Projectile.NewProjectile(source_ItemUse, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<AnglerBowlPro>(), damage, 0f, player.whoAmI);
                    if (Main.projectile.IndexInRange(num2))
                    {
                        Main.projectile[num2].originalDamage = num;
                    }
                }
            }
        }

        public class TideHunterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideHunterEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TideHunterCap>().UpdateArmorSet(player);
            }
        }
        public class AnglerBowlEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideHunterEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TideHunterCap>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterChestpiece>());
            recipe.AddIngredient(ModContent.ItemType<TideHunterLeggings>());
            recipe.AddIngredient(ModContent.ItemType<AnglerBowl>());
            recipe.AddIngredient(ModContent.ItemType<HydroPickaxe>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
