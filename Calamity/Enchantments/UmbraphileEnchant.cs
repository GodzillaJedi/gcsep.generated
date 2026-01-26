using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Umbraphile;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class UmbraphileEnchant : BaseEnchant
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
        public override Color nameColor => new(163, 0, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<VampiricEffect>(Item);
            player.AddEffect<DecietEffect>(Item);
            player.AddEffect<UmbraphileEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UmbraphileHood>());
            recipe.AddIngredient(ModContent.ItemType<UmbraphileRegalia>());
            recipe.AddIngredient(ModContent.ItemType<UmbraphileBoots>());
            recipe.AddIngredient(ModContent.ItemType<VampiricTalisman>());
            recipe.AddIngredient(ModContent.ItemType<CoinofDeceit>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class UmbraphileEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<UmbraphileEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.umbraphileSet = true;
                calamityPlayer.rogueStealthMax += 1.1f;
                player.setBonus = ModContent.GetInstance<UmbraphileHood>().GetLocalization("SetBonus").Format();
                player.Calamity().wearingRogueArmor = true;
            }
        }
        public class VampiricEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<UmbraphileEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().vampiricTalisman = true;
                player.GetDamage<ThrowingDamageClass>() += 0.12f;
            }
        }
        public class DecietEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<UmbraphileEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().stealthStrike90Cost = true;
                player.GetCritChance<ThrowingDamageClass>() += 6f;
            }
        }
    }
}
