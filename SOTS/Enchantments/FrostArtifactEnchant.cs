using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SOTS.Items;
using SOTS.Items.Permafrost;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SOTS.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    [JITWhenModsEnabled(ModCompatibility.SOTS.Name, ModCompatibility.SOTSBardHealer.Name)]
    public class FrostArtifactEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 40000;
        }
        public override Color nameColor => new(255, 128, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<ShardEffect>(Item))
            {
                ModContent.GetInstance<ShardGuard>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PermafrostEffect>(Item))
            {
                ModContent.GetInstance<PermafrostMedallion>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SupernovaEffect>(Item))
            {
                ModContent.GetInstance<SupernovaEmblem>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<FrostArtifactEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostArtifactHelmet>());
            recipe.AddIngredient(ModContent.ItemType<FrostArtifactChestplate>());
            recipe.AddIngredient(ModContent.ItemType<FrostArtifactTrousers>());
            recipe.AddIngredient(ModContent.ItemType<SupernovaEmblem>());
            recipe.AddIngredient(ModContent.ItemType<PermafrostMedallion>());
            recipe.AddIngredient(ModContent.ItemType<ShardGuard>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class FrostArtifactEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrostArtifactEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FrostArtifactHelmet>().UpdateArmorSet(player);
            }
        }
        public class ShardEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrostArtifactEnchant>();
        }
        public class PermafrostEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrostArtifactEnchant>();
        }
        public class SupernovaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SecretsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrostArtifactEnchant>();
        }
    }
}
