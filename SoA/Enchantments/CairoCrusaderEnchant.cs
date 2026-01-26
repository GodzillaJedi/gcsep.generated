using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Items.Armor.Blightbone;
using SacredTools.Content.Items.Armor.CairoCrusader;
using SacredTools.Items.Tools;
using SacredTools.Items.Weapons;
using SacredTools.Items.Weapons.Sand;
using SacredTools.Projectiles.Minions.EternalOasis;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class CairoCrusaderEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 100000;
        }

        public override Color nameColor => new(70, 10, 10);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CairoEffect>(Item);
        }

        public class CairoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CairoCrusaderEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CairoCrusaderTurban>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<CairoCrusaderTurban>();
            recipe.AddIngredient<CairoCrusaderRobe>();
            recipe.AddIngredient<CairoCrusaderFaulds>();
            recipe.AddIngredient<DesertStaff>();
            recipe.AddIngredient<SandstormMedallion>();
            recipe.AddIngredient<ElementalFlinger>();
            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
