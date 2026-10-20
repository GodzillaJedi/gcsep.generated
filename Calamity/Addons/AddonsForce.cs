using CalamityEntropy.Content.Items.Armor.Azafure;
using CalamityEntropy.Content.Items.Armor.AzafureT3;
using CalamityEntropy.Content.Items.Armor.NihTwins;
using CalamityEntropy.Content.Items.Armor.Smoldering;
using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class AddonsForce : BaseForce
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ModCompatibility.Clamity.Loaded)
            {
                ModContent.GetInstance<ClamitasEnchant>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<FrozenEnchant>().UpdateAccessory(player, hideVisual);
            }

            if (ModCompatibility.Catalyst.Loaded)
            {
                ModContent.GetInstance<IntergelacticEnchant>().UpdateAccessory(player, false);
            }

            if (ModCompatibility.Goozma.Loaded)
            {
                ModContent.GetInstance<ShogunEnchant>().UpdateAccessory(player, false);
            }

            if (ModCompatibility.Entropy.Loaded)
            {
                ModContent.GetInstance<MariviumEnchant>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<VoidFaquirEnchant>().UpdateAccessory(player, hideVisual);
                player.AddEffect<SmolderingEffect>(Item);
                player.AddEffect<AzafureEffect>(Item);
                player.AddEffect<AcropolisEffect>(Item);
                player.AddEffect<NihilistEffect>(Item);
                player.AddEffect<AnarchoEffect>(Item);
            }
            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                ModContent.GetInstance<SolynsSigil>().UpdateAccessory(player, hideVisual);
            }
        }
        public class SmolderingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AddonsForce>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SmolderingHelmet>().UpdateArmorSet(player);
            }
        }
        public class AzafureEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AddonsForce>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AzafureSteamKnightHelmet>().UpdateArmorSet(player);
                ModContent.GetInstance<AzafureHeavyHelmet>().UpdateArmorSet(player);
            }
        }
        public class AcropolisEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AddonsForce>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AcropolisHeavyHelmet>().UpdateArmorSet(player);
            }
        }
        public class NihilistEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AddonsForce>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VoidEaterHelmet>().UpdateArmorSet(player);
            }
        }
        public class AnarchoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AddonsForce>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ChaoticHelmet>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            if (ModCompatibility.Clamity.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<FrozenEnchant>());
                recipe.AddIngredient(ModContent.ItemType<ClamitasEnchant>());
            }

            if (ModCompatibility.Catalyst.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<IntergelacticEnchant>());
            }

            if (ModCompatibility.Goozma.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<ShogunEnchant>());
            }

            if (ModCompatibility.Entropy.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<VoidFaquirEnchant>());
                recipe.AddIngredient(ModContent.ItemType<MariviumEnchant>());
            }

            if (ModCompatibility.WrathoftheGods.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<SolynsSigil>());
            }

            recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
            recipe.Register();
        }
    }
}
