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
                player.Calamity().giantPearl = true;
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.45f, 0.8f, 0.8f);
            }
        }
        public class EmblemEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MolluskEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().aquaticEmblem = true;
                player.npcTypeNoAggro[65] = true;
                player.npcTypeNoAggro[220] = true;
                player.npcTypeNoAggro[64] = true;
                player.npcTypeNoAggro[67] = true;
                player.npcTypeNoAggro[221] = true;
                if (player.IsUnderwater())
                {
                    player.gills = true;
                }
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
