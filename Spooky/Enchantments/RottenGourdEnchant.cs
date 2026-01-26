using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Spooky.Content.Items.Catacomb;
using Spooky.Content.Items.Quest;
using Spooky.Content.Items.SpiderCave.Armor;
using Spooky.Content.Items.SpookyBiome;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Spooky.Enchantments.OldWoodEnchant;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class RottenGourdEnchant : BaseEnchant
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
            player.AddEffect<GourdHelmEffect>(Item);
            if (player.AddEffect<GhostBookEffect>(Item))
            {
                ModContent.GetInstance<GhostBook>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<RustyRingEffect>(Item))
            {
                ModContent.GetInstance<RustyRing>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<GourdHead>();
            recipe.AddIngredient<GourdBody>();
            recipe.AddIngredient<GourdLegs>();
            recipe.AddIngredient<GhostBook>();
            recipe.AddIngredient<RustyRing>();
            recipe.AddIngredient<GourdFlail>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class GourdHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RottenGourdEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GourdHead>().UpdateArmorSet(player);
            }
        }
        public class GhostBookEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RottenGourdEnchant>();
        }
        public class RustyRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RottenGourdEnchant>();
        }
    }
}
