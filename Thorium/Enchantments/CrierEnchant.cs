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
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Coral;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class CrierEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public static readonly int SetEmpowermentDuration = 3;
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CrierEffect>(Item);
            if (player.AddEffect<LuckyRabbitEffect>(Item))
            {
                ModContent.GetInstance<LuckyRabbitsFoot>().UpdateAccessory(player, hideVisual);
            }
        }
        public class CrierEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CrierEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CriersCap>().UpdateArmorSet(player);
            }
        }
        public class LuckyRabbitEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CrierEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CriersCap>());
            recipe.AddIngredient(ModContent.ItemType<CriersSash>());
            recipe.AddIngredient(ModContent.ItemType<CriersLeggings>());
            recipe.AddIngredient(ModContent.ItemType<LuckyRabbitsFoot>());
            recipe.AddIngredient(ModContent.ItemType<WoodenWhistle>());
            recipe.AddRecipeGroup("gcsep:AnyBugleHorn");

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
