using CalamityEntropy;
using CalamityEntropy.Content.Items.Accessories;
using CalamityEntropy.Content.Items.Armor.VoidFaquir;
using CalamityEntropy.Content.Items.Weapons;
using CalamityEntropy.Content.Items.Weapons.Chainsaw;
using CalamityEntropy.Content.Rarities;
using CalamityMod;
using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Entropy.Name)]
    [JITWhenModsEnabled(ModCompatibility.Entropy.Name)]
    public class VoidFaquirEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ModContent.RarityType<VoidPurple>();
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<GCSEPlayer>().equippedVoidFaquirEnchantment = true;
            player.AddEffect<VoidFaquirArmorEffect>(Item);
            player.AddEffect<VoidSymbiontEffect>(Item);
            player.AddEffect<ReincarnationBadgeEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirDevourerHelm>());
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirShadowHelm>());
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirCosmosHood>());
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirLurkerMask>());
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirEvokerHelm>());
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<VoidFaquirCuises>());
            recipe.AddIngredient(ModContent.ItemType<GhostdomWhisper>());
            recipe.AddIngredient(ModContent.ItemType<Pioneer>());
            recipe.AddIngredient(ModContent.ItemType<ReincarnationBadge>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class VoidFaquirArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidFaquirEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                player.Entropy().VFSet = true;
                ModContent.GetInstance<VoidFaquirDevourerHelm>().UpdateArmorSet(player);
                ModContent.GetInstance<VoidFaquirShadowHelm>().UpdateArmorSet(player);
                ModContent.GetInstance<VoidFaquirCosmosHood>().UpdateArmorSet(player);
                ModContent.GetInstance<VoidFaquirLurkerMask>().UpdateArmorSet(player);
            }
        }
        public class VoidSymbiontEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidFaquirEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VoidFaquirEvokerHelm>().UpdateArmorSet(player);
            }
        }
        public class ReincarnationBadgeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidFaquirEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ReincarnationBadge>().UpdateAccessory(player, true);
            }
        }
    }
}
