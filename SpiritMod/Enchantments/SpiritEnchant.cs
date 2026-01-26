using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.SeraphSet;
using SpiritMod.Items.Sets.SlagSet.FieryArmor;
using SpiritMod.Items.Sets.SpiritSet;
using SpiritMod.Items.Sets.SpiritSet.SpiritArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.SlagTyrantEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SpiritEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(0, 186, 242);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<SpiritFangEffect>(Item))
            {
                ModContent.GetInstance<ShadowSingeFang>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<SpiritHelmEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<SpiritHeadgear>();
            recipe.AddIngredient<SpiritBodyArmor>();
            recipe.AddIngredient<SpiritLeggings>();
            recipe.AddIngredient<SpiritSaber>();
            recipe.AddIngredient<GlowSting>();
            recipe.AddIngredient<ShadowSingeFang>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class SpiritFangEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritEnchant>();
        }
        public class SpiritHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SpiritHeadgear>().UpdateArmorSet(player);
            }
        }
    }
}
