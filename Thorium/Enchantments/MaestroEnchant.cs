using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Buffs.Bard;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Projectiles.Bard;
using ThoriumMod.Sounds;
using ThoriumMod.Utilities;
using static gcsep.Thorium.Enchantments.MarchingBandEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class MaestroEnchant : BaseEnchant
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
            Item.rare = 8;
            Item.value = 200000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MaestroEffect>(Item);
            if (player.AddEffect<MetronomeEffect>(Item))
            {
                ModContent.GetInstance<Metronome>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<ConductorsEffect>(Item))
            {
                ModContent.GetInstance<ConductorsBaton>().UpdateAccessory(player, hideVisual);
            }
            ModContent.GetInstance<MarchingBandEnchant>().UpdateAccessory(player, hideVisual);
        }
        public class MaestroEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MaestroEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MaestroWig>().UpdateArmorSet(player);
            }
        }
        public class MetronomeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MaestroEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class ConductorsEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<NiflheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MaestroEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MaestroWig>());
            recipe.AddIngredient(ModContent.ItemType<MaestroSuit>());
            recipe.AddIngredient(ModContent.ItemType<MaestroLeggings>());
            recipe.AddIngredient(ModContent.ItemType<MarchingBandEnchant>());
            recipe.AddIngredient(ModContent.ItemType<Metronome>());
            recipe.AddIngredient(ModContent.ItemType<ConductorsBaton>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
