using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BossBuriedChampion;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class FlightEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoSoulsPlayer fargoPlayer = player.GetModPlayer<FargoSoulsPlayer>();
            fargoPlayer.WingTimeModifier += 1f;
            if (player.AddEffect<FabergeEffect>(Item))
            {
                ModContent.GetInstance<FabergeEgg>().UpdateAccessory(player, hideVisual);
            }
        }
        public class FabergeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FlightMasteryHeader>();
            public override int ToggleItemType => ModContent.ItemType<FlightEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FlightMask>());
            recipe.AddIngredient(ModContent.ItemType<FlightMail>());
            recipe.AddIngredient(ModContent.ItemType<FlightBoots>());
            recipe.AddIngredient(ModContent.ItemType<ChampionWing>());
            recipe.AddIngredient(ModContent.ItemType<FabergeEgg>());
            recipe.AddIngredient(ModContent.ItemType<Bolas>(), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
