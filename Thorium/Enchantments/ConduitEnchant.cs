using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.Cultist;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ConduitEnchant : BaseEnchant
    {
        public const int SetBubbleMax = 5;
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
            Item.rare = 8;
            Item.value = 250000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ConduitEffect>(Item);
        }

        public class ConduitEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ConduitEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ConduitHelmet>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ConduitHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ConduitSuit>());
            recipe.AddIngredient(ModContent.ItemType<ConduitLeggings>());
            recipe.AddIngredient(ModContent.ItemType<VegaPhaser>());
            recipe.AddIngredient(ModContent.ItemType<ElectroRebounder>(), 300);
            recipe.AddIngredient(ModContent.ItemType<AncientSpark>());

            recipe.AddTile(TileID.CrystalBall);

            recipe.Register();
        }
    }
}
