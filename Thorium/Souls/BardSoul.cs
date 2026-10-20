using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;

namespace gcsep.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BardSoul : BaseSoul
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
            Item.value = 1000000;
            Item.rare = 11;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Thorium(player);
        }

        private void Thorium(Player player)
        {
            //general
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            player.GetDamage<BardDamage>() += 0.22f;
            player.GetCritChance<BardDamage>() += 0.10f;
            player.GetAttackSpeed<BardDamage>() += 0.15f;
            player.GetModPlayer<ThoriumPlayer>().bardBuffDuration += 3000;
            player.GetModPlayer<ThoriumPlayer>().inspirationRegenBonus += 1;
            player.GetModPlayer<ThoriumPlayer>().bardResourceDropBoost += 1;
            player.GetModPlayer<ThoriumPlayer>().bardResource += 20;
            player.GetModPlayer<ThoriumPlayer>().bardHomingSpeedBonus += 10;
            player.GetModPlayer<ThoriumPlayer>().bardHomingRangeBonus += 10;

            //epic mouthpiece
            ModContent.GetInstance<EpicMouthpiece>().UpdateAccessory(player, true);
            //straight mute
            ModContent.GetInstance<StraightMute>().UpdateAccessory(player, true);
            //digital tuner
            ModContent.GetInstance<DigitalTuner>().UpdateAccessory(player, true);
            //guitar pick claw
            ModContent.GetInstance<GuitarPickClaw>().UpdateAccessory(player, true);
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(null, "BardEssence");
            recipe.AddIngredient<AbomEnergy>(10);

            recipe.AddIngredient<HauntingBassDrum>();
            recipe.AddIngredient<HellBell>();
            recipe.AddIngredient<JingleBells>();
            recipe.AddIngredient<CallofCthulhu>();
            recipe.AddIngredient<TerrariumSurroundSound>();
            recipe.AddIngredient<TerrariumAutoharp>();
            recipe.AddIngredient<DigitalTuner>();
            recipe.AddIngredient<EpicMouthpiece>();
            recipe.AddIngredient<GuitarPickClaw>();
            recipe.AddIngredient<StraightMute>();
            recipe.AddIngredient<BandKit>();
            recipe.AddIngredient<Fishbone>();
            recipe.AddIngredient<SonicAmplifier>();

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}
