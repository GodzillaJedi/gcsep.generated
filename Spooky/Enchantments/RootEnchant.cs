using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Spooky.Content.Items.SpiderCave.Armor;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class RootEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Spooky;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<RootHelmEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<RootHead>();
            recipe.AddIngredient<RootBody>();
            recipe.AddIngredient<RootLegs>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class RootHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RootEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<RootHead>().UpdateArmorSet(player);
            }
        }
    }
}
