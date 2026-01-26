using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Buffs.Minions;
using gcsep.Content.SoulToggles;
using System.Buffers.Text;
using System.Drawing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Items.Accessories
{
    public class EternityForce : BaseForce
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[this.Type] = true;
        }
        public override void SetDefaults()
        {
            this.Item.value = Item.buyPrice(10, 0, 0, 0);
            this.Item.rare = 10;
            this.Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<MutantSoulEffect>(Item))
            {
                player.AddBuff(ModContent.BuffType<MutantSoulBuff>(), 2);
            }

            // Directly call your own enchantments
            ModContent.GetInstance<StyxEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<PhantaplazmalEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<NekomiEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<EridanusEnchant>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<GaiaEnchant>().UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe(1);
            recipe.AddIngredient<GaiaEnchant>(1);
            recipe.AddIngredient<EridanusEnchant>(1);
            recipe.AddIngredient<StyxEnchant>(1);
            recipe.AddIngredient<PhantaplazmalEnchant>(1);
            recipe.AddIngredient<NekomiEnchant>(1);
            recipe.AddIngredient<EternalEnergy>(15);
            recipe.AddIngredient<AbomEnergy>(15);
            recipe.AddIngredient<DeviatingEnergy>(15);
            recipe.AddIngredient<Eridanium>(15);
            recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
            recipe.Register();
        }

        public abstract class EternityForceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
        }

        public class MutantSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<EternityForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<EternityForce>();
            public override bool MinionEffect => true;
        }
    }
}
