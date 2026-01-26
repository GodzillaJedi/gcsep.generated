using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Accessory.ShurikenLauncher;
using SpiritMod.Items.Armor.LeatherArmor;
using SpiritMod.Items.BossLoot.InfernonDrops;
using SpiritMod.Items.BossLoot.InfernonDrops.InfernonArmor;
using SpiritMod.Items.Sets.MagicMisc.TerraStaffTree;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.GraniteChunkEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class PainMongerEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(234, 93, 15);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 4;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PainMongerGuard>(Item);
            if (player.AddEffect<PainMongerMaw>(Item))
            {
                ModContent.GetInstance<HellEater>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<InfernalVisor>(1);
            recipe.AddIngredient<InfernalBreastplate>(1);
            recipe.AddIngredient<InfernalGreaves>(1);
            recipe.AddIngredient<InfernalStaff>(1);
            recipe.AddIngredient<HellStaff>(1);
            recipe.AddIngredient<HellEater>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PainMongerGuard : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PainMongerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<InfernalVisor>().UpdateArmorSet(player);
            }
        }
        public class PainMongerMaw : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PainMongerEnchant>();
        }
    }
}