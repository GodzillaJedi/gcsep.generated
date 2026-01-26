using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities.Content.Items.Armor.Classless.Hardmode.SnakescaleArmor;
using Spooky.Content.Items.BossBags.Accessory;
using Spooky.Content.Items.Catacomb;
using Spooky.Content.Items.Catacomb.Armor;
using Spooky.Core;
using SpookyBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class FlowerEnchant : BaseEnchant
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
            player.AddEffect<FlowerAndMushroomEffect>(Item);
            if (player.AddEffect<FlowerEffect>(Item))
            {
                ModContent.GetInstance<DaffodilHairpin>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<FlowerHead>();
            recipe.AddIngredient<FlowerBody>();
            recipe.AddIngredient<FlowerLegs>();
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                recipe.AddIngredient<MushroomMarcherCap>();
                recipe.AddIngredient<MushroomMarcherArmor>();
                recipe.AddIngredient<MushroomMarcherBoots>();
            }
            recipe.AddIngredient<DaffodilHairpin>();
            recipe.AddIngredient<DaffodilBow>();
            recipe.AddIngredient<DaffodilStaff>();

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class FlowerAndMushroomEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlowerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FlowerHead>().UpdateArmorSet(player);
                if (ModCompatibility.SpookyBardHealer.Loaded)
                {
                    ModContent.GetInstance<MushroomMarcherCap>().UpdateArmorSet(player);
                }
            }
        }

        public class FlowerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlowerEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
