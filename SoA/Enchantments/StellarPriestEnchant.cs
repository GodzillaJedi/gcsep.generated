using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Items.Armor.Lunar.Stardust;
using SacredTools.Content.Items.Armor.Quasar;
using SacredTools.Items.Weapons.Lunatic;
using SacredTools.Items.Weapons.Oblivion;
using SacredTools.Projectiles.Lunar;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class StellarPriestEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(108, 116, 204);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<StellarPriestHelmEffect>(Item);
            if (player.AddEffect<StellarGaurdianEffect>(Item))
            {
                // Spawn the StellarGuardian projectile if it doesn't already exist
                if (player.ownedProjectileCounts[ModContent.ProjectileType<StellarGuardian>()] < 1)
                {
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<StellarGuardian>(),
                        900, // damage
                        8f,  // knockback
                        player.whoAmI
                    );
                }
            }
        }
        public class StellarGaurdianEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StellarPriestEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<StellarPriestHead>().UpdateArmorSet(player);
            }
        }
        public class StellarPriestHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StellarPriestEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<StellarPriestHead>();
            recipe.AddIngredient<StellarPriestChest>();
            recipe.AddIngredient<StellarPriestLegs>();
            recipe.AddIngredient<GalaxyScepter>();
            recipe.AddIngredient<LunarCrystalStaff>();
            recipe.AddIngredient<OblivionRod>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
