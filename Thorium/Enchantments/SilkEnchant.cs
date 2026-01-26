using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.EarlyMagic;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SilkEnchant : BaseEnchant
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
            Item.rare = 0;
            Item.value = 20000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SilkEffect>(Item);
            player.AddEffect<SilkHatEffect>(Item);
            if (player.AddEffect<ArtificersFocusEffect>(Item))
            {
                ModContent.GetInstance<ArtificersFocus>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ArtificersShieldEffect>(Item))
            {
                ModContent.GetInstance<ArtificersShield>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ArtificersRocketEffect>(Item))
            {
                ModContent.GetInstance<ArtificersRocketeers>().UpdateAccessory(player, hideVisual);
            }
        }
        public class SilkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilkEnchant>();
            public override void PostUpdateMiscEffects(Player player)
            {
                if (Main.gameMenu) return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();

                if (player.statMana < modPlayer.silkPreviousMana)
                {
                    int manaSpent = modPlayer.silkPreviousMana - player.statMana;
                    modPlayer.silkManaAccumulator += manaSpent;
                    modPlayer.silkEffectTimer = 600;

                    while (modPlayer.silkManaAccumulator >= 50 && modPlayer.silkManaStacks < 10)
                    {
                        modPlayer.silkManaStacks++;
                        modPlayer.silkManaAccumulator -= 50;
                    }
                }

                modPlayer.silkPreviousMana = player.statMana;

                if (modPlayer.silkEffectTimer > 0)
                {
                    modPlayer.silkEffectTimer--;
                    if (modPlayer.silkEffectTimer == 0)
                    {
                        modPlayer.silkManaStacks = 0;
                        modPlayer.silkManaAccumulator = 0;
                    }
                }

                player.GetDamage<GenericDamageClass>() += 0.02f * modPlayer.silkManaStacks;
            }
        }
        public class SilkHatEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilkEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SilkHat>().UpdateArmorSet(player);
            }
        }
        public class ArtificersFocusEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilkEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ArtificersShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilkEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ArtificersRocketEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SilkEnchant>();
            public override bool MutantsPresenceAffects => true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SilkHat>());
            recipe.AddIngredient(ModContent.ItemType<SilkTabard>());
            recipe.AddIngredient(ModContent.ItemType<SilkLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersFocus>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersShield>());
            recipe.AddIngredient(ModContent.ItemType<ArtificersRocketeers>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
