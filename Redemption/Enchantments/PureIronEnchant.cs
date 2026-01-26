using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Redemption.BaseExtension;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.PreHM.ElderWood;
using Redemption.Items.Armor.PreHM.PureIron;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.Redemption.Enchantments.HardlightEnchant;

namespace gcsep.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class PureIronEnchant : BaseEnchant
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

        public override Color nameColor => new Color(89, 89, 105);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PureIronHelmEffect>(Item);
            player.AddEffect<PureIronEffect>(Item);
            if (player.AddEffect<PureIronCrossEffect>(Item))
            {
                ModContent.GetInstance<ErhanCross>().UpdateAccessory(player, hideVisual);
            }
        }
        public class PureIronHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PureIronEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<PureIronHelmet>().UpdateArmorSet(player);
            }
        }
        public class PureIronCrossEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PureIronEnchant>();
        }
        public class PureIronEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PureIronEnchant>();
            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                if (player.HasEffect<PureIronEffect>())
                {
                    if (Main.rand.NextBool())
                    {
                        target.AddBuff(ModContent.BuffType<PureChillDebuff>(), 1200);
                    }
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<PureChillDebuff>(), 1200);
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<PureIronHelmet>();
            recipe.AddIngredient<PureIronChestplate>();
            recipe.AddIngredient<PureIronGreaves>();
            recipe.AddIngredient<AntlerStaff>();
            recipe.AddIngredient<PureIronSword>();
            recipe.AddIngredient<ErhanCross>();
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
