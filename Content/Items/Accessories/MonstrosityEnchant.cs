using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gcsep.Content.Items.Armor;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Content.Items.Materials;

namespace gcsep.Content.Items.Accessories
{
    public class MonstrosityEnchant : BaseEnchant
    {
        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[this.Type] = true;
        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
        }
        public override Color nameColor => new(200, 20, 250);

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod == "Terraria" && line.Name == "ItemName")
            {
                GameShaders.Armor.GetShaderFromItemId(3562).Apply(Item, null);
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White);
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<GCSEPlayer>().equippedMonstrosityEnchantment = true;

            if (player.AddEffect<MonstrosityEffect>(Item))
            {
                // Correct way: call a custom set bonus method
                ModContent.GetInstance<TrueMonstrosityMask>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<EternalEnergy>(50);
            recipe.AddIngredient<MonstrosityMask>();
            recipe.AddIngredient<MonstrositySuit>();
            recipe.AddIngredient<MonstrosityPants>();
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }

        public class MonstrosityEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ShadowsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MonstrosityEnchant>();
        }
    }
}
