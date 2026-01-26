using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Redemption.BaseExtension;
using Redemption.Globals.Players;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.HM.Xenomite;
using Redemption.Items.Armor.PostML.Xenium;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class XenomiteEnchant : BaseEnchant
    {
        public override List<AccessoryEffect> ActiveSkillTooltips =>
            [AccessoryEffectLoader.GetEffect<XenomiteEffect>()];
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

        public override Color nameColor => new Color(88, 126, 121);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.AddEffect<XenomiteNecklaceEffect>(Item))
            {
                ModContent.GetInstance<NecklaceOfPerception>().UpdateAccessory(player, hideVisual);
            }
            player.AddEffect<XenomiteArmorEffect>(Item);
            player.AddEffect<XenomiteEffect>(Item);
            player.GetModPlayer<EnergyPlayer>().energyRegen += 10;
        }
        public class XenomiteNecklaceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XenomiteEnchant>();
        }

        public class XenomiteArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<XenomiteEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<XenomiteHelmet>().UpdateArmorSet(player);
            }
        }
        public class XenomiteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<XenomiteEnchant>();
            public override bool ActiveSkill => true;

            public int cd;

            public override void PostUpdateMiscEffects(Player player)
            {
                if (cd > 0)
                {
                    cd--;
                }
            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (!(cd > 0))
                {
                    //Vector2 groundPosition = FindGroundPosition(player.Center);

                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 position = player.Center + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-20, 20));
                        Vector2 velocity = new Vector2(Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextFloat(-0.2f, 0.2f));

                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            position,
                            velocity,
                            ModContent.ProjectileType<ToxicCloudsProj>(),
                            30,
                            0f,
                            player.whoAmI);

                        SoundEngine.PlaySound(SoundID.Item85 with { Pitch = -0.5f }, player.Center);
                    }
                    cd += 1200;
                }
            }
            //works really bad
            //instead use players center
            //private Vector2 FindGroundPosition(Vector2 startPos)
            //{
            //    int tileX = (int)(startPos.X / 16);
            //    int tileY = (int)(startPos.Y / 16);

            //    while (tileY < Main.maxTilesY && !WorldGen.SolidTile(tileX, tileY))
            //    {
            //        tileY++;
            //    }

            //    return new Vector2(startPos.X, tileY * 16 - 40);
            //}
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient<XenomiteHelmet>();
            recipe.AddIngredient<XenomitePlate>();
            recipe.AddIngredient<XenomiteLeggings>();
            recipe.AddIngredient<Chernobyl>();
            recipe.AddIngredient<DAN>();
            recipe.AddIngredient<NecklaceOfPerception>();
            recipe.AddTile(125);

            recipe.Register();
        }
    }
}
