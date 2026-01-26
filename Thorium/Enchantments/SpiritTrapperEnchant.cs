using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.BossMini;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SpiritTrapperEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 80000;
        }
        public static readonly int MaxMinions = 1;
        public static readonly int SetMaxMinions = 1;
        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CSEThoriumPlayer>().SpiritTrapperEnchant = true;
            player.AddEffect<SpiritTrapperEffect>(Item);
            if (player.AddEffect<ScryingEffect>(Item))
            {
                ModContent.GetInstance<ScryingGlass>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<InnerFlameEffect>(Item))
            {
                ModContent.GetInstance<InnerFlame>().UpdateAccessory(player, hideVisual);
            }
        }
        public class SpiritTrapperEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritTrapperEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SpiritTrapperCowl>().UpdateArmorSet(player);
            }
        }
        public class ScryingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritTrapperEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class InnerFlameEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritTrapperEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperCowl>());
            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperCuirass>());
            recipe.AddIngredient(ModContent.ItemType<SpiritTrapperGreaves>());
            recipe.AddIngredient(ModContent.ItemType<InnerFlame>());
            recipe.AddIngredient(ModContent.ItemType<ScryingGlass>());
            recipe.AddIngredient(ModContent.ItemType<SpiritBlastWand>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
