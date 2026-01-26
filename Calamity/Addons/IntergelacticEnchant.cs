using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CatalystMod;
using CatalystMod.Buffs.DamageOverTime;
using CatalystMod.Items;
using CatalystMod.Items.Accessories;
using CatalystMod.Items.Armor.Intergelactic;
using CatalystMod.Items.Tools.GrapplingHooks;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Catalyst.Name)]
    [JITWhenModsEnabled(ModCompatibility.Catalyst.Name)]
    public class IntergelacticEnchant : BaseEnchant
    {
        public int CurrentRockDamage;

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = CatalystItem.RaritySuperboss;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<IntergelacticArmorsEffect>(Item);
            player.AddEffect<IntergelacticSummonEffect>(Item);
            player.AddEffect<InfluxEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("gcsep:IntergelacticHelmet");
            recipe.AddIngredient(ModContent.ItemType<IntergelacticBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<IntergelacticGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CatalystMod.Items.Weapons.Typeless.ScytheoftheAbandonedGod>());
            recipe.AddIngredient(ModContent.ItemType<AstralLash>());
            recipe.AddIngredient(ModContent.ItemType<InfluxCluster>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class IntergelacticArmorsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IntergelacticEnchant>();
            public static void SetBonus(Player player, Item item)
            {
                player.buffImmune[ModContent.BuffType<AstralBlight>()] = true;
                CatalystPlayer modPlayer = player.GetModPlayer<CatalystPlayer>();
                modPlayer.resistMetanovaGravity = true;
                modPlayer.intergelacticAstralBlight = true;
                modPlayer.intergelacticSet = item;
            }
            public override void PostUpdateEquips(Player player)
            {
                string toggleKeyText;
                try
                {
                    var keys = CatalystPlayer.AsteroidVisToggleKey.GetAssignedKeys();
                    toggleKeyText = keys.Count > 0
                        ? Language.GetTextValue("Mods.CatalystMod.Common.BoundKey", keys[0])
                        : Language.GetTextValue("Mods.CatalystMod.Common.UnboundKey");
                }
                catch
                {
                    toggleKeyText = Language.GetTextValue("Mods.CatalystMod.Common.UnboundKey");
                }

                string setBonus = Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticAll", toggleKeyText);

                // MELEE
                if (player.GetDamage(DamageClass.Melee).Base > 0f)
                {
                    setBonus = Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticMelee", toggleKeyText);
                    player.noKnockback = true;
                    player.buffImmune[32] = true;
                    player.buffImmune[33] = true;
                    player.buffImmune[46] = true;
                    player.buffImmune[47] = true;
                    player.buffImmune[156] = true;
                    player.buffImmune[31] = true;
                    player.buffImmune[197] = true;
                    player.buffImmune[ModContent.BuffType<GlacialState>()] = true;
                    SetBonus(player, EffectItem(player));
                }

                // MAGE
                if (player.GetDamage(DamageClass.Magic).Base > 0f)
                {
                    setBonus += "\n" + Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticMagic");
                    player.manaCost -= 0.2f;
                    player.GetModPlayer<CatalystPlayer>().manaPotionBonus += 100;
                    IntergelacticHeadMelee.SetBonus(player, EffectItem(player));
                }

                // RANGED
                if (player.GetDamage(DamageClass.Ranged).Base > 0f)
                {
                    setBonus += "\n" + Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticRanged");
                    IntergelacticHeadMelee.SetBonus(player, EffectItem(player));
                }

                // ROGUE
                if (player.GetDamage(ModContent.GetInstance<RogueDamageClass>()).Base > 0f)
                {
                    setBonus += "\n" + Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticRogue");
                    CalamityPlayer calamityPlayer = player.Calamity();
                    calamityPlayer.rogueStealthMax += 1.2f;
                    calamityPlayer.wearingRogueArmor = true;
                    IntergelacticHeadMelee.SetBonus(player, EffectItem(player));
                }

                
            }
        }
        public class IntergelacticSummonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IntergelacticEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                try
                {
                    using (List<string>.Enumerator enumerator = CatalystPlayer.AsteroidVisToggleKey.GetAssignedKeys().GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            string current = enumerator.Current;
                            player.setBonus = Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticAll", Language.GetTextValue("Mods.CatalystMod.Common.BoundKey", current)) + "\n" + Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticSummon");
                        }
                    }

                    player.setBonus = Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticAll", Language.GetTextValue("Mods.CatalystMod.Common.UnboundKey")) + "\n" + Language.GetTextValue("Mods.CatalystMod.ArmorSetBonus.IntergelacticSummon");
                }
                catch (Exception)
                {
                }

                CatalystPlayer modPlayer = player.GetModPlayer<CatalystPlayer>();
                modPlayer.intergelacticAstralBlight = true;
                modPlayer.intergelacticSet = base.EffectItem(player);
                modPlayer.minionCrit += 6;
                player.maxMinions += 3;
                player.Calamity().WearingPostMLSummonerSet = true;
                IntergelacticHeadMelee.SetBonus(player, EffectItem(player));
            }
        }

        public class InfluxEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IntergelacticEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetModPlayer<CatalystPlayer>().influxCore = base.EffectItem(player);
            }
        }
    }
}
