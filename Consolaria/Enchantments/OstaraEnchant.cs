using Consolaria.Content.Items.Accessories;
using Consolaria.Content.Items.Armor.Misc;
using Consolaria.Content.Items.Consumables;
using Consolaria.Content.Items.Weapons.Ranged;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using gcsep.Vitality.Enchantments;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using VitalityMod.BloodHunter.Armor;

namespace gcsep.Consolaria.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.Consolaria.Name)]
    [ExtendsFromMod(ModCompatibility.Consolaria.Name)]
    public class OstaraEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(148, 214, 107);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 2;
            Item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<OstaraJump>(Item);
            if (player.AddEffect<OstaraGift>(Item))
            {
                ModContent.GetInstance<OstarasGift>().UpdateAccessory(player, hideVisual);
            }
            player.noFallDmg = true;
            player.moveSpeed += 0.03f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OstaraHat>();
            recipe.AddIngredient<OstaraJacket>();
            recipe.AddIngredient<OstaraBoots>();
            recipe.AddIngredient<OstarasGift>();
            recipe.AddIngredient<EggCannon>();
            recipe.AddIngredient<CandiedFruit>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class OstaraJump : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<OstaraEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<OstaraHat>().UpdateArmorSet(player);
            }
        }
        public class OstaraGift : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HeroHeader>();
            public override int ToggleItemType => ModContent.ItemType<OstaraEnchant>();
        }
    }
}
