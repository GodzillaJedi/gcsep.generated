using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Wulfrum;
using CalamityMod.Items.Tools;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;


namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class WulfrumEnchantEx : BaseEnchant
    {
        public new string LocalizationCategory => "Items.Armor.PreHardmode";
        
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
                ModContent.GetInstance<WulfrumHat>().UpdateArmorSet(player);
            }

            public class WulfrumArmorPlayer : ModPlayer
            {
                public bool wulfrumSet;
                public bool wulfrumHatEquipped;

                public override void ResetEffects()
                {
                    wulfrumSet = false;
                    wulfrumHatEquipped = false;
                }

                public override void PostUpdateEquips()
                {
                    if (wulfrumHatEquipped)
                    {
                        // Optional: Additional visual or passive effects
                        Lighting.AddLight(Player.Center, 0.2f, 0.8f, 0.2f);
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