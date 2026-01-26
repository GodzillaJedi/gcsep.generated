using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using gunrightsmod.Content.Items;
using gunrightsmod.Content.Items.Armor;
using Microsoft.Xna.Framework;
using OrchidMod.Content.Guardian.Armors.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class AstatineEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.TerMerica;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 11;
            Item.value = 3135864;
        }

        public override Color nameColor => new(94, 48, 117);

        public class AstatineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<RadioactiveForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AstatineEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<AstatineHelmet>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AstatineHelmet>();
            recipe.AddIngredient<AstatineBreastplate>();
            recipe.AddIngredient<AstatineGreaves>();
            recipe.AddIngredient<ATFsNightmare>();
            recipe.AddIngredient<PlasmaRifle3>();
            recipe.AddIngredient<FissileDart>(3396);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}


