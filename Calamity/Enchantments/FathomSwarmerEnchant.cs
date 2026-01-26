using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.FathomSwarmer;
using CalamityMod.Projectiles.Rogue;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class FathomSwarmerEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(70, 63, 69);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AmuletEffect>(Item);
            player.AddEffect<SpineEffect>(Item);
            player.AddEffect<SwarmerEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerVisage>());
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<FathomSwarmerBoots>());
            recipe.AddIngredient(ModContent.ItemType<LumenousAmulet>());
            recipe.AddIngredient(ModContent.ItemType<CorrosiveSpine>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class SwarmerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FathomSwarmerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.setBonus = ModContent.GetInstance<FathomSwarmerVisage>().GetLocalizedValue("SetBonus");
                player.Calamity().fathomSwarmer = true;
                player.spikedBoots = 2;
                player.maxMinions += 2;
                player.GetDamage<SummonDamageClass>() += 0.1f;
                if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
                {
                    player.GetDamage<SummonDamageClass>() += 0.2f;
                    player.statDefense += 10;
                    player.lifeRegen += 5;
                }
            }
        }
        public class SpineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FathomSwarmerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.moveSpeed += 0.05f;
                player.Calamity().corrosiveSpine = true;
                if (player.immune && Main.rand.NextBool(15))
                {
                    IEntitySource source_Accessory = player.GetSource_Accessory(EffectItem(player));
                    int num = Main.rand.Next(2, 5);
                    for (int i = 0; i < num; i++)
                    {
                        int type = Utils.SelectRandom<int>(Main.rand, ModContent.ProjectileType<Corrocloud1>(), ModContent.ProjectileType<Corrocloud2>(), ModContent.ProjectileType<Corrocloud3>());
                        float num2 = Main.rand.NextFloat(3f, 11f);
                        int damage = (int)player.GetTotalDamage<RogueDamageClass>().ApplyTo(80f);
                        Projectile.NewProjectile(source_Accessory, player.Center, Vector2.One.RotatedByRandom(6.2831854820251465) * num2, type, damage, 0f, player.whoAmI);
                    }
                }
            }
        }
        public class AmuletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<DesolationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FathomSwarmerEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().lumenousAmulet = true;
                player.buffImmune[ModContent.BuffType<RiptideDebuff>()] = true;
                player.buffImmune[ModContent.BuffType<CrushDepth>()] = true;
            }
        }
    }
}
