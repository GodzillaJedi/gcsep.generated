using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.BossLoot.AvianDrops;
using SpiritMod.Items.BossLoot.AvianDrops.ApostleArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class ApostleEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(174, 152, 132);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ApostleEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<TalonHeaddress>();
            recipe.AddIngredient<TalonGarb>();
            recipe.AddIngredient<TalonPiercer>();
            recipe.AddIngredient<TalonBlade>();
            recipe.AddIngredient<Talonginus>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class ApostleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ApostleEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TalonHeaddress>().UpdateArmorSet(player);
            }
        }
    }
}
