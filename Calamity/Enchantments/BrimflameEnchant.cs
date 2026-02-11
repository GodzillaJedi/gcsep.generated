using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Brimflame;
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
                
            }
        }
        public class FlameShellEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().flameLickedShell = true;
            }
        }
        public class AbaddonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().abaddon = true;
                player.GetCritChance<GenericDamageClass>() += 8f;
            }
        }
        public class SlagSpitterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.sPauldron = true;
                calamityPlayer.sPauldronVisual = true;
            }
        }
        public class VoidCalamityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().voidOfCalamity = true;
                player.GetDamage<GenericDamageClass>() += 0.12f;
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_Accessory = player.GetSource_Accessory(EffectItem(player));
                    if (player.immune && player.miscCounter % 10 == 0)
                    {
                        int damage = (int)player.GetBestClassDamage().ApplyTo(30f);
                        CalamityUtils.ProjectileRain(source_Accessory, player.Center, 400f, 100f, 500f, 800f, 22f, ModContent.ProjectileType<StandingFire>(), damage, 5f, player.whoAmI);
                    }
                }
            }
        }
        public class SigilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BrimflameEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.manaMagnet = true;
                player.statManaMax2 += 100;
                player.GetDamage<MagicDamageClass>() += 0.15f;
                player.manaCost *= 0.9f;
            }
        }
    }
}
