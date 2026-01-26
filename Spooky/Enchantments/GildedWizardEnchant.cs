using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS.Items.Nature;
using Spooky.Content.Items.BossBags.Accessory;
using Spooky.Content.Items.SpookyBiome;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Tiles.SpookyBiome.Furniture;
using SpookyBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Spooky.Enchantments.FlowerEnchant;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class GildedWizardEnchant : BaseEnchant
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
            player.AddEffect<GildedWizardEffect>(Item);
            if (player.AddEffect<GlowshroomEffect>(Item))
            {
                ModContent.GetInstance<BustlingGlowshroom>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                if (player.AddEffect<CandleCoveEffect>(Item))
                {
                    ModContent.GetInstance<CandleCove>().UpdateAccessory(player, hideVisual);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddRecipeGroup("gcsep:AnyGildedHat");
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                recipe.AddIngredient<FiestaGarments>();
                recipe.AddIngredient<FiestaPantaloons>();
                recipe.AddIngredient<CandleCove>();
            }
            recipe.AddIngredient<WizardGangsterBody>();
            recipe.AddIngredient<WizardGangsterLegs>();
            recipe.AddIngredient<BustlingGlowshroom>();
            recipe.AddIngredient<ShroomWhip>();
            recipe.AddIngredient<SwoleMushroomStatueItem>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class GildedWizardEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlowerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<WizardGangsterHead>().UpdateArmorSet(player);
                ModContent.GetInstance<WizardGangsterHead2>().UpdateArmorSet(player);
                if (ModCompatibility.SpookyBardHealer.Loaded)
                {
                    ModContent.GetInstance<FiestaMaskara>().UpdateArmorSet(player);
                }
            }
        }
        public class GlowshroomEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlowerEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class CandleCoveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlowerEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
