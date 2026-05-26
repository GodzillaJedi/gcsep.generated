using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.PlagueReaper;
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
    public class PlagueReaperEnchant : BaseEnchant
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
        public override Color nameColor => new(0, 71, 12);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AlchemicalEffect>(Item);
            player.AddEffect<FuelPackEffect>(Item);
            player.AddEffect<PlagueReaperEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlagueReaperMask>());
            recipe.AddIngredient(ModContent.ItemType<PlagueReaperVest>());
            recipe.AddIngredient(ModContent.ItemType<PlagueReaperStriders>());
            recipe.AddIngredient(ModContent.ItemType<PlaguedFuelPack>());
            recipe.AddIngredient(ModContent.ItemType<AlchemicalDecanter>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PlagueReaperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlagueReaperEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PlagueReaperMask>().UpdateArmorSet(player);
            }
        }
        public class AlchemicalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlagueReaperEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AlchemicalDecanter>().UpdateAccessory(player, true);
            }
        }
        public class FuelPackEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlagueReaperEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PlaguedFuelPack>().UpdateAccessory(player, true);
            }
        }
    }
}
