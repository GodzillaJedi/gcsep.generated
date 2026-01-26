using CalamityMod.Items.Armor.Aerospec;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.DemonBlood;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.FleshEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DemonBloodEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public static readonly int SetMaxLife = 100;
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DemonBloodEffect>(Item);
            if (player.AddEffect<VileFlailCoreEffect>(Item))
            {
                ModContent.GetInstance<VileFlailCore>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<FleshEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class DemonBloodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonBloodEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DemonBloodHelmet>().UpdateArmorSet(player);
            }
        }
        public class VileFlailCoreEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonBloodEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DemonBloodHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DemonBloodBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<DemonBloodGreaves>());
            recipe.AddIngredient(ModContent.ItemType<FleshEnchant>());
            recipe.AddIngredient(ModContent.ItemType<VileSpitter>());
            recipe.AddIngredient(ModContent.ItemType<VileFlailCore>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
