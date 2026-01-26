using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.BossLoot.StarplateDrops.StarArmor;
using SpiritMod.Items.Sets.BismiteSet;
using SpiritMod.Items.Sets.BismiteSet.BismiteArmor;
using SpiritMod.Items.Sets.BriarChestLoot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class BismiteEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(164, 202, 74);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<BismiteShieldEffect>(Item))
            {
                ModContent.GetInstance<BismiteShield>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<BismiteExplosion>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismiteHelmet>(1);
            recipe.AddIngredient<BismiteChestplate>(1);
            recipe.AddIngredient<BismiteLeggings>(1);
            recipe.AddIngredient<BismiteShield>(1);
            recipe.AddIngredient<BismiteChakra>(1);
            recipe.AddIngredient<ReachBoomerang>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class BismiteExplosion : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BismiteEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BismiteHelmet>().UpdateArmorSet(player);
            }
        }
        public class BismiteShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BismiteEnchant>();
        }
    }
}