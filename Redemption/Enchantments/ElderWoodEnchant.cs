using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Redemption.BaseExtension;
using Redemption.Globals.Players;
using Redemption.Items.Armor.PreHM.ElderWood;
using Redemption.Items.Placeable.Furniture.Misc;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Redemption.Forces.AdvancementForce;

namespace gcsep.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class ElderWoodEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Redemption;
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

        public override Color nameColor => new Color(206, 182, 95);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<ElderWoodEffect>(Item);
            player.AddEffect<ElderWoodHelmEffect>(Item);
        }

        public class ElderWoodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElderWoodEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                BuffPlayer buffPlayer = player.RedemptionPlayerBuff();
                float toAdd = player.HasEffect<AdvancementEffect>() ? 1 : 0.15f;
                buffPlayer.ElementalResistance[3] += toAdd;
                buffPlayer.ElementalResistance[1] += toAdd;
                buffPlayer.ElementalResistance[2] += toAdd;
                buffPlayer.ElementalResistance[4] += toAdd;
                buffPlayer.ElementalResistance[5] += toAdd;
                buffPlayer.ElementalResistance[6] += toAdd;
                buffPlayer.ElementalResistance[7] += toAdd;
                buffPlayer.ElementalResistance[8] += toAdd;
                buffPlayer.ElementalResistance[9] += toAdd;
                buffPlayer.ElementalResistance[10] += toAdd;
                buffPlayer.ElementalResistance[11] += toAdd;
                buffPlayer.ElementalResistance[12] += toAdd;
                buffPlayer.ElementalResistance[13] += toAdd;
                buffPlayer.ElementalResistance[14] += toAdd;
                buffPlayer.ElementalResistance[0] += toAdd;
               // buffPlayer.ElementalResistance[15] += toAdd;
            }
        }
        public class ElderWoodHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ElderWoodEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<ElderWoodHelmet>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<ElderWoodHelmet>();
            recipe.AddIngredient<ElderWoodBreastplate>();
            recipe.AddIngredient<ElderWoodGreaves>();
            recipe.AddIngredient<ElderWoodSword>();
            recipe.AddIngredient<ElderWoodStaff>();
            recipe.AddIngredient<GathicCryoFurnace>();
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
