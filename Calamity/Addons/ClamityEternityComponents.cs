using Clamity.Content.Items.Accessories;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using gcsep.Content.Items.Accessories;
using gcsep.Content.SoulToggles;
using gcsep.Thorium.Toggles;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Calamity.Addons
{
    // This entire GlobalItem ONLY loads when Calamity is present
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class ClamityEternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (IsSoul(item.type) && player.AddEffect<BloodGodEffect>(item))
            {
                TryApplyAccessory(ModCompatibility.Clamity.Name, "SkullOfTheBloodGod", player, hideVisual);
            }
        }

        private bool IsSoul(int type)
        {
            return type == ModContent.ItemType<ColossusSoul>() ||
                   type == ModContent.ItemType<DimensionSoul>() ||
                   type == ModContent.ItemType<EternitySoul>() ||
                   type == ModContent.ItemType<StargateSoul>();
        }

        private void TryApplyAccessory(string modName, string itemName, Player player, bool hideVisual)
        {
            if (ModContent.TryFind(modName, itemName, out ModItem modItem))
            {
                modItem.UpdateAccessory(player, hideVisual);
            }
        }

        public class BloodGodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override int ToggleItemType => ModContent.ItemType<SkullOfTheBloodGod>();
        }
    }

    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class ClamityRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.HasResult(ModContent.ItemType<ColossusSoul>()))
                {
                    if (ModContent.TryFind(ModCompatibility.Clamity.Name, "SkullOfTheBloodGod", out ModItem skull))
                    {
                        recipe.AddIngredient(skull.Type, 1);
                    }
                }
            }
        }
    }
}
