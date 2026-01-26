using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.BossMini;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TideTurnerEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new Color(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<TideTurnerHelmEffect>(Item);
            player.AddEffect<TideTurnerCrownEffect>(Item);
            if (player.AddEffect<PlagueLordEffect>(Item))
            {
                ModContent.GetInstance<PlagueLordFlask>().UpdateAccessory(player, hideVisual);
            }
        }
        public class TideTurnerCrownEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideTurnerEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TideTurnersGaze>().UpdateArmorSet(player);
            }
        }
        public class TideTurnerHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideTurnerEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<TideTurnerHelmet>().UpdateArmorSet(player);
            }
        }
        public class PlagueLordEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideTurnerEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TideTurnersGaze>());
            recipe.AddIngredient(ModContent.ItemType<TideTurnerHelmet>());
            recipe.AddIngredient(ModContent.ItemType<TideTurnerBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<TideTurnerGreaves>());
            recipe.AddIngredient(ModContent.ItemType<PlagueLordFlask>());
            recipe.AddIngredient(ModContent.ItemType<PoseidonCharge>());
            recipe.AddIngredient(ModContent.ItemType<TidalWave>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
