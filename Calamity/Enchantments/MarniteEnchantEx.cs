using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.MarniteArchitect;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class MarniteEnchantEx : BaseEnchant
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
            player.AddEffect<LiftEffect>(Item);
            player.AddEffect<HallowedEffect>(Item);
            player.AddEffect<RoverEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<MarniteArchitectToga>());
            recipe.AddIngredient(ModContent.ItemType<MarniteEnchant>());
            recipe.AddIngredient(ModContent.ItemType<HallowedRune>());
            recipe.AddIngredient(ModContent.ItemType<RoverDrive>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class LiftEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.setBonus = "Marnite Lift";
                player.GetModPlayer<MarniteArchitectPlayer>().setEquipped = true;
            }
        }
        public class HallowedEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().hallowedRune = true;
            }
        }
        public class RoverEffect : AccessoryEffect
        {
            private const int ShieldDefenseBoost = 20;

            public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarniteEnchantEx>();

            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                calamityPlayer.roverDrive = true;
                calamityPlayer.roverDriveShieldVisible = true;

                if (calamityPlayer.RoverDriveShieldDurability > 0)
                {
                    player.statDefense += ShieldDefenseBoost;
                }
            }
        }
    }
}
