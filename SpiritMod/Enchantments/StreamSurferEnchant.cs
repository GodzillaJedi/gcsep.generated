using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.SpiritSet.SpiritArmor;
using SpiritMod.Items.Sets.TideDrops.StreamSurfer;
using SpiritMod.Items.Sets.TideDrops.Whirltide;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.SpiritEnchant;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class StreamSurferEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(30, 142, 185);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<StreamSurferFishJump>(Item))
            {
                ModContent.GetInstance<Flying_Fish_Fin>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<StreamSurferSpout>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<StreamSurferHelmet>();
            recipe.AddIngredient<StreamSurferChestplate>();
            recipe.AddIngredient<StreamSurferLeggings>();
            recipe.AddIngredient<Whirltide>();
            recipe.AddIngredient<BassSlapper>();
            recipe.AddIngredient<Flying_Fish_Fin>();
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class StreamSurferFishJump : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StreamSurferEnchant>();
        }
        public class StreamSurferSpout : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<StreamSurferEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<StreamSurferHelmet>().UpdateArmorSet(player);
            }
        }
    }
}
