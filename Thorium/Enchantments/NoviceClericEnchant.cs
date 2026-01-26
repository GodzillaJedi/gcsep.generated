using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class NoviceClericEnchant : BaseEnchant
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
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<NoviceClericEffect>(Item);
            if (player.AddEffect<NursePurseEffect>(Item))
            {
                ModContent.GetInstance<NursePurse>().UpdateAccessory(player, hideVisual);
            }
        }
        public class NoviceClericEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NoviceClericEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<NoviceClericCowl>().UpdateArmorSet(player);
            }
        }
        public class NursePurseEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AsgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NoviceClericEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<NoviceClericCowl>());
            recipe.AddIngredient(ModContent.ItemType<NoviceClericTabard>());
            recipe.AddIngredient(ModContent.ItemType<NoviceClericPants>());
            recipe.AddIngredient(ModContent.ItemType<NursePurse>());
            recipe.AddIngredient(ModContent.ItemType<PalmCross>());
            recipe.AddIngredient(ModContent.ItemType<Renew>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
