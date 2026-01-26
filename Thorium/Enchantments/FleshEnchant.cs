using CalamityMod;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Buffs.Pet;
using ThoriumMod.Buffs.Summon;
using ThoriumMod.Empowerments;
using ThoriumMod.Items.BossViscount;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Flesh;
using ThoriumMod.Projectiles;
using ThoriumMod.Projectiles.LightPets;
using ThoriumMod.Projectiles.Minions;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FleshEnchant : BaseEnchant
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
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FleshMaskEffect>(Item);
            if (player.AddEffect<VampireGlandEffect>(Item))
            {
                ModContent.GetInstance<VampireGland>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<VampireBatEffect>(Item))
            {
                IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
                if (player.FindBuffIndex(ModContent.BuffType<ViscountCaneBuff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<ViscountCaneBuff>(), 3600);
                }

                if (player.ownedProjectileCounts[ModContent.ProjectileType<ViscountCanePro>()] < 1)
                {
                    int num = player.ApplyArmorAccDamageBonusesTo(25f);
                    int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(num);
                    int num2 = Projectile.NewProjectile(source_ItemUse, player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<ViscountCanePro>(), damage, 0f, player.whoAmI);
                    if (Main.projectile.IndexInRange(num2))
                    {
                        Main.projectile[num2].originalDamage = num;
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FleshMask>());
            recipe.AddIngredient(ModContent.ItemType<FleshBody>());
            recipe.AddIngredient(ModContent.ItemType<FleshLegs>());
            recipe.AddIngredient(ModContent.ItemType<VampireGland>());
            recipe.AddIngredient(ModContent.ItemType<FleshMace>());
            recipe.AddIngredient(ModContent.ItemType<BloodBelcher>());
            recipe.AddIngredient(ModContent.ItemType<ViscountCane>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class FleshMaskEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FleshEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FleshMask>().UpdateArmorSet(player);
            }
        }
        public class VampireGlandEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FleshEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class VampireBatEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FleshEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
    }
}
