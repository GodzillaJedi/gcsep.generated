using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools.Content.Items.Armor.Vulcan;
using SOTS.Items.Evil;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.BossLoot.StarplateDrops;
using SpiritMod.Items.BossLoot.StarplateDrops.StarArmor;
using SpiritMod.Items.Placeable.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SOTS.Enchantments.CursedEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class AstraliteEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(234, 197, 128);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 30000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AstraliteArmorEffect>(Item);
            if (player.AddEffect<AstraliteBoots>(Item))
            {
                ModContent.GetInstance<HighGravityBoots>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<StarMask>(1);
            recipe.AddIngredient<StarPlate>(1);
            recipe.AddIngredient<StarLegs>(1);
            recipe.AddIngredient<OrionPistol>(1);
            recipe.AddIngredient<HighGravityBoots>(1);
            recipe.AddIngredient<StarplatePainting>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class AstraliteArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstraliteEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<StarMask>().UpdateArmorSet(player);
            }
        }
        public class AstraliteBoots : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstraliteEnchant>();
        }
    }
}
