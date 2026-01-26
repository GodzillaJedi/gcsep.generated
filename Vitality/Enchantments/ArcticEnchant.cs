using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.NPCs;
using VitalityMod;
using VitalityMod.Items.Accessories;
using VitalityMod.Items.ArcaneGold;
using VitalityMod.Items.Arctic;
using static gcsep.Vitality.Enchantments.ArcaneGoldEnchant;

namespace gcsep.Vitality.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Vitality.Name)]
    [JITWhenModsEnabled(ModCompatibility.Vitality.Name)]
    public class ArcticEnchant : BaseEnchant
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
        public override Color nameColor => Color.DarkCyan;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ArcticHatEffect>(Item);
            if (player.AddEffect<FrozenFists>(Item))
            {
                ModContent.GetInstance<FrozenGlove>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<FrozenFangEffect>(Item))
            {
                ModContent.GetInstance<FrozenFang>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ArcticHat>());
            recipe.AddIngredient(ModContent.ItemType<ArcticGuard>());
            recipe.AddIngredient(ModContent.ItemType<ArcticPants>());
            recipe.AddIngredient(ModContent.ItemType<FrozenGlove>());
            recipe.AddIngredient(ModContent.ItemType<FrozenFang>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class ArcticHatEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ArcticEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ArcticHat>().UpdateArmorSet(player);
            }
        }
        public class FrozenFists : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ArcticEnchant>();
        }
        public class FrozenFangEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NatureForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ArcticEnchant>();
        }
    }
}
