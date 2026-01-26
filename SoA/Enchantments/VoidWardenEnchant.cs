using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Items.Armor.Lunar.Stardust;
using SacredTools.Content.Items.Armor.Oblivion;
using SacredTools.Items.Weapons.Oblivion;
using SacredTools.Items.Weapons.Special;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SoA.Enchantments.StellarPriestEnchant;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class VoidWardenEnchant : BaseEnchant
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

        public override Color nameColor => new(79, 21, 137);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<VoidWardenEffect>(Item);
            player.GetModPlayer<ModdedPlayer>().accuracy += player.ForceEffect<VoidWardenEffect>() ? 20 : 15;
            player.AddEffect<VoidWardenHelmEffect>(Item);
        }

        public class VoidWardenHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidWardenEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VoidHelm>().UpdateArmorSet(player);
            }
        }
        public class VoidWardenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VoidWardenEnchant>();

            private int bubbleTimer;

            public override void PostUpdate(Player player)
            {
                bubbleTimer++;
                if (player.velocity.X != 0 && bubbleTimer >= 20)
                {
                    SpawnBubble(player);
                    bubbleTimer = 0;
                }
            }

            private void SpawnBubble(Player player)
            {
                Vector2 position = player.Center + new Vector2(Main.rand.Next(-20, 20), -20);
                Vector2 velocity = new Vector2(0, -0.5f);

                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    position,
                    velocity,
                    ModContent.ProjectileType<NightmareBubble>(),
                    100,
                    0f,
                    player.whoAmI
                );
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VoidHelm>();
            recipe.AddIngredient<VoidChest>();
            recipe.AddIngredient<VoidLegs>();
            recipe.AddIngredient<Skill_FuryForged>();
            recipe.AddIngredient<DarkRemnant>();
            recipe.AddIngredient<EdgeOfGehenna>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
