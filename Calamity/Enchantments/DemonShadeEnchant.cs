using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Demonshade;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Buffs.Minions;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Reworks.DemonshadeHelmRework;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class DemonShadeEnchant : BaseEnchant
    {
        private Mod calamity;
        private static MethodInfo getPscStateMethod = null;
        public override void Load()
        {
            ModLoader.TryGetMod("CalamityMod", out calamity);

            if (ModLoader.HasMod("CalamityMod"))
            {
                // Target the background method hook
                var eligibilityMethod = typeof(ProfanedSoulCrystal).GetMethod("DetermineTransformationEligibility", BindingFlags.NonPublic | BindingFlags.Static);
                if (eligibilityMethod != null)
                {
                    MonoModHooks.Add(eligibilityMethod, BypassCrystalEligibilityCheck);
                }

                // FIXED REFLECTION LOOKUP: Pulls Calamity's 'internal static' method hidden from the assembly
                getPscStateMethod = typeof(ProfanedSoulCrystal).GetMethod("GetPscStateFor", BindingFlags.NonPublic | BindingFlags.Static);
            }
        }
        public override void Unload()
        {
            calamity = null;
            getPscStateMethod = null;
        }
        private delegate void orig_DetermineTransformationEligibility(Player player);
        private static void BypassCrystalEligibilityCheck(orig_DetermineTransformationEligibility orig, Player player)
        {
            orig(player);

            if (player.HasEffect<SoulCrystal>())
            {
                player.Calamity().profanedCrystalBuffs = true;

                // FIXED EXECUTION: Invokes Calamity's hidden internal engine using reflection safely!
                if (player.Calamity().pscState == 0 && getPscStateMethod != null)
                {
                    // Invokes: GetPscStateFor(player, false)
                    object stateResult = getPscStateMethod.Invoke(null, new object[] { player, false });
                    if (stateResult != null)
                    {
                        player.Calamity().pscState = (int)stateResult;
                    }
                }
            }
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ModContent.RarityType<HotPink>();
            Item.value = 50000000;
        }
        public override Color nameColor => new(173, 52, 70);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<RedDevil>(Item);
            player.AddEffect<AngelicAllianceEffect>(Item);
            player.AddEffect<SoulCrystal>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DemonshadeHelm>());
            recipe.AddIngredient(ModContent.ItemType<DemonshadeBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DemonshadeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ProfanedSoulCrystal>());
            recipe.AddIngredient(ModContent.ItemType<AngelicAlliance>());
            recipe.AddIngredient(ModContent.ItemType<Apotheosis>());
            recipe.AddIngredient(ModContent.ItemType<Eternity>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class RedDevil : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();
            public static int DevilDamage = 1000;

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DemonshadeHelm>().UpdateArmorSet(player);

                player.GetModPlayer<GCSEPlayer>().rDevil = true;

                int buffType = ModContent.BuffType<DemonshadeSetDevilBuff>();
                if (!player.HasBuff(buffType))
                    player.AddBuff(buffType, 60);

                DemonshadeDevilHelper.EnsureDevil(player, GetSource_EffectItem(player), DevilDamage);
            }
        }

        public class AngelicAllianceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                // 1. Force background logic on natively 
                player.Calamity().angelicAlliance = true;

                // 2. STAT MODIFIERS: Directly inherit from your source layout configuration
                player.GetDamage<GenericDamageClass>() += 0.08f;
                player.GetDamage<SummonDamageClass>() += 0.07f;
                player.maxMinions += 2;

                if (player.wingTime < player.wingTimeMax)
                {
                    player.lifeRegen += 4;
                }

                // 3. FIXED CALAMITY INPUT RECONSTRUCTION:
                // We look directly at the active keybind registry instance from Calamity, bypassing FindAccessory!
                if (CalamityMod.CalamityKeybinds.Accessory1 != null &&
                    CalamityMod.CalamityKeybinds.Accessory1.JustPressed &&
                    Main.myPlayer == player.whoAmI)
                {
                    int blessBuff = ModContent.BuffType<CalamityMod.Buffs.StatBuffs.DivineBless>();

                    // Check if they are clear of active cooldown arrays before initializing the blast
                    // Verify if your 2.0.7.2 build uses Cooldowns namespace tracking for DivineBless
                    if (!player.HasBuff(blessBuff) && !player.HasCooldown(CalamityMod.Cooldowns.DivineBless.ID))
                    {
                        // Trigger the buff length mapping configuration parameters
                        player.AddBuff(blessBuff, 900); // 15 seconds * 60 frames = 900 ticks
                        Terraria.Audio.SoundEngine.PlaySound(in AngelicAlliance.ActivationSound, player.Center);

                        // 4. ARCHANGEL SCANNER RANGE LOOP (Copied directly from your source data)
                        float activeMinionCount = 0f;
                        for (int k = 0; k < Main.maxProjectiles; k++)
                        {
                            Projectile projectile2 = Main.projectile[k];
                            // Clean check to see if the projectile counts as a valid summon minion slot container
                            if (projectile2.active && projectile2.owner == player.whoAmI &&
                                (projectile2.minionSlots > 0f || projectile2.type == ModContent.ProjectileType<Pigion>())) // Direct namespace path prevent compile locks
                            {
                                if (projectile2.CountsAsClass<SummonDamageClass>())
                                {
                                    activeMinionCount += 1f;
                                }
                            }
                        }
                        var source = player.GetSource_Accessory(EffectItem(player));

                        // We create a counter index to match against the minion array correctly
                        int currentAngelCount = 0;

                        for (int k = 0; k < Main.maxProjectiles; k++)
                        {
                            Projectile projectile2 = Main.projectile[k];

                            // Re-verify the valid minion index slots so we only grab our active summons
                            if (projectile2.active && projectile2.owner == player.whoAmI &&
                                (projectile2.minionSlots > 0f || projectile2.type == ModContent.ProjectileType<Pigion>()))
                            {
                                if (projectile2.CountsAsClass<SummonDamageClass>())
                                {
                                    float angleDegreeStep = 360f / activeMinionCount;

                                    // Math orbital distribution geometry coordinates matching CalPlayer source specs
                                    double spawnX = player.Center.X + Math.Sin((float)currentAngelCount * angleDegreeStep) * 300.0;
                                    double spawnY = player.Center.Y + Math.Cos((float)currentAngelCount * angleDegreeStep) * 300.0;
                                    Vector2 spawnPosition = new Vector2((int)spawnX, (int)spawnY);

                                    // FIXED SYNTAX: Cleaned up the cut-off ModContent line
                                    Projectile.NewProjectile(
                                        source,
                                        spawnPosition,
                                        Vector2.Zero,
                                        ModContent.ProjectileType<AngelicAllianceArchangel>(),
                                        projectile2.damage / 10,
                                        projectile2.knockBack / 10f,
                                        player.whoAmI,
                                        Main.rand.Next(180),
                                        (float)currentAngelCount * angleDegreeStep
                                    );

                                    // Apply the flat custom instant recovery healing adjustments
                                    player.HealPlayer(2);

                                    currentAngelCount++;
                                    if (currentAngelCount >= activeMinionCount)
                                        break; // Safety cutoff
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public class SoulCrystal : AccessoryEffect
    {
         public override Header ToggleHeader => Header.GetHeader<SalvationForceHeader>();
         public override int ToggleItemType => ModContent.ItemType<DemonShadeEnchant>();

         public override void PostUpdateEquips(Player player)
         {
                CalamityPlayer modPlayer = player.Calamity();

                modPlayer.pSoulArtifact = true;
                modPlayer.profanedCrystal = true;
                modPlayer.pSoulShieldVisible = true;
                modPlayer.profanedSoulRelicBuff = true;

                if (player.Transformation().Type == -1)
                {
                    player.Transformation().Type = ModContent.ItemType<ProfanedSoulCrystal>();
                }
         }
        
    }
}
