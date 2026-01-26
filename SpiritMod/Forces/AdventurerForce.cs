using FargowiltasSouls.Content.Items.Accessories.Forces;
using gcsep.Core;
using gcsep.SpiritMod.Enchantments;
using SpiritMod.Items.Accessory;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SpiritMod.Forces
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class AdventurerForce : BaseForce
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 600000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic).Flat += 10f;
            ModContent.GetInstance<ElderbarkEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<DriftwoodEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<BotanistEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<FloranEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<WayfarersEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SunflowerEnchant>().UpdateAccessory(player, hideVisual);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ElderbarkEnchant>();
            recipe.AddIngredient<DriftwoodEnchant>();
            recipe.AddIngredient<BotanistEnchant>();
            recipe.AddIngredient<FloranEnchant>();
            recipe.AddIngredient<WayfarersEnchant>();
            recipe.AddIngredient<SunflowerEnchant>();
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }
    }
}
