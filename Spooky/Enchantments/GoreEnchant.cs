using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Spooky.Content.Items.BossBags.Accessory;
using Spooky.Content.Items.Catacomb.Armor;
using Spooky.Content.Items.SpookyHell;
using Spooky.Content.Items.SpookyHell.Armor;
using Spooky.Content.Items.SpookyHell.EggEvent;
using Spooky.Core;
using SpookyBardHealer.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Spooky.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class GoreEnchant : BaseEnchant
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
            player.AddEffect<OrroBoroEffect>(Item);
            if (player.AddEffect<OrgansEffect>(Item))
            {
                ModContent.GetInstance<TotalOrganPackage>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<OrroboroEmbryoEffect>(Item))
            {
                ModContent.GetInstance<OrroboroEmbryo>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddRecipeGroup("gcsep:AnyGoreHelmet");
            recipe.AddIngredient<GoreBody>();
            recipe.AddIngredient<GoreLegs>();
            recipe.AddIngredient<TotalOrganPackage>();
            recipe.AddIngredient<OrroboroEmbryo>();
            recipe.AddIngredient<EyeFlail>();

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class OrgansEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GoreEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
        public class OrroboroEmbryoEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GoreEnchant>();
            public override bool MinionEffect => true;
            public override bool MutantsPresenceAffects => true;
        }
        public class OrroBoroEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<TerrorForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GoreEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<GoreHoodEye>().UpdateArmorSet(player);
                ModContent.GetInstance<GoreHoodMouth>().UpdateArmorSet(player);
                if (ModCompatibility.SpookyBardHealer.Loaded)
                {
                    ModContent.GetInstance<GoreHandVisage>().UpdateArmorSet(player);
                    ModContent.GetInstance<GoreEarVisor>().UpdateArmorSet(player);
                    ModContent.GetInstance<GoreWitchHat>().UpdateArmorSet(player);
                }
            }
        }
    }
}
