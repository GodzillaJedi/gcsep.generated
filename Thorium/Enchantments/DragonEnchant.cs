using CalamityMod.Items.Armor.Aerospec;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FullSerializer;
using gcsep.Calamity.Enchantments;
using gcsep.Content.Buffs;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod.Buffs.Healer;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Dragon;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Utilities;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DragonEnchant : BaseEnchant
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
            player.AddEffect<DragonEffect>(Item);
            player.AddEffect<DragonBreath>(Item);
        }

        public class DragonEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<VanaheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DragonEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<DragonMask>().UpdateArmorSet(player);
            }
        }
        public class DragonBreath : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<DragonEnchant>();
            public override bool ActiveSkill => true;
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                player.AddBuff(ModContent.BuffType<DragonHeartWandBuff>(), 1200);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DragonMask>());
            recipe.AddIngredient(ModContent.ItemType<DragonBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<DragonGreaves>());
            recipe.AddIngredient(ModContent.ItemType<TunePlayerMovementSpeed>());
            recipe.AddIngredient(ModContent.ItemType<EbonyTail>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
