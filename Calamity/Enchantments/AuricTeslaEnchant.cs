using CalamityBardHealer;
using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Pets;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Projectiles.Pets;
using CalamityMod.Projectiles.Typeless;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AuricTeslaEnchant : BaseEnchant
    {
        private Mod calamity;
        
        public override void Load()
        {
            ModLoader.TryGetMod("CalamityMod", out calamity);
        }
        public override Color nameColor => new(255, 213, 0);
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DragonScalesEffect>(Item);
            player.AddEffect<SpiritOriginEffect>(Item);
            player.AddEffect<PermafrostsEffect>(Item);
            player.AddEffect<TransformerEffect>(Item);
            player.AddEffect<YharimJamEffect>(Item);
            player.AddEffect<AuricTeslaArmorsEffect>(Item);
            player.AddEffect<AuricTeslaSummonEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<AuricTeslaCuisses>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<AuricTeslaHealerHead>());
                recipe.AddIngredient(ModContent.ItemType<AuricTeslaFrilledHelmet>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<AugmentedAuricTeslaFeatheredHeadwear>());
                recipe.AddIngredient(ModContent.ItemType<AugmentedAuricTeslaValkyrieVisage>());
                recipe.AddIngredient(ModContent.ItemType<CalamityBardHealer.Items.YharimsJam>());
            }
            recipe.AddIngredient(ModContent.ItemType<DaawnlightSpiritOrigin>());
            recipe.AddIngredient(ModContent.ItemType<TheTransformer>());
            recipe.AddIngredient(ModContent.ItemType<DragonScales>());
            recipe.AddIngredient(ModContent.ItemType<PermafrostsConcoction>());


            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }

        public class AuricTeslaArmorsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AuricTeslaHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<AuricTeslaHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<AuricTeslaHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<AuricTeslaHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<AugmentedAuricTeslaFeatheredHeadwear>().UpdateArmorSet(player);
                ModContent.GetInstance<AugmentedAuricTeslaValkyrieVisage>().UpdateArmorSet(player);
                ModContent.GetInstance<AuricTeslaHealerHead>().UpdateArmorSet(player);
                ModContent.GetInstance<AuricTeslaFrilledHelmet>().UpdateArmorSet(player);
            }
        }
        public class AuricTeslaSummonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AuricTeslaHeadSummon>().UpdateArmorSet(player);
            }
        }
        public class DragonScalesEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().dragonScales = true;
                player.buffImmune[ModContent.BuffType<Dragonfire>()] = true;
            }
        }
        public class SpiritOriginEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                var calamityPlayer = player.Calamity();
                calamityPlayer.spiritOrigin = true;

                int buffType = ModContent.BuffType<ArcherofLunamoon>();
                int minionType = ModContent.ProjectileType<DaawnlightSpiritOriginMinion>();

                if (player.whoAmI != Main.myPlayer)
                    return;

                // Apply buff if missing
                if (!player.HasBuff(buffType))
                {
                    player.AddBuff(buffType, 18000);
                }

                // Refresh buff and spawn minion
                int buffIndex = player.FindBuffIndex(buffType);
                if (buffIndex != -1)
                {
                    player.buffTime[buffIndex] = 2;
                    calamityPlayer.spiritOriginPet = true;

                    if (player.ownedProjectileCounts[minionType] <= 0)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_Buff(buffIndex),
                            player.Center,
                            -Vector2.UnitY * 3f,
                            minionType,
                            0, 0f, player.whoAmI);
                    }
                }
            }
        }
        public class PermafrostsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().permafrostsConcoction = true;
            }
        }
        public class TransformerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                if (!player.active || player.dead)
                    return;

                CalamityPlayer calamityPlayer = player.Calamity();

                // Enable effect
                calamityPlayer.transformer = true;

                // Respect visual toggle
                calamityPlayer.transformerVisual = true;

                // Spawn aura if needed
                bool noAura = player.ownedProjectileCounts[ModContent.ProjectileType<TransformerAura>()] < 1;
                bool visualsOn = true;
                bool offCooldown = calamityPlayer.transformerCooldown == 0;

                if (noAura && visualsOn && offCooldown)
                {
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<TransformerAura>(),
                        0,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }
        public class YharimJamEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AuricTeslaEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<ThorlamityPlayer>().yharimsJam = true;
                int num = ModContent.ProjectileType<global::CalamityBardHealer.Projectiles.YharimsJam>();
                int num2 = player.ownedProjectileCounts[num];
                if (num2 <= 0 && Main.myPlayer == player.whoAmI)
                {
                    num2 = Projectile.NewProjectile(player.GetSource_Misc("YharimsJam"), player.MountedCenter, player.velocity, num, (int)player.GetDamage(ThoriumDamageBase<BardDamage>.Instance).ApplyTo(930f), 0f, player.whoAmI, num2);
                    NetMessage.SendData(MessageID.SyncProjectile, -1, -1, null, num2);
                }
            }
        }
    }
}
