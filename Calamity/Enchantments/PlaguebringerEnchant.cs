using CalamityMod;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Armor.Mollusk;
using CalamityMod.Items.Armor.Plaguebringer;
using CalamityMod.Items.Armor.PlagueReaper;
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
                ModContent.GetInstance<TheBee>().UpdateAccessory(player, true);
            }
        }
        public class ViriliEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AnnihilationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PlaguebringerEnchant>();
            public override bool MinionEffect => true; 
            public override void PostUpdateEquips(Player player)
            {
                int buffType = ModContent.BuffType<ViriliBuff>();
                int projType = ModContent.ProjectileType<PlaguePrincess>();

                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                if (player.whoAmI != Main.myPlayer)
                    return;

                if (player.ownedProjectileCounts[projType] <= 0 && !player.dead)
                {
                    var source = GetSource_EffectItem(player);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(140f);

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
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<ViriliEffect>())
                {
                    int buffType = ModContent.BuffType<ViriliBuff>();
                    int projType = ModContent.ProjectileType<PlaguePrincess>();

                    int buffIndex = player.FindBuffIndex(buffType);
                    if (buffIndex != -1)
                    {
                        player.DelBuff(buffIndex);
                    }

                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == player.whoAmI && p.type == projType)
                        {
                            p.Kill();
                        }
                    }
                }
            }
        }
    }
}
