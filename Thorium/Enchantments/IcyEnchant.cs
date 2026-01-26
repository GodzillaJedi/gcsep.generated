using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs;
using ThoriumMod.Items.BossGraniteEnergyStorm;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Icy;
using ThoriumMod.Projectiles;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class IcyEnchant : BaseEnchant
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
            player.AddEffect<IcyEffect>(Item);
            if (player.AddEffect<BlizzardPouchEffect>(Item))
            {
                ModContent.GetInstance<BlizzardPouch>().UpdateAccessory(player, hideVisual);
            }
        }
        public class IcyEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IcyEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<IcyHeadgear>().UpdateArmorSet(player);
            }
        }
        public class BlizzardPouchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<JotunheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IcyEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<IcyHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<IcyMail>());
            recipe.AddIngredient(ModContent.ItemType<IcyGreaves>());
            recipe.AddIngredient(ModContent.ItemType<IceShaver>());
            recipe.AddIngredient(ModContent.ItemType<BlizzardPouch>());
            recipe.AddIngredient(ItemID.IceBoomerang);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
