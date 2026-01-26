using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gunrightsmod.Content.Items;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gunrightsmod.Content.Items.Armor;

namespace gcsep.gunrightsmod.Enchantments
{
    [ExtendsFromMod(ModCompatibility.gunrightsmod.Name)]
    [JITWhenModsEnabled(ModCompatibility.gunrightsmod.Name)]
    public class KevlarEnchant : BaseEnchant
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
            Item.value = 193331;
        }

        public override Color nameColor => new(94, 48, 117);

        public class KevlarEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<IdeocracyForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<KevlarEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<KevlarBeret>().UpdateArmorSet(player);
                ModContent.GetInstance<KevlarFedora>().UpdateArmorSet(player);
                ModContent.GetInstance<KevlarHelmet>().UpdateArmorSet(player);
                ModContent.GetInstance<KevlarMask>().UpdateArmorSet(player);
                ModContent.GetInstance<KevlarVisor>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<KevlarBodysuit>();
            recipe.AddIngredient<KevlarPants>();
            recipe.AddIngredient<KevlarWhip>();
            recipe.AddIngredient<BallisticKnife>();
            recipe.AddIngredient<DeadSoldiersRifle>();
            recipe.AddRecipeGroup("gcsep:KevlarHelms");
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}