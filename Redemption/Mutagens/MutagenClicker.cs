using ClickerClass;
using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Redemption.Mutagens
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name, ModCompatibility.ClickerClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name, ModCompatibility.ClickerClass.Name)]
    public class MutagenClicker : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            base.Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.Item.width = 28;
            base.Item.height = 36;
            base.Item.value = Item.sellPrice(0, 12);
            base.Item.rare = 11;
            base.Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<ClickerDamage>() += 0.20f;
            player.GetCritChance<ClickerDamage>() += 0.10f;
            player.GetModPlayer<ClickerPlayer>().clickerRadius += 1;
        }

        //public override void AddRecipes()
        //{
        //    CreateRecipe().AddIngredient(ModContent.ItemType<MiceFragment>(), 10).AddIngredient(ModContent.ItemType<EmptyMutagen>()).AddIngredient(ModContent.ItemType<ClickerEssence>())
        //        .AddTile(412)
        //        .Register();
        //}
    }
}
