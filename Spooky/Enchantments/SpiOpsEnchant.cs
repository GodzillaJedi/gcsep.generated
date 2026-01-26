using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Spooky.Content.Items.Quest;
using Spooky.Content.Items.SpiderCave;
using Spooky.Content.Items.SpiderCave.Armor;
using Spooky.Content.Items.SpiderCave.OldHunter;
using SpookyBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name, ModCompatibility.SpookyBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name, ModCompatibility.SpookyBardHealer.Name)]
    public class SpiOpsEnchant : BaseEnchant
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
            player.AddEffect<SciOpsEffect>(Item);
            if (player.AddEffect<HunterScarfEffect>(Item))
            {
                ModContent.GetInstance<HunterScarf>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SewingTreadEffect>(Item))
            {
                ModContent.GetInstance<SewingThread>().UpdateAccessory(player, hideVisual);
            }
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                if (player.AddEffect<CasetteEffect>(Item))
                {
                    ModContent.GetInstance<CursedCassette>().UpdateAccessory(player, hideVisual);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<SpiderHead>();
            recipe.AddIngredient<SpiderBody>();
            recipe.AddIngredient<SpiderLegs>();
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                recipe.AddIngredient<CursedCassette>();
            }
            recipe.AddIngredient<HunterScarf>();
            recipe.AddIngredient<SewingThread>();
            recipe.AddIngredient<SpiderEggCannon>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class SciOpsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiOpsEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SpiderHead>().UpdateArmorSet(player);
            }
        }
        public class SewingTreadEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiOpsEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
        public class HunterScarfEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiOpsEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
        public class CasetteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiOpsEnchant>();
            public override bool ExtraAttackEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
