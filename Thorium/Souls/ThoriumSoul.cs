using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.UI;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using gcsep.Thorium.Forces;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Buffs.Bard;

namespace gcsep.Thorium.Souls
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumSoul : BaseSoul
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 61));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.value = 5000000;
            Item.defense = 15;
            Item.rare = -12;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CSEThoriumPlayer thoriumPlayer = player.GetModPlayer<CSEThoriumPlayer>();
            thoriumPlayer.ThoriumSoul = true;

            player.ClearBuff(ModContent.BuffType<MetronomeDebuff>());

            // Forces
            ModContent.GetInstance<MuspelheimForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<JotunheimForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AlfheimForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<NiflheimForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<SvartalfheimForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<MidgardForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<VanaheimForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<HelheimForce>().UpdateAccessory(player, hideVisual);
            ModContent.GetInstance<AsgardForce>().UpdateAccessory(player, hideVisual);

            // MotDE
            ModContent.GetInstance<MotDE>().UpdateAccessory(player, hideVisual);
        }
        public class ThoriumSoulEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ThoriumSoulHeader>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<AbomEnergy>(10);
            recipe.AddIngredient(null, "MuspelheimForce");
            recipe.AddIngredient(null, "JotunheimForce");
            recipe.AddIngredient(null, "AlfheimForce");
            recipe.AddIngredient(null, "NiflheimForce");
            recipe.AddIngredient(null, "SvartalfheimForce");
            recipe.AddIngredient(null, "MidgardForce");
            recipe.AddIngredient(null, "VanaheimForce");
            recipe.AddIngredient(null, "HelheimForce");
            recipe.AddIngredient(null, "AsgardForce");
            recipe.AddIngredient(null, "MotDE");

            recipe.AddTile<CrucibleCosmosSheet>();

            recipe.Register();
        }
    }
}