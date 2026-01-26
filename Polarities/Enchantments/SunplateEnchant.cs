using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Polarities;
using Polarities.Content.Items.Accessories.Combat.Offense.PreHardmode;
using Polarities.Content.Items.Accessories.ExpertMode.PreHardmode;
using Polarities.Content.Items.Armor.Classless.PreHardmode.SunplateArmor;
using Polarities.Content.Items.Weapons.Summon.Minions.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Polarities.Enchantments.StormcloudEnchant;

namespace gcsep.Polarities.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Polarities.Name)]
    [JITWhenModsEnabled(ModCompatibility.Polarities.Name)]
    public class SunplateEnchant : BaseEnchant
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
            Item.value = 40000;
        }

        public override Color nameColor => new(0, 72, 128);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SunplateEffect>(Item);
            if (player.AddEffect<CosmicCableEffect>(Item))
            {
                ModContent.GetInstance<CosmicCable>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<StarCuffEffect>(Item))
            {
                ModContent.GetInstance<StarbindCuffs>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SunplateMask>());
            recipe.AddIngredient(ModContent.ItemType<SunplateArmor>());
            recipe.AddIngredient(ModContent.ItemType<SunplateBoots>());
            recipe.AddIngredient(ModContent.ItemType<StarbindCuffs>());
            recipe.AddIngredient(ModContent.ItemType<CosmicCable>());
            recipe.AddIngredient(ModContent.ItemType<WingedStarStaff>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class SunplateEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SunplateEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SunplateMask>().UpdateArmorSet(player);
            }
        }
        public class CosmicCableEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SunplateEnchant>();
        }
        public class StarCuffEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<WildernessForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SunplateEnchant>();
        }
    }
}
