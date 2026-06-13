using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Buffs;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.Content.Items.Armor.Ranged.MartianGunner;
using VitalityMod.Content.Items.Weapons.Summoner.Sentries;
using VitalityMod.Content.Projectiles.Summoner.Sentries;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class MartianGunnerEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => Color.AliceBlue;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MartianGunnerEffect>(Item);
            player.AddEffect<XenoniteCluster>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MartianGunnerHelmet>());
            recipe.AddIngredient(ModContent.ItemType<MartianGunnerChestplate>());
            recipe.AddIngredient(ModContent.ItemType<MartianGunnerLeggings>());
            recipe.AddIngredient(ModContent.ItemType<XenoniteClusterStaff>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class MartianGunnerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MartianGunnerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MartianGunnerHelmet>().UpdateArmorSet(player);
            }
        }
        public class XenoniteCluster : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EvilForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MartianGunnerEnchant>();
            public override bool MinionEffect => true;
            public override void PostUpdateEquips(Player player)
            {
                int buffType = ModContent.BuffType<XenoniteClusterBuff>();

                // Apply buff without resetting timer every tick
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 2);

                // Only spawn projectile on local player
                if (player.whoAmI != Main.myPlayer)
                    return;

                int projType = ModContent.ProjectileType<XenoniteClusterProj>();

                if (player.ownedProjectileCounts[projType] <= 0)
                {
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(140f);

                    var proj = Projectile.NewProjectileDirect(
                        player.GetSource_FromThis(),
                        player.Center,
                        -Vector2.UnitY,
                        projType,
                        damage,
                        0f,
                        player.whoAmI
                    );

                    proj.originalDamage = damage;
                }
            }
        }
    }
}
