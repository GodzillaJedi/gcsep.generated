using FargowiltasSouls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Content.SoulToggles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using SacredTools;
using SacredTools.Content.Items.Accessories;
using SacredTools.Content.Items.Armor.Lunar.Nebula;
using SacredTools.Content.Items.Armor.Marstech;
using SacredTools.Content.Items.Weapons.Asthraltite;
using SacredTools.Content.Projectiles.Armors.Nuba;
using SacredTools.Items.Weapons.Lunatic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static gcsep.SoA.Enchantments.LapisEnchant;

namespace gcsep.SoA.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class NebulousApprenticeEnchant : BaseEnchant
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

        public override Color nameColor => new(206, 7, 221);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<NebulousApprenticeHelmEffect>(Item);
            if (player.AddEffect<NubaEffect>(Item))
            {
                ModContent.GetInstance<NubasBlessing>().UpdateAccessory(player, hideVisual);
            }
        }
        public class NubaEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NebulousApprenticeEnchant>();
        }
        public class NebulousApprenticeHelmEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SoranForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<NebulousApprenticeEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<NubaHood>().UpdateArmorSet(player);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();
            recipe.AddIngredient<NubaHood>();
            recipe.AddIngredient<NubaChest>();
            recipe.AddIngredient<NubaRobe>();
            recipe.AddIngredient<NubasBlessing>();
            recipe.AddIngredient<LunaticBurstStaff>();
            recipe.AddIngredient<AsthralStaff>();
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}
