using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.BaseExtension;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.PreHM.CommonGuard;
using SOTS.Items.Earth;
using SOTS.Items.Planetarium.FromChests;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class CommonGuardEnchant : BaseEnchant
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

        public override Color nameColor => new Color(139, 145, 156);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<CommonGuardHelmEffect>(Item);
            player.AddEffect<CommonGuardEffect>(Item);
            if (player.AddEffect<KeepersCircletEffect>(Item))
            {
                ModContent.GetInstance<KeepersCirclet>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<WardbreakerEffect>(Item))
            {
                ModContent.GetInstance<Wardbreaker>().UpdateAccessory(player, hideVisual);
            }
            if (player.AddEffect<CommonGuardBauble>(Item))
            {
                ModContent.GetInstance<TrappedSoulBauble>().UpdateAccessory(player, hideVisual);
            }
        }
        public class CommonGuardEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
            public override bool ActiveSkill => true;

            public int abilityCD;

            public override void PostUpdateEquips(Player player)
            {
                if (player.whoAmI == Main.myPlayer)
                    CooldownBarManager.Activate("CommonGuardCD", ModContent.Request<Texture2D>("gcsep/Redemption/Enchantments/CommonGuardEnchant").Value, new Color(139, 145, 156),
                        () => (float)abilityCD / 1200, true, activeFunction: () => abilityCD > 0);
            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (abilityCD < 0)
                {
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];

                        if (npc.active && npc.getRect().Intersects(new Rectangle(0, 0, Main.screenWidth, Main.screenHeight)))
                        {
                            npc.RedemptionGuard().GuardPoints = player.ForceEffect<CommonGuardEffect>() ? 0 : npc.RedemptionGuard().GuardPoints / 2;
                            abilityCD = 1200;
                        }
                    }
                }
            }
        }
        public class CommonGuardHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<CommonGuardHelm1>().UpdateArmorSet(player);
                ModContent.GetInstance<CommonGuardHelm2>().UpdateArmorSet(player);
            }
        }
        public class KeepersCircletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
        }
        public class WardbreakerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
        }
        public class CommonGuardBauble : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();

            public override int ToggleItemType => ModContent.ItemType<CommonGuardEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<CommonGuardHelm1>());
            recipe.AddIngredient(ModContent.ItemType<CommonGuardHelm2>());
            recipe.AddIngredient(ModContent.ItemType<CommonGuardPlateMail>());
            recipe.AddIngredient(ModContent.ItemType<CommonGuardGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Wardbreaker>());
            recipe.AddIngredient(ModContent.ItemType<KeepersCirclet>());
            recipe.AddIngredient(ModContent.ItemType<TrappedSoulBauble>());

            recipe.AddTile(26);
            recipe.Register();
        }
    }
}
