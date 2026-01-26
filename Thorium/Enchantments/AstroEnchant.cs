using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AstroEnchant : BaseEnchant
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
            Item.rare = ItemRarityID.Red;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AstroArmorEffect>(Item);
            player.AddEffect<AstroEffect>(Item);
        }
        public class AstroArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstroEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AstroHelmet>().UpdateArmorSet(player);
            }
        }
        public class AstroEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstroEnchant>();
            public override bool MinionEffect => true;

            public override void PostUpdateMiscEffects(Player player)
            {
                if (Main.gameMenu || Main.myPlayer != player.whoAmI) return;

                int projType = ModContent.ProjectileType<SpaceshipMinion>();
                if (player.ownedProjectileCounts[projType] < 1)
                {
                    Projectile.NewProjectile(
                        player.GetSource_Accessory(EffectItem(player)),
                        player.Center,
                        Vector2.Zero,
                        projType,
                        10,
                        0f,
                        player.whoAmI
                    );
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AstroHelmet>());
            recipe.AddIngredient(ModContent.ItemType<AstroSuit>());
            recipe.AddIngredient(ModContent.ItemType<AstroBoots>());
            recipe.AddIngredient(ModContent.ItemType<MeteorHeadStaff>());
            recipe.AddIngredient(ModContent.ItemType<TechniqueMeteorStomp>());
            recipe.AddIngredient(ModContent.ItemType<MeteoriteClusterBomb>(), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
