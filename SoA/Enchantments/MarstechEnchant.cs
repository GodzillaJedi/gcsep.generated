using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.UI.Elements;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.Projectiles.Enchantments;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SacredTools;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Lapis;
using SacredTools.Content.Items.Armor.Marstech;
using SacredTools.Items.Weapons.Marstech;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class MarstechEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.SacredTools;
        }

        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 8;
            Item.value = 250000;
        }

        public override Color nameColor => new(61, 155, 189);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<MarstechHelmEffect>(Item);
            player.AddEffect<MarstechEffect>(Item);
            ModContent.GetInstance<SpaceJunkEnchant>().UpdateAccessory(player, hideVisual);
        }

        public class MarstechHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<GenerationsForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<MarstechEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MarstechHelm>().UpdateArmorSet(player);
            }
        }
        public class MarstechEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ModContent.ItemType<MarstechEnchant>();
            public override bool ActiveSkill => true;

            private int cd;
            public override void PostUpdateEquips(Player player)
            {
                if (cd > 0)
                {
                    cd--;
                }

                CooldownBarManager.Activate("MarstechEnchantCooldown", ModContent.Request<Texture2D>("gcsep/SoA/Enchantments/MarstechEnchant").Value, new(61, 155, 189),
                    () => cd / (60f * 15), true, activeFunction: player.HasEffect<MarstechEffect>);

            }
            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                if (!(cd > 0))
                {
                    Projectile.NewProjectile(
                        player.GetSource_GiftOrReward(),
                        player.Center,
                        new Vector2(0, -9f),
                        ModContent.ProjectileType<MartianProbe>(),
                        50,
                        5f,
                        player.whoAmI
                    );
                    cd += 7 * 60;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<PhaseSlasher>();
            recipe.AddIngredient<PlasmaDischarge>();
            recipe.AddIngredient<MarstechHelm>();
            recipe.AddIngredient<MarstechPlate>();
            recipe.AddIngredient<MarstechLegs>();
            recipe.AddIngredient<SpaceJunkEnchant>();
            recipe.AddTile(125);
            recipe.Register();
        }
    }
}
