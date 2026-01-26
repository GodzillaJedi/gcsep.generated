using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Guardian.Accessories;
using OrchidMod.Content.Guardian.Armors.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Orchid.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Orchid.Name)]
    [JITWhenModsEnabled(ModCompatibility.Orchid.Name)]
    public class GitGudEnchant : BaseEnchant
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
        public override Color nameColor => new(132, 212, 246);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GitGudEffect>(Item);
            if (player.AddEffect<ParryMailNinjaEffect>(Item))
            {
                ModContent.GetInstance<ParryingMailNinja>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<TempleSpikeEffect>(Item))
            {
                ModContent.GetInstance<TempleSpike>().UpdateAccessory(player, hideVisual);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GuardianGitHelm>());
            recipe.AddIngredient(ModContent.ItemType<TempleSpike>());
            recipe.AddIngredient(ModContent.ItemType<ParryingMailNinja>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class GitGudEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GitGudEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GuardianGitHelm>().UpdateArmorSet(player);
            }
        }
        public class ParryMailNinjaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GitGudEnchant>();
        }
        public class TempleSpikeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlchemistForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GitGudEnchant>();
        }
    }
}
