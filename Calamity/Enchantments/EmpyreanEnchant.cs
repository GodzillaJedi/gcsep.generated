using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Empyrean;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.xerocSet = true;
                calamityPlayer.rogueStealthMax += 1.15f;
                player.setBonus = ModContent.GetInstance<EmpyreanMask>().GetLocalization("SetBonus").Format(true);
                if (player.statLife <= (int)((double)player.statLifeMax2 * 0.5))
                {
                    player.AddBuff(ModContent.BuffType<EmpyreanWrath>(), 2);
                    player.AddBuff(ModContent.BuffType<EmpyreanRage>(), 2);
                }

                player.GetDamage<ThrowingDamageClass>() += 0.09f;
                calamityPlayer.rogueVelocity += 0.09f;
                calamityPlayer.wearingRogueArmor = true;
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
