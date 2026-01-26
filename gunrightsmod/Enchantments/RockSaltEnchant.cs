using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gunrightsmod.Content.Items;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gunrightsmod.Content.Items.Tools;
using gunrightsmod.Content.Items.Armor;

namespace gcsep.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class RockSaltEnchant : BaseEnchant
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
            Item.rare = 5;
            Item.value = 69199;
        }

        public override Color nameColor => new(94, 48, 117);

        public class RockSaltEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<IdeocracyForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RockSaltEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<RockSaltFedora>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RockSaltFedora>();
            recipe.AddIngredient<RockSaltChestplate>();
            recipe.AddIngredient<RockSaltLeggings>();
            recipe.AddIngredient<Naclslash>();
            recipe.AddIngredient<SodiumHamaxe>();
            recipe.AddIngredient<TheSaltLick>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}