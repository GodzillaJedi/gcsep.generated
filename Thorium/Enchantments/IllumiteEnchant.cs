using Fargowiltas.Common.Configs;
using FargowiltasSouls;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Illumite;
using static FargowiltasSouls.Content.Items.Accessories.Forces.TimberForce;
using ThoriumMod.Items.BossMini;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class IllumiteEnchant : BaseEnchant
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
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<IllumiteEffect>(Item);
            player.AddEffect<IllumiteArmorEffect>(Item);
            if (player.AddEffect<NuclearOptionEffect>(Item))
            {
                ModContent.GetInstance<TheNuclearOption>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<IllumiteMask>());
            recipe.AddIngredient(ModContent.ItemType<IllumiteChestplate>());
            recipe.AddIngredient(ModContent.ItemType<IllumiteGreaves>());
            recipe.AddIngredient(ModContent.ItemType<TheNuclearOption>());
            recipe.AddIngredient(ModContent.ItemType<TunePlayerLifeRegen>());
            recipe.AddIngredient(ModContent.ItemType<HandCannon>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class IllumiteArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IllumiteEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<IllumiteMask>().UpdateArmorSet(player);
            }
        }
        public class NuclearOptionEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IllumiteEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class IllumiteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IllumiteEnchant>();
            public override bool ActiveSkill => true;

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (!player.GetModPlayer<CSEThoriumPlayer>().illumiteNightVision)
                {
                    player.GetModPlayer<CSEThoriumPlayer>().illumiteNightVision = true;
                }
                else
                {
                    player.GetModPlayer<CSEThoriumPlayer>().illumiteNightVision = false;
                }
            }
            public static int Range(Player player) => (int)250f;

            public override void PostUpdateEquips(Player player)
            {
                if (Main.gameMenu || player.whoAmI != Main.myPlayer)
                    return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                if (modPlayer.healCD > 0)
                    modPlayer.healCD--;

                if (!Main.dayTime)
                {
                    int visualProj = ModContent.ProjectileType<IllumiteAuraProj>();
                    if (player.ownedProjectileCounts[visualProj] <= 0)
                    {
                        Projectile.NewProjectile(GetSource_EffectItem(player), player.Center, Vector2.Zero, visualProj, 0, 0, player.whoAmI);
                    }

                    int dist = Range(player);

                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player target = Main.player[i];
                        if (target.active && !target.dead && target.whoAmI != player.whoAmI &&
                            target.team == player.team && player.team != 0)
                        {
                            if (player.Distance(target.Center) < dist &&
                                Collision.CanHitLine(player.Center, 0, 0, target.Center, 0, 0))
                            {
                                if (modPlayer.healCD <= 0)
                                {
                                    target.Heal(10);
                                    modPlayer.healCD = 600;
                                }
                            }
                        }
                    }
                }
            }
        }

        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
        public class IllumiteAuraProj : ModProjectile
        {
            public override string Texture => FargoSoulsUtil.EmptyTexture;

            public override Color? GetAlpha(Color lightColor) => lightColor * Projectile.Opacity;

            public override void SetDefaults()
            {
                Projectile.width = 32;
                Projectile.height = 32;
                Projectile.friendly = false;
                Projectile.penetrate = -1;
                Projectile.scale = 1f;
                Projectile.timeLeft = 60;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
            }
            public override void AI()
            {
                if (!Projectile.owner.IsWithinBounds(Main.maxPlayers))
                {
                    Projectile.Kill();
                    return;
                }
                Player player = Main.player[Projectile.owner];
                if (!player.Alive() || !player.HasEffect<IllumiteEffect>() || Main.dayTime)
                {
                    Projectile.Kill();
                    return;
                }
                Projectile.Center = player.Center;
                Projectile.timeLeft = 60;
                Projectile.ai[0] = IllumiteEffect.Range(player);
            }
            public static bool CombinedAura(Player player) => player.HasEffect<NatureEffect>() && player.HasEffect<MoltenEffect>() && (player.HasEffect<EbonwoodEffect>() || player.HasEffect<EbonwoodEffect>()) && player.HasEffect<TimberEffect>();
            public override bool PreDraw(ref Color lightColor)
            {
                if (!Projectile.owner.IsWithinBounds(Main.maxPlayers))
                {
                    Projectile.Kill();
                    return false;
                }
                Player player = Main.player[Projectile.owner];
                if (!player.Alive() || !player.HasEffect<IllumiteEffect>())
                {
                    Projectile.Kill();
                    return false;
                }

                if (CombinedAura(player))
                {
                    return false;
                }

                Color darkColor = Color.Purple;
                Color mediumColor = Color.DeepPink;
                Color lightColor2 = Color.Lerp(Color.HotPink, Color.White, 0.35f);

                Vector2 auraPos = player.Center;
                float radius = Projectile.ai[0];
                var target = Main.LocalPlayer;
                var blackTile = TextureAssets.MagicPixel;
                var diagonalNoise = FargosTextureRegistry.HoneycombNoise;
                if (!blackTile.IsLoaded || !diagonalNoise.IsLoaded)
                    return false;
                var maxOpacity = Projectile.Opacity * ModContent.GetInstance<FargoClientConfig>().TransparentFriendlyProjectiles;

                ManagedShader borderShader = ShaderManager.GetShader("FargowiltasSouls.GenericInnerAura");
                borderShader.TrySetParameter("colorMult", 7.35f);
                borderShader.TrySetParameter("time", Main.GlobalTimeWrappedHourly);
                borderShader.TrySetParameter("radius", radius);
                borderShader.TrySetParameter("anchorPoint", auraPos);
                borderShader.TrySetParameter("screenPosition", Main.screenPosition);
                borderShader.TrySetParameter("screenSize", Main.ScreenSize.ToVector2());
                borderShader.TrySetParameter("playerPosition", target.Center);
                borderShader.TrySetParameter("maxOpacity", maxOpacity);
                borderShader.TrySetParameter("darkColor", darkColor.ToVector4());
                borderShader.TrySetParameter("midColor", mediumColor.ToVector4());
                borderShader.TrySetParameter("lightColor", lightColor2.ToVector4());
                borderShader.TrySetParameter("opacityAmp", 1f);

                Main.spriteBatch.GraphicsDevice.Textures[1] = diagonalNoise.Value;

                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.LinearWrap, DepthStencilState.None, Main.Rasterizer, borderShader.WrappedEffect, Main.GameViewMatrix.TransformationMatrix);
                Rectangle rekt = new(Main.screenWidth / 2, Main.screenHeight / 2, Main.screenWidth, Main.screenHeight);
                Main.spriteBatch.Draw(blackTile.Value, rekt, null, default, 0f, blackTile.Value.Size() * 0.5f, 0, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
                return false;
            }
        }
        [ExtendsFromMod(ModCompatibility.Thorium.Name)]
        [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
        public class LightingSystemTest : ModSystem
        {
            public override void PostUpdateEverything()
            {
                bool shouldLight = false;

                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player.active && !player.dead && player.GetModPlayer<CSEThoriumPlayer>().illumiteNightVision)
                    {
                        shouldLight = true;
                        break;
                    }
                }

                if (shouldLight)
                {
                    //Lighting.GlobalBrightness = 1.5f;
                }
                else
                {
                    //Lighting.GlobalBrightness = 1f;
                }
            }
        }
    }
}
