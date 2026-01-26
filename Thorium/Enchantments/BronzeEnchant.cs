using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Items.Bronze;
using ThoriumMod.Items.ThrownItems;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BronzeEnchant : BaseEnchant
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
            Item.rare = ItemRarityID.Green;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<BronzeEffect>(Item);
            if (player.AddEffect<BronzeHelmEffect>(Item))
            {
                ModContent.GetInstance<BronzeHelmet>().UpdateArmorSet(player);
            }
            if (player.AddEffect<OlympicTorchEffect>(Item))
            {
                ModContent.GetInstance<OlympicTorch>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ChampionsRebuttalEffect>(Item))
            {
                ModContent.GetInstance<ChampionsRebuttal>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SpartanSandlesEffect>(Item))
            {
                ModContent.GetInstance<SpartanSandles>().UpdateAccessory(player, hideVisual);
            }
        }
        public class BronzeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BronzeEnchant>();
            public override bool MutantsPresenceAffects => true;

            public override void PostUpdate(Player player)
            {
                if (Main.gameMenu) return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();

                if (player.wingTime > 0 && player.velocity.Y != 0)
                {
                    modPlayer.bronzeSwordTimer++;
                    if (modPlayer.bronzeSwordTimer >= 42)
                    {
                        SpawnSword(player);
                        modPlayer.bronzeSwordTimer = 0;
                    }
                }
                else
                {
                    modPlayer.bronzeSwordTimer = 0;
                }
            }

            private void SpawnSword(Player player)
            {
                Vector2 position = new Vector2(
                    player.position.X + Main.rand.Next(-20, 20),
                    player.position.Y + player.height + 10);

                Projectile.NewProjectile(
                    player.GetSource_Accessory(EffectItem(player)),
                    position,
                    new Vector2(0, 10),
                    ModContent.ProjectileType<SwordRainProjectile>(),
                    50,
                    5f,
                    player.whoAmI);
            }
        }
        public class BronzeHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BronzeEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class SpartanSandlesEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BronzeEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ChampionsRebuttalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BronzeEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class OlympicTorchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BronzeEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<BronzeHelmet>());
            recipe.AddIngredient(ModContent.ItemType<BronzeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<BronzeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<OlympicTorch>());
            recipe.AddIngredient(ModContent.ItemType<ChampionsRebuttal>());
            recipe.AddIngredient(ModContent.ItemType<SpartanSandles>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
