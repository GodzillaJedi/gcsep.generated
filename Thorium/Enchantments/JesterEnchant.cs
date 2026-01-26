using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Projectiles;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;


namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class JesterEnchant : BaseEnchant
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
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<JesterEffect>(Item);
            player.AddEffect<JesterMaskEffect>(Item);
            if (player.AddEffect<FanLetterEffect>(Item))
            {
                ModContent.GetInstance<FanLetter>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<FanLetter2>().UpdateAccessory(player, hideVisual);
            }
        }
        public class JesterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<JesterEnchant>();
            public override bool MinionEffect => true;

            public override void PostUpdateMiscEffects(Player player)
            {
                if (Main.gameMenu) return;

                int projType = ModContent.ProjectileType<MinionBellProj>();
                if (player.ownedProjectileCounts[projType] < 1)
                {
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        player.Center,
                        Vector2.Zero,
                        projType,
                        0,
                        0,
                        player.whoAmI
                    );
                }
            }
        }
        public class JesterMaskEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<JesterEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<JestersMask>().UpdateArmorSet(player);
                ModContent.GetInstance<JestersMask2>().UpdateArmorSet(player);
            }
        }
        public class FanLetterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<JesterEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddRecipeGroup("gcsep:AnyJesterMask");
            recipe.AddRecipeGroup("gcsep:AnyJesterShirt");
            recipe.AddRecipeGroup("gcsep:AnyJesterLeggings");
            recipe.AddRecipeGroup("gcsep:AnyLetter");
            recipe.AddRecipeGroup("gcsep:AnyTambourine");
            recipe.AddIngredient(ModContent.ItemType<SkywareLute>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
