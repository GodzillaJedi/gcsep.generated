using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.SummonItems;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LifeBloomEnchant : BaseEnchant
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
            Item.rare = 7;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<JungleHeartEffect>(Item))
            {
                ModContent.GetInstance<HeartOfTheJungle>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<LifeBloomEffect>(Item);
            ModContent.GetInstance<LivingWoodEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class LifeBloomEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LifeBloomEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<LifeBloomMask>().UpdateArmorSet(player);
            }
        }
        public class JungleHeartEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LifeBloomEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LifeBloomMask>());
            recipe.AddIngredient(ModContent.ItemType<LifeBloomMail>());
            recipe.AddIngredient(ModContent.ItemType<LifeBloomLeggings>());
            recipe.AddIngredient(ModContent.ItemType<HeartOfTheJungle>());
            recipe.AddIngredient(ModContent.ItemType<LivingWoodEnchant>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
