using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Buffs;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Misc;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class NobleEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<NobleEffect>(Item);
            player.AddEffect<NobleHatEffect>(Item);
            if (player.AddEffect<BrassCapEffect>(Item))
            {
                ModContent.GetInstance<BrassCap>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<RingOfUnityEffect>(Item))
            {
                ModContent.GetInstance<RingofUnity>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<WaxyRosinEffect>(Item))
            {
                ModContent.GetInstance<WaxyRosin>().UpdateAccessory(player, hideVisual);
            }
        }
        public class NobleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NobleEnchant>();

            public override void PostUpdate(Player player)
            {
                if (Main.gameMenu) return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                CheckCoin(0, ItemID.CopperCoin, player, modPlayer);
                CheckCoin(1, ItemID.SilverCoin, player, modPlayer);
                CheckCoin(2, ItemID.GoldCoin, player, modPlayer);
                CheckCoin(3, ItemID.PlatinumCoin, player, modPlayer);
            }

            private void CheckCoin(int index, int coinType, Player player, CSEThoriumPlayer modPlayer)
            {
                bool hasCoin = player.CountItem(coinType) > 0;
                if (hasCoin && !modPlayer.nobleCoinSeen[index])
                {
                    player.AddBuff(ModContent.BuffType<TheRichBuff>(), 300);
                }
                modPlayer.nobleCoinSeen[index] = hasCoin;
            }
        }
        public class NobleHatEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NobleEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<NoblesHat>().UpdateArmorSet(player);
            }
        }
        public class BrassCapEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NobleEnchant>();
            public override bool MutantsPresenceAffects => true;

        }
        public class RingOfUnityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NobleEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class WaxyRosinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NobleEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<NoblesHat>());
            recipe.AddIngredient(ModContent.ItemType<NoblesJerkin>());
            recipe.AddIngredient(ModContent.ItemType<NoblesLeggings>());
            recipe.AddIngredient(ModContent.ItemType<RingofUnity>());
            recipe.AddIngredient(ModContent.ItemType<BrassCap>());
            recipe.AddIngredient(ModContent.ItemType<WaxyRosin>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
