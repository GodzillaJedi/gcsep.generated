using ClickerClass.Prefixes.ClickerPrefixes;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Buffs;
using ThoriumMod.Dusts;
using ThoriumMod.Items.BossLich;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Projectiles.Thrower;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.PlagueDoctorEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LichEnchant : BaseEnchant
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
            Item.rare = 6;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<LichEffect>(Item);
            if (player.AddEffect<PhylacteryEffect>(Item))
            {
                ModContent.GetInstance<Phylactery>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<PlagueDoctorEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class LichEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LichEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LichCowl>().UpdateArmorSet(player);
            }
        }
        public class PhylacteryEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LichEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LichCowl>());
            recipe.AddIngredient(ModContent.ItemType<LichCarapace>());
            recipe.AddIngredient(ModContent.ItemType<LichTalon>());
            recipe.AddIngredient(ModContent.ItemType<PlagueDoctorEnchant>());
            recipe.AddIngredient(ModContent.ItemType<Phylactery>());
            recipe.AddIngredient(ModContent.ItemType<SoulCleaver>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
