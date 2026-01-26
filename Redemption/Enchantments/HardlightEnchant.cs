using CalamityBardHealer.Items;
using CalamityMod.Items.Armor.Bloodflare;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using Redemption.BaseExtension;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.HM.Hardlight;
using Redemption.Items.Weapons.HM.Ranged;
using RedemptionBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class HardlightEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(0, 242, 170);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<HardlightEffect>(Item);
            if (player.AddEffect<ShieldGeneratorEffect>(Item))
            {
                ModContent.GetInstance<PocketShieldGenerator>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<SlayerEffect>(Item))
            {
                ModContent.GetInstance<SlayerController>().UpdateAccessory(player, hideVisual);
            }
        }
        public class HardlightEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<HardlightCasque>().UpdateArmorSet(player);
                ModContent.GetInstance<HardlightCowl>().UpdateArmorSet(player);
                ModContent.GetInstance<HardlightHelm>().UpdateArmorSet(player);
                ModContent.GetInstance<HardlightHood>().UpdateArmorSet(player);
                ModContent.GetInstance<HardlightVisor>().UpdateArmorSet(player);
                ModContent.GetInstance<HardlightMask>().UpdateArmorSet(player);
                ModContent.GetInstance<HardlightReticle>().UpdateArmorSet(player);
                ModContent.GetInstance<HardlightVisage>().UpdateArmorSet(player);
            }
        }
        public class ShieldGeneratorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();
        }
        public class SlayerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();
        }
        public class HardlightDroneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override bool MinionEffect => true;
            public override int ToggleItemType => ModContent.ItemType<HardlightEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<HardlightDrone>()] < 1)
                {
                    Vector2 position = player.Center;
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        position,
                        Vector2.Zero,
                        ModContent.ProjectileType<HardlightDrone>(),
                        20,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<HardlightCasque>();
            recipe.AddIngredient<HardlightCowl>();
            recipe.AddIngredient<HardlightHelm>();
            recipe.AddIngredient<HardlightHood>();
            recipe.AddIngredient<HardlightVisor>();
            if (ModCompatibility.RedemptionBardHealer.Loaded)
            {
                recipe.AddIngredient<HardlightMask>();
                recipe.AddIngredient<HardlightReticle>();
                recipe.AddIngredient<HardlightVisage>();
            }
            recipe.AddIngredient<HardlightPlate>();
            recipe.AddIngredient<HardlightBoots>();
            recipe.AddIngredient<SlayerGun>();
            recipe.AddIngredient<PocketShieldGenerator>();
            recipe.AddIngredient<SlayerController>();

            recipe.AddTile(125);

            recipe.Register();
        }
    }
}
