using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities.Content.Items.Accessories.Combat.Offense.Hardmode;
using Polarities.Content.Items.Accessories.Movement.PreHardmode;
using Polarities.Content.Items.Accessories.Wings;
using Polarities.Content.Items.Weapons.Ranged.Guns.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Polarities.Enchantments.HaliteEnchant;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class HighTechEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
          return GCSEConfig.Instance.Polarities;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(156, 156, 156);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<HighTechEffect>(Item))
            {
                ModContent.GetInstance<LightningCore>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ElectricWings>());
            recipe.AddIngredient(ModContent.ItemType<LightningCore>());
            recipe.AddIngredient(ModContent.ItemType<Railgun>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class HighTechEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<HighTechEnchant>();
        }
    }
}
