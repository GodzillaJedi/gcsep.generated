using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Empyrean;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Misc;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class EmpyreanEnchant : BaseEnchant
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
        public override Color nameColor => new(20, 20, 100);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DarkHeartCalamityEffect>(Item);
            player.AddEffect<DarkSheathEffect>(Item);
            player.AddEffect<EmpyreanEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EmpyreanCuisses>());
            recipe.AddIngredient(ModContent.ItemType<EmpyreanCloak>());
            recipe.AddIngredient(ModContent.ItemType<EmpyreanMask>());
            recipe.AddIngredient(ModContent.ItemType<HeartofDarkness>());
            recipe.AddIngredient(ModContent.ItemType<DarkMatterSheath>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class EmpyreanEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EmpyreanEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<EmpyreanMask>().UpdateArmorSet(player);
            }
        }
        public class DarkHeartCalamityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EmpyreanEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().heartOfDarkness = true;
            }
        }
        public class DarkSheathEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EmpyreanEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.stealthStrikeHalfCost = true;
                calamityPlayer.rogueStealthMax += 0.1f;
                calamityPlayer.darkGodSheath = true;
                player.GetCritChance<ThrowingDamageClass>() += 6f;
                player.GetDamage<ThrowingDamageClass>() += 0.06f;
            }
        }
    }
}
