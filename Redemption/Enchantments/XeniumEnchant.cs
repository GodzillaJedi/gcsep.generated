using CalamityMod;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Redemption.Globals.Players;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.PostML.Xenium;
using Redemption.Items.Armor.PreHM.PureIron;
using Redemption.Items.Weapons.PostML.Summon;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.Calamity.Name)]
    public class XeniumEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(143, 227, 84);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<XeniumConcoctionEffect>(Item))
            {
                ModContent.GetInstance<BeelzebubConcoction>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<XeniumShieldEffect>(Item))
            {
                ModContent.GetInstance<InfectionShield>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<XeniumDroneEffect>(Item))
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    // Apply buff
                    int buff = ModContent.BuffType<XeniumTurretBuff>();
                    if (!player.HasBuff(buff))
                        player.AddBuff(buff, 3600);

                    // Spawn turret if missing
                    int projType = ModContent.ProjectileType<XeniumTurret>();
                    if (player.ownedProjectileCounts[projType] < 1)
                    {
                        IEntitySource source = player.GetSource_ItemUse(Item);

                        // Modern damage scaling
                        int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(140f);

                        var proj = Projectile.NewProjectileDirect(
                            source,
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
            player.AddEffect<XeniumVisorEffect>(Item);
        }
        public class XeniumVisorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XeniumEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<XeniumVisor>().UpdateArmorSet(player);
            }
        }
        public class XeniumDroneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XeniumEnchant>();
        }
        public class XeniumConcoctionEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XeniumEnchant>();
        }

        public class XeniumShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XeniumEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<XeniumVisor>();
            recipe.AddIngredient<XeniumBreastplate>();
            recipe.AddIngredient<XeniumLeggings>();
            recipe.AddIngredient<XeniumDrone>();
            recipe.AddIngredient<InfectionShield>();
            recipe.AddIngredient<BeelzebubConcoction>();
            recipe.AddTile(412);

            recipe.Register();
        }
    }
}
