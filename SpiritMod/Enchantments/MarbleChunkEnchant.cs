using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using SpiritMod.Items.Sets.GraniteSet.GraniteArmor;
using SpiritMod.Items.Sets.MarbleSet;
using SpiritMod.Items.Sets.MarbleSet.MarbleArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class MarbleChunkEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(206, 182, 95);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MarbleChunkEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<MarbleHelm>(1);
            recipe.AddIngredient<MarbleChest>(1);
            recipe.AddIngredient<DefenseCodex>(1);
            recipe.AddIngredient<MarbleBident>(1);
            recipe.AddIngredient<MarbleStaff>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class MarbleChunkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarbleChunkEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MarbleHelm>().UpdateArmorSet(player);
            }
        }
    }
}
