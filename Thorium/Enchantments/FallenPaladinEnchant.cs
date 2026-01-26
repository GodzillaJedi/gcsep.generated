using CalamityMod.Items.Armor.Aerospec;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.TemplarEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FallenPaladinEnchant : BaseEnchant
    {
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
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FallenPaladinEffect>(Item);
            if (player.AddEffect<NirvanaEffect>(Item))
            {
                ModContent.GetInstance<NirvanaStatuette>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<PrydwenEffect>(Item))
            {
                ModContent.GetInstance<Prydwen>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<TemplarEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class FallenPaladinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SacredEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FallenPaladinFaceguard>().UpdateArmorSet(player);
            }
        }
        public class NirvanaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SacredEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class PrydwenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SacredEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {


            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FallenPaladinFaceguard>());
            recipe.AddIngredient(ModContent.ItemType<FallenPaladinCuirass>());
            recipe.AddIngredient(ModContent.ItemType<FallenPaladinGreaves>());
            recipe.AddIngredient(ModContent.ItemType<TemplarEnchant>());
            recipe.AddIngredient(ModContent.ItemType<Prydwen>()); //WHO TF NAMED THIS THING Wynebgwrthucher
            recipe.AddIngredient(ModContent.ItemType<NirvanaStatuette>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
