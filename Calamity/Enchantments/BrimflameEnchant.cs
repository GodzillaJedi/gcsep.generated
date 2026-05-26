using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Brimflame;
using CalamityMod.Items.Armor.Plaguebringer;
using CalamityMod.Projectiles.Typeless;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class BrimflameEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(191, 68, 59);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FlameShellEffect>(Item);
            player.AddEffect<AbaddonEffect>(Item);
            player.AddEffect<SlagSpitterEffect>(Item);
            player.AddEffect<VoidCalamityEffect>(Item);
            player.AddEffect<SigilEffect>(Item);
            player.AddEffect<BrimflameEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BrimflameCowl>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameRobes>());
            recipe.AddIngredient(ModContent.ItemType<BrimflameBoots>());
            recipe.AddIngredient(ModContent.ItemType<VoidofCalamity>());
            recipe.AddIngredient(ModContent.ItemType<Abaddon>());
            recipe.AddIngredient(ModContent.ItemType<SlagsplitterPauldron>());
            recipe.AddIngredient(ModContent.ItemType<SigilofCalamitas>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class BrimflameEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BrimflameCowl>().UpdateArmorSet(player);
            }
        }
        public class FlameShellEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FlameLickedShell>().UpdateAccessory(player, true);
            }
        }
        public class AbaddonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<Abaddon>().UpdateAccessory(player, true);
            }
        }
        public class SlagSpitterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SlagsplitterPauldron>().UpdateAccessory(player, true);
            }
        }
        public class VoidCalamityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VoidofCalamity>().UpdateAccessory(player, true);
            }
        }
        public class SigilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SigilofCalamitas>().UpdateAccessory(player, true);
            }
        }
    }
}
