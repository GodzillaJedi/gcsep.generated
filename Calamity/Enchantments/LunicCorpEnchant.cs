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
                CalamityPlayer calamityPlayer = player.Calamity();
                player.dashType = 2;
                calamityPlayer.DashID = string.Empty;
                calamityPlayer.copyrightInfringementShield = true;
                player.noKnockback = true;
                player.fireWalk = true;
                player.buffImmune[24] = true;
                player.buffImmune[46] = true;
                player.buffImmune[44] = true;
                player.buffImmune[33] = true;
                player.buffImmune[36] = true;
                player.buffImmune[30] = true;
                player.buffImmune[20] = true;
                player.buffImmune[32] = true;
                player.buffImmune[31] = true;
                player.buffImmune[35] = true;
                player.buffImmune[23] = true;
                player.buffImmune[22] = true;
                player.buffImmune[194] = true;
                player.buffImmune[156] = true;
                player.statLifeMax2 += 10;
            }
        }
        public class LunicArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LunicCorpEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().lunicCorpsSet = true;
                string text = (CalamityWorld.revenge ? ("\n" + ModContent.GetInstance<LunicCorpsHelmet>().GetLocalization("ShieldAdren")) : "");
                player.setBonus = ModContent.GetInstance<LunicCorpsHelmet>().GetLocalization("SetBonus").Format(text);
                player.bulletDamage += 0.1f;
                player.specialistDamage += 0.1f;
                player.jumpSpeedBoost += 1f;
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
                        int num = player.ApplyArmorAccDamageBonusesTo(140f);
                        Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<NaturePixie>(), num, 0f, player.whoAmI).originalDamage = num;
                    }
                }
            }
        }
    }
}