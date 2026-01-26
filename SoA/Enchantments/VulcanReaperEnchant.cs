using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Common.Players;
using SacredTools.Content.Items.Armor.Oblivion;
using SacredTools.Content.Items.Armor.Vulcan;
using SacredTools.Items.Placeable.Paintings;
using SacredTools.Items.Potions;
using SacredTools.Items.Weapons.Flarium;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class VulcanReaperEnchant : BaseEnchant
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
            Item.rare = 11;
            Item.value = 350000;
        }

        public override Color nameColor => new(138, 36, 58);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<VulcanReaperEffect>(Item);
            player.AddEffect<VulcanReaperHelmEffect>(Item);
        }

        public class VulcanReaperHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VulcanReaperEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<VulcanHelm>().UpdateArmorSet(player);
            }
        }
        public class VulcanReaperEffect : AccessoryEffect
        {
            public int vulcanStacks;
            public int vulcanTime;
            public override Header ToggleHeader => Header.GetHeader<SyranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<VulcanReaperEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                if (vulcanTime >= 300)
                {
                    vulcanStacks--;
                    vulcanTime = 0;
                }

                player.GetModPlayer<MiscEffectsPlayer>().bossDamage += vulcanStacks * 0.05f;
            }

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                if (target.life <= 0 && !target.friendly && target.type != NPCID.TargetDummy && vulcanStacks < 5)
                {
                    vulcanStacks++;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<VulcanHelm>();
            recipe.AddIngredient<VulcanChest>();
            recipe.AddIngredient<VulcanLegs>();
            recipe.AddIngredient<SmolderingSpicyCurry>();
            recipe.AddIngredient<SerpentChain>();
            recipe.AddIngredient<Warmth>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
