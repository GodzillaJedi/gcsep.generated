using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Statigel;
using CalamityMod.Items.Armor.Sulphurous;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class StatigelEnchantEx : BaseEnchant
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

        public override Color nameColor => new(89, 170, 204);
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<StarTaintedEffect>(Item);
            player.AddEffect<PolarizerEffect>(Item);
            player.AddEffect<StatigelArmorEffect>(Item);
            player.AddEffect<SlimeGodEffect>(Item);
            ModContent.GetInstance<StatigelEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<StatigelHeadSummon>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<StatigelHeadBard>());
                recipe.AddIngredient(ModContent.ItemType<StatigelHeadHealer>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<StatigelEarrings>());
                recipe.AddIngredient(ModContent.ItemType<StatigelFoxMask>());
            }
            recipe.AddIngredient(ModContent.ItemType<StatigelArmor>());
            recipe.AddIngredient(ModContent.ItemType<StatigelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<StarTaintedGenerator>());
            recipe.AddIngredient(ModContent.ItemType<ManaPolarizer>());
            recipe.AddIngredient(ModContent.ItemType<StatigelEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class StatigelArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<StatigelEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<StatigelHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<StatigelHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<StatigelHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<StatigelHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<StatigelHeadBard>().UpdateArmorSet(player);
                ModContent.GetInstance<StatigelHeadHealer>().UpdateArmorSet(player);
                ModContent.GetInstance<StatigelEarrings>().UpdateArmorSet(player);
                ModContent.GetInstance<StatigelFoxMask>().UpdateArmorSet(player);
            }
        }
        public class SlimeGodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<StatigelEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<StatigelHeadSummon>().UpdateArmorSet(player);
            }
        }
        public class StarTaintedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<StatigelEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().voltaicJelly = true;
                player.Calamity().starbusterCore = true;
                player.Calamity().starTaintedGenerator = true;
                player.GetDamage<SummonDamageClass>() += 0.07f;
                player.buffImmune[ModContent.BuffType<Irradiated>()] = true;
            }
        }
        public class PolarizerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<StatigelEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().manaOverloader = true;
                player.statManaMax2 += 50;
            }
        }
    }
}