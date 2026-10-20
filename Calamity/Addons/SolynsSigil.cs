using CalamityMod;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using gcsep.CrossMod.CraftingStations;
using Microsoft.Xna.Framework;
using NoxusBoss.Content.Items.Accessories.VanityEffects;
using NoxusBoss.Content.Items.Accessories.Wings;
using NoxusBoss.Content.Rarities;
using NoxusBoss.Core.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Recipe recipe = CreateRecipe();

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
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<SkirtEffect>())
                {
                    player.GetValueRef<bool>("WearingPortalSkirt").Value = false;
                }
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
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<DeificEffect>())
                {
                    player.GetValueRef<bool>("DeificTouch").Value = false;
                }
            }
        }
        public class DivineEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SolynsSigilHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineWings>();
            public override void PostUpdateEquips(Player player)
            {
                player.wingsLogic = DivineWings.WingSlotID;

                player.noFallDmg = true;

                if (ModLoader.HasMod("CalamityMod"))
                {
                    player.Calamity().infiniteFlight = true;
                }

                Lighting.AddLight(player.Center, Vector3.One);

                player.wingTimeMax = 100000000; 

                if (player.wingTime > 0f && player.controlJump)
                {
                    if (player.velocity.Y > 0f)
                        player.velocity.Y -= 2f * player.gravDir; 
                }
            }
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<DivineEffect>())
                {
                    if (player.wingsLogic == DivineWings.WingSlotID)
                    {
                        player.wingsLogic = 0;
                    }
                }
            }
        }
    }
}
