using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Accessory.MageTree;
using SpiritMod.Items.Sets.RunicSet.RunicArmor;
using SpiritMod.Items.Sets.SeraphSet;
using SpiritMod.Items.Sets.SeraphSet.SeraphArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.RunicEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SeraphEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(165, 189, 221);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SeraphimHelmEffect>(Item);
            if (player.AddEffect<SeraphAngelicSigil>(Item))
            {
                ModContent.GetInstance<FallenAngel>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SeraphimBulwarkEffect>(Item))
            {
                ModContent.GetInstance<SeraphimBulwark>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<SeraphHelm>();
            recipe.AddIngredient<SeraphArmor>();
            recipe.AddIngredient<SeraphLegs>();
            recipe.AddIngredient<GlowSting>();
            recipe.AddIngredient<SeraphimBulwark>();
            recipe.AddIngredient<FallenAngel>();
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class SeraphAngelicSigil : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SeraphEnchant>();
        }
        public class SeraphimBulwarkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SeraphEnchant>();
        }
        public class SeraphimHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SeraphEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SeraphHelm>().UpdateArmorSet(player);
            }
        }
    }
}
