using CalamityMod.Items.Armor.Auric;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Utils;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Steel;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.DarksteelEnchant;
using static gcsep.Thorium.Enchantments.SteelEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DurasteelEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void UpdateInventory(Player player)
        {
            player.AddEffect<DurasteelEffectOres>(Item);
        }

        public override void UpdateVanity(Player player)
        {
            player.AddEffect<DurasteelEffectOres>(Item);
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
            player.AddEffect<DurasteelEffectOres>(Item);
            player.AddEffect<DurasteelEffect>(Item);
            if (player.AddEffect<IncandescentEffect>(Item))
            {
                ThoriumPlayer thoriumPlayer = player.GetThoriumPlayer();
                thoriumPlayer.needsAfterimage = true;
                thoriumPlayer.accIncandescentAlacrity = true;
                player.noFallDmg = true;
                player.runAcceleration += 0.25f;
                player.jumpSpeedBoost = 2.5f;
                if (player.controlDown && !player.controlUp)
                {
                    player.maxFallSpeed *= (player.wet ? 2.25f : 2.5f);
                    bool flag = player.IsOnStandableGround();
                    if (thoriumPlayer.falling >= 0 && player.velocity.Y < player.maxFallSpeed && !flag)
                    {
                        player.velocity.Y += 0.2f;
                    }
                }
            }
            ModContent.GetInstance<DarksteelEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class DurasteelEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DurasteelEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DurasteelHelmet>().UpdateArmorSet(player);
            }
        }
        public class DurasteelEffectOres : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DurasteelEnchant>();
            public override bool MutantsPresenceAffects => true;

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                TryDoubleOreDrops(target);
            }

            private void TryDoubleOreDrops(NPC target)
            {
                if (Main.rand.NextBool(2) && target.life <= 0)
                {
                    // Placeholder: implement ore duplication logic here
                    // Example: clone ore drops or spawn additional items
                }
            }
        }
        public class IncandescentEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DurasteelEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DurasteelHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<DarksteelEnchant>());
            recipe.AddIngredient(ModContent.ItemType<IncandescentAlacrity>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelBlade>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
