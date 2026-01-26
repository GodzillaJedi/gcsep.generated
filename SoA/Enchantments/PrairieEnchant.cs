using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools.Content.Items.Armor.Lunar.Nebula;
using SacredTools.Content.Items.Armor.Prairie;
using SacredTools.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class PrairieEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 50000;
        }

        public override Color nameColor => new(129, 19, 29);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PrairieHelmEffect>(Item);
            player.AddEffect<PrairieEffect>(Item);
        }
        public class PrairieEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrairieEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                float windSpeed = Main.windSpeedCurrent;

                float windSpeedAbs = System.Math.Abs(windSpeed);

                float minbonus = player.ForceEffect<PrairieEffect>() ? 1.1f : 1.05f;
                float maxbonus = player.ForceEffect<PrairieEffect>() ? 1.2f : 1.1f;

                if (windSpeedAbs > 0.5f)
                {
                    float bonusMultiplier = (player.ForceEffect<PrairieEffect>() ? 1.1f : 1.1f) + (windSpeedAbs - 0.5f) * 0.06f;

                    if (bonusMultiplier > maxbonus)
                        bonusMultiplier = maxbonus;


                    player.moveSpeed *= bonusMultiplier;
                    player.maxRunSpeed *= bonusMultiplier;
                    player.runAcceleration *= bonusMultiplier;
                    player.jumpSpeedBoost *= bonusMultiplier;
                }
            }
        }
        public class PrairieHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FoundationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrairieEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PrairieHelmet>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PrairieHelmet>();
            recipe.AddIngredient<PrairieChest>();
            recipe.AddIngredient<PrairieLegs>();
            recipe.AddIngredient<WoodJavelin>(100);
            recipe.AddIngredient<GoldJavelin>(100);
            recipe.AddIngredient<PlatinumJavelin>(100);
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
