using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Buffs.Healer;
using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Valadium;
using ThoriumMod.NPCs;
using ThoriumMod.Projectiles.Healer;
using ThoriumMod.Utilities;
using ThoriumRework;
using static gcsep.Thorium.Enchantments.EbonEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class WarlockEnchant : BaseEnchant
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
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<WarlockEffect>(Item))
            {
                player.GetThoriumPlayer().warlockSet = true;
            }
            if (player.AddEffect<DemonTongueEffect>(Item))
            {
                ModContent.GetInstance<DemonTongue>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<DarkEffigyEffect>(Item))
            {
                ModContent.GetInstance<DarkEffigy>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<EbonEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class WarlockEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WarlockEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class DemonTongueEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WarlockEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class DarkEffigyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<WarlockEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<WarlockHood>());
            recipe.AddIngredient(ModContent.ItemType<WarlockGarb>());
            recipe.AddIngredient(ModContent.ItemType<WarlockLeggings>());
            recipe.AddIngredient(ModContent.ItemType<EbonEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DemonTongue>());
            recipe.AddIngredient(ModContent.ItemType<DarkEffigy>());


            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
