using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Items.Accessories
{
    public class EridanusEnchant : BaseEnchant
    {
        private readonly Mod FargoSoul = Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls");

        public override void SetStaticDefaults() => ItemID.Sets.ItemNoGravity[this.Type] = true;

        public override Color nameColor => new(100, 40, 130);

        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(1, 0, 0, 0);
            this.Item.rare = ItemRarityID.Purple;
            this.Item.accessory = true;
        }
        /*public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
          if (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName"))
            return true;
          Main.spriteBatch.End();
          Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
          GameShaders.Armor.GetShaderFromItemId(2869).Apply((Entity) this.Item, new DrawData?());
          Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
          Main.spriteBatch.End();
          Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
          return false;
        }*/

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.FargoSoul.Name, "UniverseCore").UpdateAccessory(player, true);

            if (player.AddEffect<EridanusEffect>(Item))
            {
                ModContent.Find<ModItem>(this.FargoSoul.Name, "EridanusBattleplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.FargoSoul.Name, "EridanusHat").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.FargoSoul.Name, "EridanusLegwear").UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient(this.FargoSoul, "Eridanium", 50);
            recipe.AddIngredient(this.FargoSoul, "EridanusBattleplate", 1);
            recipe.AddIngredient<UniverseCore>(1);
            recipe.AddIngredient(this.FargoSoul, "EridanusHat", 1);
            recipe.AddIngredient(this.FargoSoul, "EridanusLegwear", 1);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }

        public class EridanusEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EridanusHat>();
        }
    }
}
