using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Gambler.Armors.Outlaw;
using SacredTools;
using SacredTools.Content.Items.Armor.Lunar.Vortex;
using SacredTools.Content.Items.Weapons.Oblivion;
using SacredTools.Items.Weapons.Lunatic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class CosmicCommanderEnchant : BaseEnchant
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
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(21, 142, 100);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CosmicCommanderEffect>(Item);
        }

        public class CosmicCommanderEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CosmicCommanderEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VortexCommanderHat>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VortexCommanderHat>();
            recipe.AddIngredient<VortexCommanderSuit>();
            recipe.AddIngredient<VortexCommanderGreaves>();
            recipe.AddIngredient<DolphinGun>();
            recipe.AddIngredient<LightningRifle>();
            recipe.AddIngredient<PGMUltimaRatioHecateII>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
