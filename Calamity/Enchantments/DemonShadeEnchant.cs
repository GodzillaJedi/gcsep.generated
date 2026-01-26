using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Weapons.Magic;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonShadeEnchant : BaseEnchant
    {
        private Mod calamity;
        public override void Load()
        {
            ModLoader.TryGetMod("CalamityMod", out calamity);
        }
        
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(173, 52, 70);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<RedDevil>(Item);
            if (player.AddEffect<AngelicAllianceEffect>(Item))
            {
                ModContent.GetInstance<AngelicAlliance>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SoulCrystal>(Item))
            {
                ModContent.GetInstance<ProfanedSoulCrystal>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonshadeHelm>());
            recipe.AddIngredient(ModContent.ItemType<DemonshadeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DemonshadeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ProfanedSoulCrystal>());
            recipe.AddIngredient(ModContent.ItemType<AngelicAlliance>());
            recipe.AddIngredient(ModContent.ItemType<Apotheosis>());
            recipe.AddIngredient(ModContent.ItemType<Eternity>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }

        public class RedDevil : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DemonshadeHelm>().UpdateArmorSet(player);
            }
        }
        public class AngelicAllianceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();
            
        }
        public class SoulCrystal : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();
            
        }
    }
}
