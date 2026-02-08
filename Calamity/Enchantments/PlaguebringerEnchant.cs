using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.Mollusk;
using CalamityMod.Items.Armor.Plaguebringer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Summon;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class PlaguebringerEnchant : BaseEnchant
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
        public override Color nameColor => new(0, 255, 0);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ViriliEffect>(Item);
            player.AddEffect<TheBeeEffect>(Item);
            player.AddEffect<PlaguebringerEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerVisor>());
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerCarapace>());
            recipe.AddIngredient(ModContent.ItemType<PlaguebringerPistons>());
            recipe.AddIngredient(ModContent.ItemType<InfectedRemote>());
            recipe.AddIngredient(ModContent.ItemType<TheBee>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PlaguebringerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PlaguebringerVisor>().UpdateArmorSet(player);
            }
        }
        public class TheBeeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().theBee = true;
            }
        }
        public class ViriliEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                int buffType = ModContent.BuffType<ViriliBuff>();
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 3600);

                // Only spawn projectile on local player
                if (player.whoAmI != Main.myPlayer)
                    return;

                int projType = ModContent.ProjectileType<PlaguePrincess>();

                if (player.ownedProjectileCounts[projType] < 1)
                {
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(140f);
                    var source = player.GetSource_Misc("VriliEffect");

                    var proj = Projectile.NewProjectileDirect(
                        source,
                        player.Center,
                        -Vector2.UnitY,
                        projType,
                        damage,
                        0f,
                        player.whoAmI
                    );

                    proj.originalDamage = damage;
                }
            }
        }
    }
}
