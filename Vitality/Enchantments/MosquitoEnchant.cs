using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Armor;
using VitalityMod.Items.Accessories;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class MosquitoEnchant : BaseEnchant
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
        public override Color nameColor => Color.LightSalmon;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MosquitoEffect>(Item);
            if (player.AddEffect<CharmMirageEffect>(Item))
            {
                ModContent.GetInstance<MirageCharm>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<TheScorpionEffect>(Item))
            {
                ModContent.GetInstance<ScorpionTail>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MosquitoMask>());
            recipe.AddIngredient(ModContent.ItemType<MosquitoBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<MosquitoGreaves>());
            recipe.AddIngredient(ModContent.ItemType<MirageCharm>());
            recipe.AddIngredient(ModContent.ItemType<ScorpionTail>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class MosquitoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MosquitoEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MosquitoMask>().UpdateArmorSet(player);
            }
        }
        public class CharmMirageEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MosquitoEnchant>();
        }
        public class TheScorpionEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MosquitoEnchant>();
        }
    }
}
