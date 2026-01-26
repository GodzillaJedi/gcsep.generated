using CalamityBardHealer.Items;
using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Tarragon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class TarragonEnchant : BaseEnchant
    {
        private Mod calamity;
        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
                calamity = ModLoader.GetMod("CalamityMod");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(169, 106, 52);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DarkRingEffect>(Item);
            player.AddEffect<BlazingEffect>(Item);
            player.AddEffect<BraveryEffect>(Item);
            player.AddEffect<TarragonArmorEffect>(Item);
            player.AddEffect<HealerGaurdianEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<TarragonHeadSummon>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<TarragonShroud>());
                recipe.AddIngredient(ModContent.ItemType<TarragonCowl>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<TarragonParagonCrown>());
                recipe.AddIngredient(ModContent.ItemType<TarragonChapeau>());
            }
            recipe.AddIngredient(ModContent.ItemType<TarragonBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<TarragonLeggings>());
            recipe.AddIngredient(ModContent.ItemType<DarkSunRing>());
            recipe.AddIngredient(ModContent.ItemType<BadgeofBravery>());
            recipe.AddIngredient(ModContent.ItemType<BlazingCore>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class TarragonArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TarragonHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<TarragonHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<TarragonHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<TarragonHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<TarragonHeadSummon>().UpdateArmorSet(player);
                ModContent.GetInstance<TarragonShroud>().UpdateArmorSet(player);
                ModContent.GetInstance<TarragonParagonCrown>().UpdateArmorSet(player);
                ModContent.GetInstance<TarragonChapeau>().UpdateArmorSet(player);
            }
        }
        public class HealerGaurdianEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TarragonCowl>().UpdateArmorSet(player);
            }
        }
        public class DarkRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().darkSunRing = true;
            }
        }

        public class BlazingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().blazingCore = true;
            }
        }
        public class BraveryEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TarragonEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().badgeOfBravery = true;
                player.GetArmorPenetration<MeleeDamageClass>() += 5f;
            }
        }
    }
}
