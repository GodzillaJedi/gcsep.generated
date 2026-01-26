using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Spooky.Content.Items.BossBags.Accessory;
using Spooky.Content.Items.Catacomb;
using Spooky.Content.Items.Cemetery;
using Spooky.Content.Items.Cemetery.Armor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpiritHorsemenEnchant : BaseEnchant
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
            player.AddEffect<SpiritHorsemenEffect>(Item);
            if (player.AddEffect<SkullAmuletEffect>(Item))
            {
                ModContent.GetInstance<SkullAmulet>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpiritAmuletEffect>(Item))
            {
                ModContent.GetInstance<SpiritAmulet>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<SpiritHorsemanHead>();
            recipe.AddIngredient<SpiritHorsemanBody>();
            recipe.AddIngredient<SpiritHorsemanLegs>();
            recipe.AddIngredient<SpiritAmulet>();
            recipe.AddIngredient<SkullAmulet>();
            recipe.AddIngredient<SpiritScroll>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class SpiritHorsemenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritHorsemenEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SpiritHorsemanHead>().UpdateArmorSet(player);
            }
        }
        public class SkullAmuletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritHorsemenEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
        public class SpiritAmuletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HorrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritHorsemenEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
