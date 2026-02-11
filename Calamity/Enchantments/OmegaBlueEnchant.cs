using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Fearmonger;
using CalamityMod.Items.Armor.OmegaBlue;
using CalamityMod.Items.Armor.Prismatic;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class OmegaBlueEnchant : BaseEnchant
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
        public override Color nameColor => new(31, 79, 156);
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TruffleEffect>(Item);
            player.AddEffect<OmegaBlueEffect>(Item);
            player.AddEffect<ReaperEffect>(Item);
            player.AddEffect<OldScaleEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueHelmet>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueChestplate>());
            recipe.AddIngredient(ModContent.ItemType<OmegaBlueTentacles>());
            recipe.AddIngredient(ModContent.ItemType<OldDukeScales>());
            recipe.AddIngredient(ModContent.ItemType<ReaperToothNecklace>());
            recipe.AddIngredient(ModContent.ItemType<MutatedTruffle>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class TruffleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                int buffType = ModContent.BuffType<MutatedTruffleBuff>();

                // Apply buff without resetting timer every tick
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 2);

                // Only spawn projectile on local player
                if (player.whoAmI != Main.myPlayer)
                    return;

                int projType = ModContent.ProjectileType<MutatedTruffleMinion>();

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
        public class ReaperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage<GenericDamageClass>() += 0.15f;
                player.GetArmorPenetration<GenericDamageClass>() += 15f;
            }
        }
        public class OldScaleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<OldDukeScalesPlayer>().OldDukeScalesOn = true;
            }
        }
        public class OmegaBlueEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OmegaBlueEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OmegaBlueHelmet>().UpdateArmorSet(player);
            }
        }
    }
}
