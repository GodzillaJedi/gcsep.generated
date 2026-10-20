using BombusApisBee.Content.Dungeon.Items.Skelecomb;
using BombusApisBee.Content.Forest.Items.Beemerang;
using BombusApisBee.Content.Forest.Items.HoneyedEmblem;
using BombusApisBee.Content.Forest.Items.PetrifiedHoneycomb;
using BombusApisBee.Content.Forest.Items.TraitorSaxophone;
using BombusApisBee.Content.Jungle.Items.Ambrosia;
using BombusApisBee.Content.Jungle.Items.Beemstick;
using BombusApisBee.Content.Jungle.Items.NectarBolt;
using BombusApisBee.Core.BeekeeperClass;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.ClassSouls.Beekeeper.Essences
{
    [ExtendsFromMod(ModCompatibility.BeekeeperClass.Name)]
    [JITWhenModsEnabled(ModCompatibility.BeekeeperClass.Name)]
    public class BeekeeperEssence : BaseEssence
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Beekeeper;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 4;
            Item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<BeekeeperDamageClass>() += 0.18f;
            player.GetCritChance<BeekeeperDamageClass>() += 0.10f;
            player.GetModPlayer<BeekeeperPlayer>().BeeResourceMax2 += 50;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<HoneyedEmblem>();

            recipe.AddIngredient<Beemerang>();
            recipe.AddIngredient<Ambrosia>();
            recipe.AddIngredient<Beemstick>();
            recipe.AddIngredient<StoneHoneycomb>();
            recipe.AddIngredient<TheTraitorsSaxophone>();
            recipe.AddIngredient<Skelecomb>();
            recipe.AddIngredient<NectarBolt>();

            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
    }
}
