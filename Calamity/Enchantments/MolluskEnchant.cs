using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.Mollusk;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class MolluskEnchant : BaseEnchant
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
        public override Color nameColor => new(153, 200, 193);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PearlEffect>(Item);
            player.AddEffect<EmblemEffect>(Item);
            player.AddEffect<ShellfishEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MolluskShellmet>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShellplate>());
            recipe.AddIngredient(ModContent.ItemType<MolluskShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<GiantPearl>());
            recipe.AddIngredient(ModContent.ItemType<AquaticEmblem>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PearlEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MolluskEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GiantPearl>().UpdateAccessory(player, true);
            }
        }
        public class EmblemEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MolluskEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AquaticEmblem>().UpdateAccessory(player, true);
            }
        }
        public class ShellfishEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MolluskEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MolluskShellmet>().UpdateArmorSet(player);
            }
        }
    }
}
