using FargowiltasSouls.Content.Items.Accessories.Essences;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BossViscount;
using ThoriumMod.Items.DD;
using ThoriumMod.Items.HealerItems;

namespace gcsep.Thorium.Essences
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class HealerEssence : BaseEssence
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            HealEffect(player);
        }

        private void HealEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.graveGoods = true;

            player.GetDamage<HealerDamage>() += 0.18f;
            player.GetCritChance<HealerDamage>() += 0.10f;
            player.GetAttackSpeed<HealerDamage>() += 0.10f;

            //ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //thoriumPlayer.radiantBoost += 0.18f;
            //thoriumPlayer.radiantSpeed -= 0.05f;
            //thoriumPlayer.healingSpeed += 0.05f;
            //thoriumPlayer.radiantCrit += 5;
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<ClericEmblem>();

            recipe.AddIngredient<HeartWand>();
            recipe.AddIngredient<LargePopcorn>();
            recipe.AddIngredient<DarkMageStaff>();
            recipe.AddIngredient<BatScythe>();
            recipe.AddIngredient<DivineLotus>();
            recipe.AddIngredient<DefenderWand>();
            recipe.AddIngredient<LifeDisperser>();

            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}
