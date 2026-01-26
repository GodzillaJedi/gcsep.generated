using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Accessories.Combat.Defense.Hardmode;
using Polarities.Content.Items.Accessories.Combat.Offense.Hardmode;
using Polarities.Content.Items.Accessories.Combat.Offense.PreHardmode;
using Polarities.Content.Items.Armor.Classless.Hardmode.LimestoneArmor;
using Polarities.Content.Items.Armor.Classless.PreHardmode.HaliteArmor;
using Polarities.Content.Items.Weapons.Melee.Yoyos.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Polarities.Enchantments.HighTechEnchant;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class LimestoneEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Polarities;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(14, 43, 21);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<LimestoneEffect>(Item);
            if (player.AddEffect<RhyoliteEffect>(Item))
            {
                ModContent.GetInstance<RhyoliteShield>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CorrosivePolishEffect>(Item))
            {
                ModContent.GetInstance<CorrosivePolish>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LimestoneHelmet>());
            recipe.AddIngredient(ModContent.ItemType<LimestoneChestplate>());
            recipe.AddIngredient(ModContent.ItemType<LimestoneGreaves>());
            recipe.AddIngredient(ModContent.ItemType<RhyoliteShield>());
            recipe.AddIngredient(ModContent.ItemType<CorrosivePolish>());
            recipe.AddIngredient(ModContent.ItemType<GlowYo>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class LimestoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LimestoneEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LimestoneHelmet>().UpdateArmorSet(player);
            }
        }
        public class RhyoliteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LimestoneEnchant>();
        }
        public class CorrosivePolishEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LimestoneEnchant>();
        }
    }
}
