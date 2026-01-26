using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Steel;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.DurasteelEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SteelEnchant : BaseEnchant
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

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SteelEffect>(Item);
            player.AddEffect<SteelArmorEffect>(Item);
            if (player.AddEffect<SpikedBracerEffect>(Item))
            {
                ModContent.GetInstance<SpikedBracer>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ThoriumShieldEffect>(Item))
            {
                ModContent.GetInstance<ThoriumShield>().UpdateAccessory(player, hideVisual);
            }
        }
        public class SteelEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SteelEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdate(Player player)
            {
                if (Main.gameMenu) return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();

                if (modPlayer.steelDashCooldown > 0)
                    modPlayer.steelDashCooldown--;

                if (modPlayer.steelWasDashing && player.dashTime <= 0)
                    OnDashEnd(player, modPlayer);

                modPlayer.steelWasDashing = player.dashTime > 0;

                if (modPlayer.steelBuffDuration > 0)
                {
                    modPlayer.steelBuffDuration--;
                    player.statDefense += 10;
                }
            }
            private void OnDashEnd(Player player, CSEThoriumPlayer modPlayer)
            {
                if (modPlayer.steelDashCooldown > 0) return;

                modPlayer.steelDashCooldown = 15 * 60;
                modPlayer.steelBuffDuration = 5 * 60;

                if (player.whoAmI == Main.myPlayer)
                {
                    NPC target = FindNearestEnemy(player);
                    if (target != null)
                    {
                        Vector2 velocity = (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 12f;
                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            player.Center,
                            velocity,
                            ModContent.ProjectileType<SteelSwordProjectile>(),
                            150,
                            5f,
                            player.whoAmI);
                    }
                }

                if (player.HasEffect<DurasteelEffect>() && player.GetThoriumPlayer().MetalShieldMax < 101)
                {
                    player.GetThoriumPlayer().MetalShieldMax += 25;
                }
            }
            private NPC FindNearestEnemy(Player player)
            {
                NPC closest = null;
                float minDistance = float.MaxValue;

                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && npc.CanBeChasedBy())
                    {
                        float distance = Vector2.DistanceSquared(player.Center, npc.Center);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closest = npc;
                        }
                    }
                }

                return closest;
            }
        }
        public class SteelArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SteelEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SteelHelmet>().UpdateArmorSet(player);
            }
        }
        public class SpikedBracerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SteelEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ThoriumShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SteelEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SteelHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SteelChestplate>());
            recipe.AddIngredient(ModContent.ItemType<SteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumShield>());
            recipe.AddIngredient(ModContent.ItemType<SpikedBracer>());
            recipe.AddIngredient(ModContent.ItemType<SteelBlade>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
