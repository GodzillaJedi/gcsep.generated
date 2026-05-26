using CalamityEntropy.Content.Items.Armor.Marivinium;
using CalamityMod;
using CalamityMod.Items.Accessories;
using Clamity;
using Clamity.Content.Bosses.Clamitas.Crafted.ClamitasArmor;
using Clamity.Content.Bosses.Clamitas.Crafted.Weapons;
using Clamity.Content.Bosses.Pyrogen.Drop;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class ClamitasEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PyrogenEffect>(Item);
            player.AddEffect<ClamitasEffect>(Item);
            player.AddEffect<HellFlareEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ClamitasShellmet>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasShellplate>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasCrusher>());
            recipe.AddIngredient(ModContent.ItemType<SoulOfPyrogen>());
            recipe.AddIngredient(ModContent.ItemType<HellFlare>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class PyrogenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SoulOfPyrogen>().UpdateAccessory(player, true);
            }
        }
        public class HellFlareEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HellFlare>().UpdateAccessory(player, true);
            }
        }

        public class ClamitasEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ClamitasShellmet>().UpdateArmorSet(player);
            }
        }
    }
}
