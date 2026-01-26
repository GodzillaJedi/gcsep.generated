using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Spooky.Content.Items.SpookyBiome;
using Spooky.Content.Items.SpookyBiome.Armor;
using Spooky.Content.Items.SpookyHell;
using Spooky.Content.Items.SpookyHell.Armor;
using SpookyBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class OldWoodEnchant : BaseEnchant
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
            Item.defense = 1;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OldWoodHelmEffect>(Item);
            if (player.AddEffect<CreepyCandleEffect>(Item))
            {
                ModContent.GetInstance<CreepyCandle>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CandyBagEffect>(Item))
            {
                ModContent.GetInstance<CandyBag>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                if (player.AddEffect<InkPipeEffect>(Item))
                {
                    ModContent.GetInstance<LeakingInkPipe>().UpdateAccessory(player, hideVisual);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<OldWoodHead>();
            recipe.AddIngredient<OldWoodBody>();
            recipe.AddIngredient<OldWoodLegs>();
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                recipe.AddIngredient<LeakingInkPipe>();
            }
            recipe.AddIngredient<OldWoodStaff>();
            recipe.AddIngredient<CandyBag>();
            recipe.AddIngredient<CreepyCandle>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class OldWoodHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OldWoodEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OldWoodHead>().UpdateArmorSet(player);
            }
        }
        public class CreepyCandleEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OldWoodEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }

        public class CandyBagEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OldWoodEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
        public class InkPipeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<OldWoodEnchant>();
        }
    }
}
