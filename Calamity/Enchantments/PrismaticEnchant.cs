using CalamityMod;
using CalamityMod.Items.Armor.Prismatic;
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
    public class PrismaticEnchant : BaseEnchant
    {
        private Mod calamity;

        public override void Load()
        {
            if (ModLoader.HasMod("CalamityMod"))
                calamity = ModLoader.GetMod("CalamityMod");
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
        public override Color nameColor => new(0, 104, 94);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PrismaticArmorEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PrismaticHelmet>());
            recipe.AddIngredient(ModContent.ItemType<PrismaticRegalia>());
            recipe.AddIngredient(ModContent.ItemType<PrismaticGreaves>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class PrismaticArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrismaticEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().prismaticSet = true;
                player.statManaMax2 += 40;
                player.manaCost *= 0.85f;
                player.manaRegenBonus += 8;
                string text = CalamityKeybinds.ArmorSetBonusHotKey.TooltipHotkeyString();
                player.setBonus = ModContent.GetInstance<PrismaticHelmet>().GetLocalization("SetBonus").Format(text);
            }
        }
    }
}
