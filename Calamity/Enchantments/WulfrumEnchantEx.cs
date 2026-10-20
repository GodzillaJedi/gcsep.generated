using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.Enums;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Armor.Wulfrum;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Tools;
using CalamityMod.Particles;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class WulfrumEnchantEx : BaseEnchant
    {
        internal static Item DummyCannon = new Item();
        public new string LocalizationCategory => "Items.Armor.PreHardmode";
        public static bool PowerModeEngaged(Player player, out CooldownInstance cd)
        {
            cd = null;
            if (player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out cd))
            {
                return cd.timeLeft > WulfrumHat.BastionCooldown;
            }

            return false;
        }
        public override void Unload()
        {
            if (ModLoader.HasMod("CalamityMod"))
            {
                On_Main.DrawPendingMouseText -= SpoofMouseItem;
            }

            DummyCannon?.TurnToAir();
            DummyCannon = null;
        }
        private delegate void orig_HoldItem(WulfrumFusionCannon self, Player player);
        private static void BypassCalamitySelfDestruct(orig_HoldItem orig, WulfrumFusionCannon self, Player player)
        {
            if (player.HasEffect<WulfrumArmorEffect>())
            {
                player.Calamity().mouseWorldListener = true;
                self.Item.noUseGraphic = false;

                if (!player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var value) ||
                    value.timeLeft > WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime)
                {
                    self.Item.noUseGraphic = true;
                }
                return; 
            }

            orig(self, player); 
        }
        public static void ActivationEffects(Player player)
        {
            Terraria.Audio.SoundEngine.PlaySound(in WulfrumHat.SetActivationSound);
            bool flag = player.Transformation().Type == ModContent.ItemType<AbandonedWulfrumHelmet>();
            if (!flag)
            {
                player.controlUseItem = false;
                player.controlUseTile = false;
                player.controlThrow = false;
                for (int i = 0; i < 5; i++)
                {
                    GeneralParticleHandler.SpawnParticle(new WulfrumBastionPartsParticle(player, i, WulfrumHat.BastionBuildTime + 2));
                }
            }
        }
        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
            {
                var holdItemMethod = typeof(WulfrumFusionCannon).GetMethod("HoldItem", BindingFlags.Public | BindingFlags.Instance);
                if (holdItemMethod != null)
                {
                    MonoModHooks.Add(holdItemMethod, BypassCalamitySelfDestruct);
                }
            }
        }
        private void SpoofMouseItem(On_Main.orig_DrawPendingMouseText orig)
        {
            if (!Main.gameMenu && DummyCannon != null)
            {
                if (DummyCannon.IsAir)
                {
                    DummyCannon.SetDefaults(ModContent.ItemType<WulfrumFusionCannon>());
                }

                if (Main.keyState.PressingShift())
                {
                    Main.HoverItem = DummyCannon.Clone();
                    Main.hoverItemName = DummyCannon.Name;
                }
            }
            orig();
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new Color(206, 201, 170);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<WulfrumArmorEffect>(Item);
            player.AddEffect<ChiEffect>(Item);
            player.AddEffect<AcrobaticsPackEffect>(Item);
            player.AddEffect<WulfrumEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WulfrumHat>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumJacket>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumOveralls>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumEnchant>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumTreasurePinger>());
            recipe.AddIngredient(ModContent.ItemType<WulfrumAcrobaticsPack>());
            recipe.AddIngredient(ModContent.ItemType<TrinketofChi>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class WulfrumArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<WulfrumEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                var wulfrumPlayer = player.GetModPlayer<WulfrumArmorPlayer>();
                wulfrumPlayer.wulfrumSet = true;

                Color color = Color.Lerp(new Color(194, 255, 67), new Color(112, 244, 244), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
                player.setBonus = EffectItem(player).ModItem.GetLocalization("SetBonus").Format(color.Hex3(), CalamityUtils.GetArmorSetBonusKey(), WulfrumHat.BastionTime.FramesToSeconds(), WulfrumHat.TimeLostPerHit.FramesToSeconds());

                if (CalamityKeybinds.ArmorSetBonusHotKey.JustPressed && Main.myPlayer == player.whoAmI)
                {
                    if (!player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var value))
                    {
                        player.ConsumeItem(ModContent.ItemType<WulfrumMetalScrap>());
                        player.AddCooldown(WulfrumBastion.ID, WulfrumHat.BastionCooldown + WulfrumHat.BastionTime);
                    }
                }

                if (PowerModeEngaged(player, out var cd))
                {
                    if (cd.timeLeft == WulfrumHat.BastionCooldown + WulfrumHat.BastionTime)
                    {
                        ActivationEffects(player);
                    }

                    player.statDefense += WulfrumHat.BastionDefenseBoost;
                    player.endurance += WulfrumHat.BastionDRBoost;

                    if (cd.timeLeft < WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime)
                    {
                        player.Transformation().Type = ModContent.ItemType<AbandonedWulfrumHelmet>();
                    }

                    if (Main.myPlayer == player.whoAmI)
                    {
                        if (DummyCannon == null || DummyCannon.IsAir)
                        {
                            DummyCannon = new Item();
                            DummyCannon.SetDefaults(ModContent.ItemType<WulfrumFusionCannon>());
                        }

                        if (player.controlUseItem || player.itemAnimation > 0)
                        {
                            player.selectedItem = 58;
                            player.inventory[58] = DummyCannon;
                        }
                    }
                }
                else if (Main.myPlayer == player.whoAmI)
                {
                    if (player.Transformation().Type == ModContent.ItemType<AbandonedWulfrumHelmet>())
                    {
                        player.Transformation().Type = -1;
                        player.head = -1;
                    }
                }
            }
        }
        public class ChiEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<WulfrumEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().trinketOfChi = true;
                if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0 && Main.LocalPlayer.team == player.team && player.team != 0)
                {
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<ChiRegenBuff>(), 20);
                }
            }
        }
        public class AcrobaticsPackEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<WulfrumEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                player.moveSpeed += 0.08f;

                var wp = player.GetModPlayer<WulfrumPackPlayer>();
                wp.WulfrumPackEquipped = true;
                wp.PackItem = EffectItem(player);

                Lighting.AddLight(
                    player.Center,
                    Color.Lerp(Color.DeepSkyBlue, Color.GreenYellow, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.5f + 0.5f).ToVector3()
                );
            }
        }
    }
}