using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools.Common.Players;
using SacredTools.Content.Items.Accessories.ChallengeItems;
using SacredTools.Content.Items.Armor.Lunar.Quasar;
using SacredTools.Items.Weapons.Lunatic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FallenPrinceEnchant : BaseEnchant
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

        public override Color nameColor => new(91, 94, 122);


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<FallenPrinceEffect>(Item);
            player.AddEffect<ResolveEffect>(Item);
        }

        public class FallenPrinceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FallenPrinceEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<FallenPrinceHelm>().UpdateArmorSet(player);
            }
        }
        public class ResolveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FallenPrinceEnchant>(); 
            public override void PostUpdateEquips(Player player)
            {
                if (player.TryGetModPlayer<MiscEffectsPlayer>(out var miscPlayer))
                {
                    miscPlayer.novanielResolveItem = ModContent.GetInstance<NovanielResolve>().Item;
                    miscPlayer.novanielResolveFunctional = true;
                }

                if (player.TryGetModPlayer<DrawEffectsPlayer>(out var drawPlayer))
                {
                    drawPlayer.novanielResolveVisible = true;
                }
            }
            public override void PostUpdate(Player player)
            {
                if (player.whoAmI != Main.myPlayer)
                    return;

                if (!player.HasEffect<ResolveEffect>())
                {
                    if (player.TryGetModPlayer<MiscEffectsPlayer>(out var miscPlayer))
                    {
                        miscPlayer.novanielResolveItem = null;
                        miscPlayer.novanielResolveFunctional = false;
                    }

                    if (player.TryGetModPlayer<DrawEffectsPlayer>(out var drawPlayer))
                    {
                        drawPlayer.novanielResolveVisible = false;
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<FallenPrinceHelm>();
            recipe.AddIngredient<FallenPrinceChest>();
            recipe.AddIngredient<FallenPrinceBoots>();
            recipe.AddIngredient<NovanielResolve>();
            recipe.AddIngredient<CosmicDesolation>();
            recipe.AddIngredient<LunaticsGamble>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
