using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.LunicCorps;
using CalamityMod.Projectiles.Summon;
using CalamityMod.World;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class LunicCorpEnchant : BaseEnchant
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
        public override Color nameColor => new(206, 201, 170);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HighRuleEffect>(Item);
            player.AddEffect<LunicArmorEffect>(Item);
            if (ModCompatibility.Redemption.Loaded)
            {
                player.AddEffect<NaviEffect>(Item);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LunicCorpsHelmet>());
            recipe.AddIngredient(ModContent.ItemType<LunicCorpsVest>());
            recipe.AddIngredient(ModContent.ItemType<LunicCorpsBoots>());
            recipe.AddIngredient(ModContent.ItemType<ShieldoftheHighRuler>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class HighRuleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LunicCorpEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                // Calamity integration
                CalamityPlayer cal = player.Calamity();

                // Force vanilla dash (Tabi-style)
                player.dashType = 2;
                cal.copyrightInfringementShield = true; // Disable Calamity dash so ours works

                // Defensive utility
                player.noKnockback = true;
                player.fireWalk = true;

                // Debuff immunities
                int[] immuneBuffs = new int[]
                {
                    24,  // On Fire!
                    46,  // Bleeding
                    44,  // Broken Armor
                    33,  // Poisoned
                    36,  // Slow
                    30,  // Darkness
                    20,  // Poison
                    32,  // Confused
                    31,  // Cursed
                    35,  // Silenced
                    23,  // Burning
                    22,  // Blackout
                    194, // Frostburn 2
                    156  // Weak
                };

                foreach (int buff in immuneBuffs)
                    player.buffImmune[buff] = true;

                // Small stat bonus
                player.statLifeMax2 += 10;
            }
        }
        public class LunicArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LunicCorpEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LunicCorpsHelmet>().UpdateArmorSet(player);
            }
        }
        public class NaviEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LunicCorpEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_ItemUse(EffectItem(player));
                    if (player.FindBuffIndex(ModContent.BuffType<NaturePixieBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<NaturePixieBuff>(), 3600);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<NaturePixie>()] < 1)
                    {
                        int num = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(140f);
                        Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<NaturePixie>(), num, 0f, player.whoAmI).originalDamage = num;
                    }
                }
            }
        }
    }
}