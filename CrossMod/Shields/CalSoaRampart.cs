using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools.Common.Players;
using SacredTools.Common.Systems;
using SacredTools.Content.Buffs;
using SacredTools.Content.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Crossmod.Shields
{
    /*
        * Progression look like this:
        * frozen shield + deific amulet
        * celestial shield
        * shield of reflection
        * rampart
    */

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class RampartShieldRecepies : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Shields;
        }
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                // frozen shield + delific amulet to celestial
                if (recipe.HasResult(ModContent.ItemType<CelestialShield>()) && recipe.HasIngredient(ItemID.AnkhShield))
                {
                    recipe.RemoveIngredient(ItemID.AnkhShield);
                    recipe.RemoveIngredient(ItemID.PaladinsShield);
                    recipe.AddIngredient(ItemID.FrozenShield, 1);
                    recipe.AddIngredient<DeificAmulet>(1);
                }
                // celestial to reflection
                // soa code
                // reflections to rampart
                if (recipe.HasResult(ModContent.ItemType<RampartofDeities>()) && recipe.HasIngredient(ItemID.FrozenShield))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<DeificAmulet>());
                    recipe.RemoveIngredient(ItemID.FrozenShield);
                    recipe.AddIngredient<ReflectionShield>(1);
                }
                //rampart to colossus (if no cal dlc)
                if (recipe.HasResult(ModContent.ItemType<ColossusSoul>()) && recipe.HasIngredient(3997))
                {
                    recipe.RemoveIngredient(3997);
                    recipe.AddIngredient<RampartofDeities>(1);
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.SacredTools.Name)]
    public class RampartShieldEffects : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod) => GCSEConfig.Instance.Shields;
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<ColossusSoul>())
            {
                if (item.type == ModContent.ItemType<CelestialShield>())
                    player.AddEffect<CelestialShieldEffect>(item);

                // Reflection Shield
                if (item.type == ModContent.ItemType<ReflectionShield>())
                    player.AddEffect<ReflectionShieldEffect>(item);

                // Rampart of Deities
                if (item.type == ModContent.ItemType<RampartofDeities>())
                    player.AddEffect<RampartOfDeitiesEffect>(item);
            }
        }
        public class CelestialShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                // Panic buff at 50% HP
                if (player.statLife <= player.statLifeMax2 * 0.5f)
                    player.AddBuff(BuffID.Panic, 5);

                // Always no knockback
                player.noKnockback = true;

                // Paladin Shield effect above 25% HP
                if (player.statLife > player.statLifeMax2 * 0.25f)
                    player.hasPaladinShield = true;

                // Team shielding pulse
                if (player.whoAmI == Main.myPlayer && player.miscCounter % 10 == 0)
                {
                    Player local = Main.player[Main.myPlayer];
                    if (local.team == player.team && player.team != 0 &&
                        Vector2.Distance(local.Center, player.Center) < 800f)
                    {
                        local.AddBuff(BuffID.Ironskin, 20);
                    }
                }
            }
        }
        public class ReflectionShieldEffect : CelestialShieldEffect
        {
            public static readonly int LifeBonus = 100;
            public override void PostUpdateEquips(Player player)
            {
                if (TrueModeSystem.TrueMode)
                {
                    player.wolfAcc = true;
                    player.hideWolf = true;
                    player.accMerman = true;
                    player.hideMerman = true;
                    player.skyStoneEffects = true;
                    player.noKnockback = true;
                    player.fireWalk = true;
                    player.buffImmune[30] = true;
                    player.buffImmune[36] = true;
                    player.buffImmune[31] = true;
                    player.buffImmune[23] = true;
                    player.buffImmune[22] = true;
                    player.buffImmune[20] = true;
                    player.buffImmune[35] = true;
                    player.buffImmune[32] = true;
                    player.buffImmune[33] = true;
                    player.buffImmune[46] = true;
                    player.statLifeMax2 += LifeBonus;
                    player.lavaImmune = true;
                    player.buffImmune[24] = true;
                    player.buffImmune[47] = true;
                    player.buffImmune[ModContent.BuffType<FlariumInfernoDebuff>()] = true;
                    if ((float)player.statLife > (float)player.statLifeMax2 * 0.25f)
                    {
                        player.hasPaladinShield = true;
                    }

                    player.buffImmune[156] = true;
                    player.GetModPlayer<MiscEffectsPlayer>().reflectionShield = true;
                }
            }
        }
        public class RampartOfDeitiesEffect : ReflectionShieldEffect
        {
            public override void PostUpdateEquips(Player player)
            {
                CalamityPlayer calamityPlayer = player.Calamity();
                player.longInvince = true;
                calamityPlayer.dAmulet = true;
                calamityPlayer.rampartOfDeities = true;
                player.noKnockback = true;
            }
        }
        
    }
}

