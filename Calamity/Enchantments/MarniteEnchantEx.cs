using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.MarniteArchitect;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class MarniteEnchantEx : BaseEnchant
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
        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
            {
                var activateLiftMethod = typeof(MarniteArchitectHeadgear).GetMethod("ActivateLift", BindingFlags.NonPublic | BindingFlags.Instance);
                if (activateLiftMethod != null)
                {
                    MonoModHooks.Add(activateLiftMethod, BypassMarniteMountArmorCheck);
                }
            }
        }
        private delegate void orig_ActivateLift(MarniteArchitectHeadgear self, On_Player.orig_QuickMount orig, Player player);
        private static void BypassMarniteMountArmorCheck(orig_ActivateLift orig, MarniteArchitectHeadgear self, On_Player.orig_QuickMount origHook, Player player)
        {
            if (player.HasEffect<LiftEffect>())
            {
                if (!player.mount.Active && player.miscEquips[3].IsAir)
                {
                    if (!player.frozen && !player.tongued && !player.webbed && !player.stoned && player.gravDir != -1f && !player.dead && !player.noItems)
                    {
                        int liftMountID = ModContent.MountType<MarniteLift>();
                        if (player.mount.CanMount(liftMountID, player))
                        {
                            player.mount.SetMount(liftMountID, player);
                            Terraria.Audio.SoundEngine.PlaySound(in MarniteArchitectHeadgear.LiftSpawnSound, player.Center);
                            return; 
                        }
                    }
                }
                origHook(player); 
                return;
            }

            orig(self, origHook, player); 
        }
        public override Color nameColor => new(153, 200, 193);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<LiftEffect>(Item);
            player.AddEffect<HallowedEffect>(Item);
            player.AddEffect<RoverEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectToga>());
            recipe.AddIngredient(ModContent.ItemType<MarniteEnchant>());
            recipe.AddIngredient(ModContent.ItemType<HallowedRune>());
            recipe.AddIngredient(ModContent.ItemType<RoverDrive>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class LiftEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    Player.tileRangeX += MarniteArchitectHeadgear.TileRangeBoost;
                    Player.tileRangeY += MarniteArchitectHeadgear.TileRangeBoost;
                }
                Color color = Color.Lerp(new Color(255, 243, 161), new Color(137, 162, 255), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
                player.setBonus = ModContent.GetInstance<MarniteArchitectHeadgear>().GetLocalization("SetBonus").Format(color.Hex3(), (MarniteArchitectHeadgear.MaxLiftHeight + MarniteArchitectHeadgear.LiftHeightOffset).ToTiles());

                player.GetModPlayer<MarniteArchitectPlayer>().setEquipped = true;
            }
        }
        public class HallowedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().hallowedRune = true;
            }
        }
        public class RoverEffect : AccessoryEffect
        {
            private const int ShieldDefenseBoost = 20;

            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.roverDrive = true;
                calamityPlayer.roverDriveShieldVisible = true;

                if (calamityPlayer.RoverDriveShieldDurability > 0)
                {
                    player.statDefense += ShieldDefenseBoost;
                }
            }
        }
    }
}
