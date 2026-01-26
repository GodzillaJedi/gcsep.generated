using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Accessories.Combat.Defense.Hardmode;
using Polarities.Content.Items.Accessories.Combat.Offense.PreHardmode;
using Polarities.Content.Items.Armor.Classless.Hardmode.LimestoneArmor;
using Polarities.Content.Items.Armor.Flawless.MechaMayhemArmor;
using Polarities.Content.Items.Weapons.Melee.Yoyos.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class MechanicalEnchant : BaseEnchant
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
            player.AddEffect<MechanicalEffect>(Item);
            ModContent.GetInstance<LimestoneEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FlawlessMechMask>());
            recipe.AddIngredient(ModContent.ItemType<FlawlessMechChestplate>());
            recipe.AddIngredient(ModContent.ItemType<FlawlessMechTail>());
            recipe.AddIngredient(ModContent.ItemType<LimestoneEnchant>());
            recipe.AddIngredient(ModContent.ItemType<GlowYo>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class MechanicalEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MechanicalEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FlawlessMechMask>().UpdateArmorSet(player);
            }
        }
    }
}
