using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using NoxusBoss.Content.Items.Accessories.VanityEffects;
using NoxusBoss.Content.Items.Accessories.Wings;
using NoxusBoss.Content.Rarities;
using NoxusBoss.Core.CrossCompatibility.Inbound.BaseCalamity;
using NoxusBoss.Core.Utilities;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using gcsep.CrossMod.CraftingStations;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.WrathoftheGods.Name)]
    [JITWhenModsEnabled(ModCompatibility.WrathoftheGods.Name)]
    public class SolynsSigil : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ModContent.RarityType<NamelessDeityRarity>();
            Item.value = 500000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<DeificEffect>(Item);
            player.AddEffect<SkirtEffect>(Item);
            player.AddEffect<DivineEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PortalSkirt>());
            recipe.AddIngredient(ModContent.ItemType<DeificTouch>());
            recipe.AddIngredient(ModContent.ItemType<DivineWings>());

            recipe.AddTile<DemonshadeWorkbenchTile>();
            recipe.Register();
        }

        public class SkirtEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SolynsSigilHeader>();
            public override int ToggleItemType => ModContent.ItemType<PortalSkirt>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetValueRef<bool>("WearingPortalSkirt").Value = true;
            }
        }

        public class DeificEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SolynsSigilHeader>();
            public override int ToggleItemType => ModContent.ItemType<DeificTouch>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetValueRef<bool>("DeificTouch").Value = true;
            }
        }
        public class DivineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SolynsSigilHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineWings>();

            public override void PostUpdateEquips(Player player)
            {
                player.noFallDmg = true;
                CalamityCompatibility.GrantInfiniteCalFlight(player);
                if (player.active && !player.dead)
                {
                    Lighting.AddLight(player.Center, Vector3.One);
                }
            }
        }
    }
}
