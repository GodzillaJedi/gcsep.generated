using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Items.Consumables
{
    public class StarblightFruit : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.consumable = true;
            Item.UseSound = SoundID.Item123;
            Item.value = Item.sellPrice(0, 15);
        }

        public override bool CanUseItem(Player player)
        {
            return !player.CSE().starlightFruit;
        }

        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                player.CSE().starlightFruit = true;
            }
            return true;
        }

        public override Color? GetAlpha(Color lightColor) => Color.Red;
    }
}