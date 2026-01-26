using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Darksteel;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.SteelEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DarksteelEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DarksteelEffect>(Item);
            if (player.AddEffect<BallnChainEffect>(Item))
            {
                ModContent.GetInstance<BallnChain>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<ThoriumEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class DarksteelEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DarksteelEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DarksteelFaceGuard>().UpdateArmorSet(player);
            }
        }
        public class BallnChainEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DarksteelEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DarksteelFaceGuard>());
            recipe.AddIngredient(ModContent.ItemType<DarksteelBreastPlate>());
            recipe.AddIngredient(ModContent.ItemType<DarksteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SteelEnchant>());
            recipe.AddIngredient(ModContent.ItemType<BallnChain>());
            recipe.AddIngredient(ModContent.ItemType<StrongestLink>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
