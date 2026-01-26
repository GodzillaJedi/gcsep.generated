using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities.Content.Buffs.PreHardmode;
using Polarities.Content.Items.Accessories.Information.PreHardmode;
using Polarities.Content.Items.Accessories.Movement.PreHardmode;
using Polarities.Content.Items.Armor.Classless.PreHardmode.HaliteArmor;
using Polarities.Content.Items.Armor.MultiClass.Hardmode.FractalArmor;
using Polarities.Content.Items.Weapons.Melee.Knives.PreHardmode;
using Polarities.Content.Items.Weapons.Ranged.Atlatls.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class HaliteEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Polarities;
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

        public override Color nameColor => new(194, 182, 172);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HaliteArmorEffect>(Item);
            if (player.AddEffect<SaltLegEffect>(Item))
            {
                ModContent.GetInstance<SaltatoryLeg>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<HaliteHelmet>());
            recipe.AddIngredient(ModContent.ItemType<HaliteArmor>());
            recipe.AddIngredient(ModContent.ItemType<HaliteGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SaltatoryLeg>());
            recipe.AddIngredient(ModContent.ItemType<Axolatl>());
            recipe.AddIngredient(ModContent.ItemType<SaltKnife>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class HaliteArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HaliteEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HaliteHelmet>().UpdateArmorSet(player);
            }
        }
        public class SaltLegEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HaliteEnchant>();
        }
    }
}
