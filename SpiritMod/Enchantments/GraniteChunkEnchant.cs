using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Accessory.ShurikenLauncher;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.FrigidSet.FrigidArmor;
using SpiritMod.Items.Sets.GraniteSet.GraniteArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SpiritMod.Enchantments.FrigidEnchantSp;

namespace gcsep.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class GraniteChunkEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(116, 112, 169);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<ShurikenEffect>(Item))
            {
                ModContent.GetInstance<ShurikenLauncher>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<GraniteBoots>(Item))
            {
                ModContent.GetInstance<TechBoots>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<GraniteChunkStomp>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<GraniteHelm>(1);
            recipe.AddIngredient<GraniteChest>(1);
            recipe.AddIngredient<GraniteLegs>(1);
            recipe.AddIngredient<ShurikenLauncher>(1);
            recipe.AddIngredient<RageBlazeDecapitator>(1);
            recipe.AddIngredient<TechBoots>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class GraniteChunkStomp : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteChunkEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GraniteHelm>().UpdateArmorSet(player);
            }
        }
        public class GraniteBoots : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteChunkEnchant>();
        }
        public class ShurikenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteChunkEnchant>();
        }
    }
}
