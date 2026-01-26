using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Spooky.Content.Items.SpookyHell;
using Spooky.Content.Items.SpookyHell.Armor;
using Spooky.Content.Items.SpookyHell.EggEvent;
using Spooky.Core;
using SpookyBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Spooky.Enchantments.GoreEnchant;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class LivingFleshEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Spooky;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<EyeArmorEffect>(Item);
            if (player.AddEffect<BloodVialEffect>(Item))
            {
                ModContent.GetInstance<MonsterBloodVial>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<EyeHead>();
            recipe.AddIngredient<EyeBody>();
            recipe.AddIngredient<EyeLegs>();
            if (ModCompatibility.SpookyBardHealer.Loaded)
            {
                recipe.AddIngredient<SneezePriestNose>();
                recipe.AddIngredient<SneezePriestRobe>();
                recipe.AddIngredient<SneezePriestGreaves>();
            }
            recipe.AddIngredient<MonsterBloodVial>();
            recipe.AddIngredient<LivingFleshAxe>();
            recipe.AddIngredient<LivingFleshWhip>();

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class EyeArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingFleshEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GoreHoodEye>().UpdateArmorSet(player);
                if (ModCompatibility.SpookyBardHealer.Loaded)
                {
                    ModContent.GetInstance<SneezePriestNose>().UpdateArmorSet(player);
                }
            }
        }
        public class BloodVialEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingFleshEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
    }
}
