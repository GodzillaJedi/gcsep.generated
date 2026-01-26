using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.BossLoot.AtlasDrops;
using SpiritMod.Items.BossLoot.AtlasDrops.PrimalstoneArmor;
using SpiritMod.Items.BossLoot.InfernonDrops.InfernonArmor;
using SpiritMod.Items.Sets.SwordsMisc.EternalSwordTree;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.PainMongerEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class PrimalstoneEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(164, 193, 176);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PrimalStoneArmorEffect>(Item);
            if (player.AddEffect<TitanboundBulwarkEffect>(Item))
            {
                ModContent.GetInstance<TitanboundBulwark>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<PrimalstoneFaceplate>(1);
            recipe.AddIngredient<PrimalstoneBreastplate>(1);
            recipe.AddIngredient<PrimalstoneLeggings>(1);
            recipe.AddIngredient<DemoncomboSword>(1);
            recipe.AddIngredient<Mountain>(1);
            recipe.AddIngredient<TitanboundBulwark>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PrimalStoneArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrimalstoneEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PrimalstoneFaceplate>().UpdateArmorSet(player);
            }
        }
        public class TitanboundBulwarkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrimalstoneEnchant>();
        }
    }
}
