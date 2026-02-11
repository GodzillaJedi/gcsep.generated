using Fargowiltas.Items.Vanity;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gcsep.Content.Items.Accessories;
using gcsep.Content.Items.Consumables;
using Terraria;
using Terraria.ModLoader;
using gcsep.CrossMod.CraftingStations;

namespace gcsep.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueLumberjackBody : AntiCheatItem
    {
        public override void SetDefaults()
        {
            ((Entity)this.Item).width = 18;
            ((Entity)this.Item).height = 18;
            this.Item.rare = 11;
            this.Item.expert = true;
            this.Item.value = Item.sellPrice(100, 0, 0, 0);
            this.Item.defense = int.MaxValue / 1000;
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
        public override void UpdateEquip(Player player)
        {
            // Effectively infinite damage
            player.GetDamage(DamageClass.Generic) += 1000f;   // +100000% damage

            // Guaranteed crits
            player.GetCritChance(DamageClass.Generic) += 100f; // +100% crit

            // Massive but safe max HP/mana
            player.statLifeMax2 += 100000;
            player.statManaMax2 += 100000;

            // DR capped at Terraria’s safe maximum
            player.endurance = 0.95f;

            // Infinite regen (safe version)
            player.lifeRegen += 9999;

            // Always full HP
            player.statLife = player.statLifeMax2;

            // Always full mana
            player.statMana = player.statManaMax2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<LumberjackBody>();

            recipe.AddIngredient<Sadism>(100);
            recipe.AddIngredient<StargateSoul>(4);
            //recipe.AddIngredient<ShardOfStarlight>(30);

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
