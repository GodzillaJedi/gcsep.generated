using CalamityMod;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Astral;
using CalamityMod.Items.Fishing.AstralCatches;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AstralEnchant : BaseEnchant
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
        public override Color nameColor => new(0, 255, 195);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AstralEffect>(Item);
            if (player.AddEffect<UrsaEffect>(Item))
            {
                ModContent.GetInstance<UrsaSergeant>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<InterstellarEffect>(Item))
            {
                ModContent.GetInstance<InterstellarStompers>().UpdateAccessory(player, hideVisual);
            }
        }
        public class UrsaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstralEnchant>();
        }
        public class InterstellarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstralEnchant>();
        }
        public class AstralEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstralEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                player.setBonus = Language.GetTextValue("Mods.gcsep.Calamity.Effects.AstralEffect.SetBonus");
                player.Calamity().astralStarRain = true;
                player.moveSpeed += 0.05f;
                player.GetDamage<GenericDamageClass>() += 0.35f;
                player.maxMinions += 3;
                player.GetCritChance<GenericDamageClass>() += 25f;
                player.Calamity().wearingRogueArmor = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AstralHelm>());
            recipe.AddIngredient(ModContent.ItemType<AstralBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<AstralLeggings>());
            recipe.AddIngredient(ModContent.ItemType<UrsaSergeant>());
            recipe.AddIngredient(ModContent.ItemType<InterstellarStompers>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
