using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.LunicCorps;
using CalamityMod.Items.Armor.Sulphurous;
using CalamityMod.Items.Armor.TitanHeart;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class TitanHeartEnchantEx : BaseEnchant
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
        public override Color nameColor => new Color(102, 96, 117);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TitanEffect>(Item);
            player.AddEffect<MoonCrownEffect>(Item);
            player.AddEffect<JewelEffect>(Item);
            player.AddEffect<TitanHeartEffect>(Item);
            ModContent.GetInstance<TitanHeartEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class TitanEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanHeartEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TitanHeartMask>().UpdateArmorSet(player);
            }
        }
        public class MoonCrownEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanHeartEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.rogueVelocity += 0.15f;
                calamityPlayer.moonCrown = true;
            }
        }
        public class JewelEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DevastationExHeader>();
            public override int ToggleItemType => ModContent.ItemType<TitanHeartEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().infectedJewel = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TitanHeartMask>());
            recipe.AddIngredient(ModContent.ItemType<TitanHeartMantle>());
            recipe.AddIngredient(ModContent.ItemType<TitanHeartBoots>());
            recipe.AddIngredient(ModContent.ItemType<TitanHeartEnchant>());
            recipe.AddIngredient(ModContent.ItemType<MoonstoneCrown>());
            recipe.AddIngredient(ModContent.ItemType<InfectedJewel>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}


