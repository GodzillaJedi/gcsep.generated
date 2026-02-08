using CalamityMod;
using CalamityMod.Rarities;
using Clamity;
using Clamity.Content.Biomes.FrozenHell.Items.FrozenArmor;
using Clamity.Content.Bosses.WoB.Drop;
using Clamity.Content.Items.Weapons.Melee.Swords;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class FrozenEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Purple;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FrozenEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<FrozenHellstoneVisor>());
            recipe.AddIngredient(ModContent.ItemType<FrozenHellstoneChestplate>());
            recipe.AddIngredient(ModContent.ItemType<FrozenHellstoneGreaves>());
            recipe.AddIngredient(ModContent.ItemType<FrozenVolcano>());
            recipe.AddIngredient(ModContent.ItemType<AMS>());
            recipe.AddIngredient(ModContent.ItemType<TheWOBbler>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
        public class FrozenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrozenEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage(ModContent.GetInstance<TrueMeleeDamageClass>()) += 0.2f;
                player.GetAttackSpeed<MeleeDamageClass>() += 0.2f;
                player.Clamity().inflicingMeleeFrostburn = true;
                player.Clamity().frozenParrying = true;
                player.buffImmune[44] = true;
                player.buffImmune[324] = true;
                player.buffImmune[47] = true;
                player.aggro += 400;
            }
        }
    }
}
