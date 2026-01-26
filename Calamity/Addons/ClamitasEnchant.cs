using CalamityMod;
using Clamity;
using Clamity.Content.Bosses.Clamitas.Crafted.ClamitasArmor;
using Clamity.Content.Bosses.Clamitas.Crafted.Weapons;
using Clamity.Content.Bosses.Pyrogen.Drop;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class ClamitasEnchant : BaseEnchant
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 50000000;
        }

        public override Color nameColor => new(173, 52, 70);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<PyrogenEffect>(Item);
            player.AddEffect<ClamitasEffect>(Item);
            player.AddEffect<HellFlareEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ClamitasShellmet>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasShellplate>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasShelleggings>());
            recipe.AddIngredient(ModContent.ItemType<ClamitasCrusher>());
            recipe.AddIngredient(ModContent.ItemType<SoulOfPyrogen>());
            recipe.AddIngredient(ModContent.ItemType<HellFlare>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public class PyrogenEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.GetDamage<MeleeDamageClass>() += 0.1f;
                player.GetDamage<RogueDamageClass>() += 0.1f;
                player.Clamity().pyroSpear = true;
            }
        }
        public class HellFlareEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Clamity().hellFlare = true;
                player.buffImmune[24] = true;
                player.buffImmune[323] = true;
            }
        }

        public class ClamitasEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AddonsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<ClamitasEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.setBonus = ModContent.GetInstance<ClamitasShellmet>().GetLocalization("SetBonus").Format();
                player.Calamity().wearingRogueArmor = true;
                player.maxMinions += 8;
                if (player.whoAmI == Main.myPlayer)
                {
                    IEntitySource source_ItemUse = player.GetSource_Misc("ClamitasEffect");
                    if (player.FindBuffIndex(ModContent.BuffType<HellstoneShellfishStaffBuff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<HellstoneShellfishStaffBuff>(), 3600);
                    }

                    if (player.ownedProjectileCounts[ModContent.ProjectileType<HellstoneShellfishStaffMinion>()] < 2)
                    {
                        Projectile.NewProjectileDirect(source_ItemUse, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HellstoneShellfishStaffMinion>(), 130, 0f, player.whoAmI).originalDamage = 130;
                    }
                }
            }
        }
    }
}
