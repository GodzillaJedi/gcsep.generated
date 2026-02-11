using FargowiltasSouls.Content.Items.Materials;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gcsep.Content.Items.Consumables;
using Terraria;
using Terraria.ModLoader;
using gcsep.CrossMod.CraftingStations;

namespace gcsep.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueMonstrositySuit : ModItem
    {
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(10, 0, 0, 0);
            this.Item.defense = 200;
        }

        public override void UpdateEquip(Player player)
        {
            // Flat bonuses (safe)
            player.GetDamage(DamageClass.Generic) += 1.5f;   // +150% damage
            player.GetCritChance(DamageClass.Generic) += 2f; // +2% crit

            // Max HP and Mana (safe)
            player.statLifeMax2 += 1000;
            player.statManaMax2 += 1000;

            // Endurance must be clamped
            player.endurance += 1f;
            if (player.endurance > 0.95f)
                player.endurance = 0.95f; // Terraria caps at 95%

            // Life regen (safe)
            player.lifeRegen += 10;
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if ((line.Mod == "Terraria" && line.Name == "ItemName") || line.Name == "FlavorText")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.Text");
                shader.TrySetParameter("mainColor", new Color(42, 66, 99));
                shader.TrySetParameter("secondaryColor", Main.DiscoColor);
                shader.Apply("PulseUpwards");
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<EternalEnergy>(15);
            recipe.AddIngredient<Sadism>(15);
            recipe.AddIngredient<MonstrositySuit>();

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
