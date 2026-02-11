using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Fearmonger;
using CalamityMod.Items.Armor.SnowRuffian;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.HealerItems.Armor;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class FearfallenEnchant : BaseEnchant
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
        public override Color nameColor => new(70, 63, 69);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<VeilEffect>(Item);
            player.AddEffect<FearfallenArmorsEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FearmongerGreathelm>());
            recipe.AddIngredient(ModContent.ItemType<FearmongerPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<FearmongerGreaves>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<NightfallenHelmet>());
                recipe.AddIngredient(ModContent.ItemType<NightfallenBreastplate>());
                recipe.AddIngredient(ModContent.ItemType<NightfallenGreaves>());
            }
            recipe.AddIngredient(ModContent.ItemType<SpectralVeil>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class FearfallenArmorsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FearfallenEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FearmongerGreathelm>().UpdateArmorSet(player);
                if (ModCompatibility.Ragnarok.Loaded)
                {
                    ModContent.GetInstance<NightfallenHelmet>().UpdateArmorSet(player);
                }
            }
        }
        public class VeilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FearfallenEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().spectralVeil = true;
                player.Calamity().stealthGenMoving += 0.15f;
                player.Calamity().stealthGenStandstill += 0.15f;
            }
        }
    }
}
