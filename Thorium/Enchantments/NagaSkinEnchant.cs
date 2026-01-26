using Fargowiltas;
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
using ThoriumMod.Items.BossForgottenOne;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class NagaSkinEnchant : BaseEnchant
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
            Item.rare = 5;
            Item.value = 150000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<NagaEffect>(Item);
            if (player.AddEffect<OceanRetaliationEffect>(Item))
            {
                ModContent.GetInstance<OceanRetaliation>().UpdateAccessory(player, hideVisual);
            }
        }
        public class NagaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NagaSkinEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<NagaSkinMask>().UpdateArmorSet(player);
            }
        }
        public class OceanRetaliationEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NagaSkinEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {


            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<NagaSkinMask>());
            recipe.AddIngredient(ModContent.ItemType<NagaSkinSuit>());
            recipe.AddIngredient(ModContent.ItemType<NagaSkinTail>());
            recipe.AddIngredient(ModContent.ItemType<OceanRetaliation>());
            recipe.AddIngredient(ModContent.ItemType<Eelrod>());
            recipe.AddIngredient(ModContent.ItemType<OldGodsVision>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
