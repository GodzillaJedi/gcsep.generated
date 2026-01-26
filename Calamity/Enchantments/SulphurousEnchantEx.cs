using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Sulphurous;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Typeless;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasCrossmod.Content.Calamity.Items.Accessories.Enchantments;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class SulphurousEnchantEx : BaseEnchant
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

        public override Color nameColor => new Color(181, 139, 161);
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SulphurArmorEffect>(Item);
            player.AddEffect<SandCloakEffect>(Item);
            player.AddEffect<AmidiasEffect>(Item);
            player.AddEffect<DiceEffect>(Item);
            player.AddEffect<MedallionEffect>(Item);
            player.AddEffect<SulphurEnchantEffect>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SulphurousHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SulphurousBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<SulphurousLeggings>());
            recipe.AddIngredient(ModContent.ItemType<SandCloak>());
            recipe.AddIngredient(ModContent.ItemType<AmidiasPendant>());
            recipe.AddIngredient(ModContent.ItemType<OldDie>());
            recipe.AddIngredient(ModContent.ItemType<RustyMedallion>());
            recipe.AddIngredient(ModContent.ItemType<CausticCroakerStaff>());
            recipe.AddIngredient(ModContent.ItemType<SulphurEnchant>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
    public class SulphurArmorEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
        public override void PostUpdateEquips(Player player)
        {
            player.setBonus = ModContent.GetInstance<SulphurousHelmet>().GetLocalization("SetBonus").Format();
            CalamityPlayer calamityPlayer = player.Calamity();
            calamityPlayer.sulphurSet = true;
            player.GetJumpState<SulphurJump>().Enable();
            calamityPlayer.rogueStealthMax += 0.65f;
            calamityPlayer.wearingRogueArmor = true;
            player.ignoreWater = true;
        }
    }
    public class AmidiasEffect : AccessoryEffect
    {
        public const int ShardProjectiles = 2;

        public const float ShardAngleSpread = 120f;

        public int ShardCountdown;
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
        public override void PostUpdateEquips(Player player)
        {
            if (ShardCountdown <= 0)
            {
                ShardCountdown = 140;
            }

            if (ShardCountdown <= 0)
            {
                return;
            }

            ShardCountdown -= Main.rand.Next(1, 4);
            if (ShardCountdown > 0 || player.whoAmI != Main.myPlayer)
            {
                return;
            }

            IEntitySource source_Accessory = player.GetSource_Misc("AmidiasEffect");
            int num = 25;
            float x = (float)(Main.rand.Next(1000) - 500) + player.Center.X;
            float y = -1000f + player.Center.Y;
            Vector2 vector = new Vector2(x, y);
            Vector2 spinningpoint = player.Center - vector;
            spinningpoint.Normalize();
            spinningpoint *= (float)num;
            int num2 = 30;
            float num3 = -60f;
            for (int i = 0; i < 2; i++)
            {
                Vector2 vector2 = vector;
                vector2.X = vector2.X + (float)(i * 30) - (float)num2;
                Vector2 vector3 = spinningpoint.RotatedBy(MathHelper.ToRadians(num3 + 120f * (float)i / 2f));
                vector3.X = vector3.X + 3f * Main.rand.NextFloat() - 1.5f;
                int type = 0;
                int num4 = 0;
                switch (Main.rand.Next(3))
                {
                    case 0:
                        type = ModContent.ProjectileType<PendantProjectile1>();
                        num4 = 15;
                        break;
                    case 1:
                        type = ModContent.ProjectileType<PendantProjectile2>();
                        num4 = 15;
                        break;
                    case 2:
                        type = ModContent.ProjectileType<PendantProjectile3>();
                        num4 = 30;
                        break;
                }

                int num5 = (int)player.GetBestClassDamage().ApplyTo(num4);
                num5 = player.ApplyArmorAccDamageBonusesTo(num5);
                Projectile.NewProjectile(source_Accessory, vector2.X, vector2.Y, vector3.X / 3f, vector3.Y / 2f, type, num5, 5f, Main.myPlayer);
            }
        }
    }
    public class SulphurEnchantEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
        public override void PostUpdateEquips(Player player)
        {
            ModCompatibility.FargoCrossmod.Mod.Find<ModItem>("SulphurEnchant").UpdateAccessory(player, true);
        }
    }
    public class SandCloakEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
        public override void PostUpdateEquips(Player player)
        {
            player.moveSpeed += 0.05f;
            player.Calamity().sandCloak = true;
        }
    }
    public class DiceEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
        public override void PostUpdateEquips(Player player)
        {
            player.luck += 0.2f;
        }
    }
    public class MedallionEffect : AccessoryEffect
    {
        public override Header ToggleHeader => Header.GetHeader<ExplorationForceExHeader>();
        public override int ToggleItemType => ModContent.ItemType<SulphurousEnchantEx>();
        public override void PostUpdateEquips(Player player)
        {
            player.Calamity().RustyMedallionDroplets = true;
            player.buffImmune[ModContent.BuffType<Irradiated>()] = true;
        }
    }
}