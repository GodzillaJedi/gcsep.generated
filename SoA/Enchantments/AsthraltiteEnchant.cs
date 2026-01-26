using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Accessories.Sigils;
using SacredTools.Content.Items.Armor.Asthraltite;
using SacredTools.NPCs;
using SacredTools.Projectiles.Asthraltite;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static CalamityMod.Projectiles.BaseProjectiles.BaseSpearProjectile;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class AsthraltiteEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(94, 48, 117);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AsthraliteArmorEffect>(Item);
            player.AddEffect<AsthraltiteEffect>(Item);
            player.AddEffect<AsthralSpellsEffect>(Item);
            if (player.AddEffect<AsthralRingEffect>(Item))
            {
                ModContent.GetInstance<AsthralRing>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<MementoEffect>(Item))
            {
                ModContent.GetInstance<MementoMori>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ArcCasEffect>(Item))
            {
                ModContent.GetInstance<CasterArcanum>().UpdateAccessory(player, hideVisual);
            }
        }
        public class AsthraltiteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                if (Main.rand.NextBool() && hitInfo.Crit)
                {
                    hitInfo.Damage *= 2;
                }
                target.GetGlobalNPC<ModGlobalNPC>().InflictDraconicBlaze(target, 300, hitInfo.Damage / 2);
            }
        }
        public class AsthraliteArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AsthralMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<AsthralRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<AsthralMage>().UpdateArmorSet(player);
                ModContent.GetInstance<AsthralSummon>().UpdateArmorSet(player);
            }
        }
        public class AsthralRingEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();
        }
        public class MementoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();
        }
        public class ArcCasEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();
        }
        public class AsthralSpellsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AsthraltiteEnchant>();

            private static readonly int[] SwitchProjectiles =
            {
                ModContent.ProjectileType<SpellSwitchBaru>(),
                ModContent.ProjectileType<SpellSwitchMaii>(),
                ModContent.ProjectileType<SpellSwitchDoem>(),
                ModContent.ProjectileType<SpellSwitchDares>()
            };

            private static readonly int[] DeployProjectiles =
            {
                ModContent.ProjectileType<DeployBaru>(),
                ModContent.ProjectileType<DeployMaii>(),
                ModContent.ProjectileType<DeployDoem>(),
                ModContent.ProjectileType<DeployDares>()
            };

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                var mp = player.GetModPlayer<ModdedPlayer>();

                if (!mp.AstralSet)
                    return;

                // 1. Handle switching first
                if (mp.switchCooldown == 0 && (player.controlUp || player.controlDown))
                {
                    HandleSwitch(player, mp);
                    return;
                }

                // 2. If no Up/Down, cast spell
                if (!player.controlUp && !player.controlDown && mp.AsthralCooldown == 0)
                {
                    CastSpell(player, mp);
                }
            }

            private void HandleSwitch(Player player, ModdedPlayer mp)
            {
                if (player.controlUp)
                    mp.spellType = (mp.spellType + 1) % 4;

                if (player.controlDown)
                    mp.spellType = (mp.spellType + 3) % 4; // backwards wrap

                Projectile.NewProjectile(
                    player.GetSource_Misc("SpellSwitch"),
                    player.Center,
                    Vector2.Zero,
                    SwitchProjectiles[mp.spellType],
                    0,
                    0f,
                    player.whoAmI
                );

                SoundEngine.PlaySound(SoundID.Item82, player.Center);
                mp.switchCooldown = 10;
            }

            private void CastSpell(Player player, ModdedPlayer mp)
            {
                Projectile.NewProjectile(
                    player.GetSource_Misc("AsthralSpell"),
                    Main.MouseWorld,
                    Vector2.Zero,
                    DeployProjectiles[mp.spellType],
                    0,
                    0f,
                    player.whoAmI
                );

                mp.AsthralCooldown = 3600;
            }

            public override void PostUpdateEquips(Player player)
            {
                var mp = player.GetModPlayer<ModdedPlayer>();

                if (mp.switchCooldown > 0)
                    mp.switchCooldown--;

                if (mp.AsthralCooldown > 0)
                    mp.AsthralCooldown--;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AsthralChest>();
            recipe.AddIngredient<AsthralLegs>();
            recipe.AddIngredient<AsthralRing>();
            recipe.AddIngredient<CasterArcanum>();
            recipe.AddIngredient<MementoMori>();
            recipe.AddRecipeGroup("gcsep:AsthralHelms");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
