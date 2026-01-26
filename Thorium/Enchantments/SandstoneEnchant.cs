using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossTheGrandThunderBird;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Items.ThrownItems;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SandstoneEnchant : BaseEnchant
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
            player.AddEffect<SandstoneEffect>(Item);
        }

        public class SandstoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MuspelheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SandstoneEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<SandStoneHelmet>().UpdateArmorSet(player);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SandStoneHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SandStoneMail>());
            recipe.AddIngredient(ModContent.ItemType<SandStoneGreaves>());
            recipe.AddIngredient(ModContent.ItemType<StoneThrowingSpear>(), 300);
            recipe.AddIngredient(ModContent.ItemType<Scorpain>());
            recipe.AddIngredient(ModContent.ItemType<TalonBurst>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
