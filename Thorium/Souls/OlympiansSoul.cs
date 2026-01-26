using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using gcsep.Thorium.Essences;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.ThrownItems;

namespace gcsep.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class OlympiansSoul : BaseSoul
    {
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
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            player.GetDamage<ThrowingDamageClass>() += 0.22f;
            player.GetCritChance<ThrowingDamageClass>() += 10f;
            player.GetAttackSpeed<ThrowingDamageClass>() += 0.15f;
            player.CSE().throwerVelocity += 0.20f;
            player.GetModPlayer<ThoriumPlayer>().throwerExhaustionRegenBonus += 10;
            player.GetModPlayer<ThoriumPlayer>().throwGuide3 = true;
            if (player.AddEffect<ThiefsWalletEffect>(Item))
            {
                player.GetModPlayer<ThoriumPlayer>().accThiefsWallet = true;
            }
            player.GetModPlayer<ThoriumPlayer>().throwConsume = 0.5f;
        }
        public class ThiefsWalletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<ThiefsWallet>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            // Thorium items
            if (ModContent.TryFind<ModItem>("ThoriumMod", "SlingerEssence", out var slingerEssence))
                recipe.AddIngredient(slingerEssence);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "ThiefsWallet", out var wallet))
                recipe.AddIngredient(wallet);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "Wreath", out var wreath))
                recipe.AddIngredient(wreath);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "ThrowingGuideVolume3", out var guide))
                recipe.AddIngredient(guide);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "TidalWave", out var tidalWave))
                recipe.AddIngredient(tidalWave);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "AngelsEnd", out var angelsEnd))
                recipe.AddIngredient(angelsEnd);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "TerrariumRippleKnife", out var rippleKnife))
                recipe.AddIngredient(rippleKnife);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "DragonFang", out var dragonFang))
                recipe.AddIngredient(dragonFang);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "TerraKnife", out var terraKnife))
                recipe.AddIngredient(terraKnife);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "TrueCarnwennan", out var carnwennan))
                recipe.AddIngredient(carnwennan);

            if (ModContent.TryFind<ModItem>("ThoriumMod", "HellRoller", out var hellRoller))
                recipe.AddIngredient(hellRoller);

            // Fargo’s Souls tile
            if (ModContent.TryFind<ModTile>("Fargowiltas", "CrucibleCosmosSheet", out var cosmosTile))
                recipe.AddTile(cosmosTile);

            recipe.Register();
        }
    }
}