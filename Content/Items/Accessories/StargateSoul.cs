using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gcsep.Content.Items.Consumables;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using gcsep.CrossMod.CraftingStations;
using gcsep.Content.Items.Materials;

namespace gcsep.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    [ExtendsFromMod("FargowiltasSouls")]
    [JITWhenModsEnabled("FargowiltasSouls")]
    public class StargateSoul : BaseSoul
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Type] = true;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 9));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 64;
            Item.value = int.MaxValue;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.defense = 200;
        }
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            player.wingsLogic = ArmorIDs.Wing.LongTrailRainbowWings;
            ascentWhenFalling = 0.85f;
            if (player.HasEffect<FlightMasteryGravity>())
                ascentWhenFalling *= 1.5f;
            ascentWhenRising = 0.25f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 1.75f;
            constantAscend = 0.135f;
            if (player.controlUp)
            {
                ascentWhenFalling *= 6f;
                ascentWhenRising *= 6f;
                constantAscend *= 6f;
            }
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 18f;
            acceleration = 0.75f;
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

        void PassiveEffect(Player player)
        {
            BionomicCluster.PassiveEffect(player, Item);
            AshWoodEnchant.PassiveEffect(player);

            player.AddEffect<AmmoCycleEffect>(Item);

            player.FargoSouls().WoodEnchantDiscount = true;

            //cell phone
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accDreamCatcher = true;
            player.accOreFinder = true;
            player.accStopwatch = true;
            player.accCritterGuide = true;
            player.accJarOfSouls = true;
            player.accThirdEye = true;
            player.accCalendar = true;
            player.accWeatherRadio = true;
        }
        public override void UpdateInventory(Player player) => PassiveEffect(player);
        public override void UpdateVanity(Player player) => PassiveEffect(player);
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Apply any passive effects first
            PassiveEffect(player);

            // Grant immunity to all debuffs, except Calamity's Rage/Adrenaline
            for (int index = 0; index < BuffLoader.BuffCount; ++index)
            {
                if (Main.debuff[index])
                {
                    player.buffImmune[index] = true;

                    if (ModLoader.TryGetMod("CalamityMod", out _))
                    {
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "RageMode").Type] = false;
                        player.buffImmune[ModContent.Find<ModBuff>("CalamityMod", "AdrenalineMode").Type] = false;
                    }
                }
            }

            // Apply accessory effects from your own mod items
            ModContent.GetInstance<EternityForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MacroverseSoul>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MicroverseSoul>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<CyclonicFin>().UpdateAccessory(player, hideVisual);

            // Apply SoulsMod effect if present
            if (ModCompatibility.SoulsMod.Loaded &&
                ModContent.TryFind<ModItem>(ModCompatibility.SoulsMod.Name, "EternitySoul", out var eternitySoul))
            {
                eternitySoul.UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);

            recipe.AddIngredient<EternityForce>();
            recipe.AddIngredient<EternitySoul>();
            recipe.AddIngredient<MacroverseSoul>();
            recipe.AddIngredient<MicroverseSoul>();
            recipe.AddIngredient<CyclonicFin>();

            recipe.AddIngredient<Sadism>(30);
            recipe.AddIngredient<tModLoadiumBar>(30); // check capitalization!

            recipe.AddTile<MutantsForgeTile>();
            recipe.Register();
        }
    }
}
