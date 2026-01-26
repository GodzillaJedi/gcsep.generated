using CalamityBardHealer.Buffs;
using CalamityBardHealer.Items;
using CalamityBardHealer.Projectiles;
using CalamityMod;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Auric;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Armor.Demonshade;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using RagnarokMod.Items.BardItems.Accessories;
using RagnarokMod.Items.BardItems.Armor;
using RagnarokMod.Items.HealerItems.Armor;
using RagnarokMod.Utils;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Utilities;

namespace gcsep.Calamity.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class BloodflareEnchant : BaseEnchant
    {
        private Mod calamity;
        
        public override void Load()
        {
            ModLoader.TryGetMod("CalamityMod", out calamity);
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = ItemRarityID.Red;
            Item.value = 50000000;
        }
        public override Color nameColor => new(219, 18, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AfflictionEffect>(Item);
            player.AddEffect<PhantomicEffect>(Item);
            player.AddEffect<BloodflareEffect>(Item);
            player.AddEffect<CruelSigilEffect>(Item);
            player.AddEffect<BloodflareArmorEffect>(Item);
            player.AddEffect<PhantomicMines>(Item);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadMelee>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadRanged>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadMagic>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadRogue>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareHeadSummon>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareBodyArmor>());
            recipe.AddIngredient(ModContent.ItemType<BloodflareCuisses>());
            if (ModCompatibility.Ragnarok.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<BloodflareHeadBard>());
                recipe.AddIngredient(ModContent.ItemType<BloodflareHeadHealer>());
                recipe.AddIngredient(ModContent.ItemType<SigilOfACruelWorld>());
            }
            if (ModCompatibility.CalamityBardHealer.Loaded)
            {
                recipe.AddIngredient(ModContent.ItemType<BloodflareRitualistMask>());
                recipe.AddIngredient(ModContent.ItemType<BloodflareSirenSkull>());
            }
            recipe.AddIngredient(ModContent.ItemType<BloodflareCore>());
            recipe.AddIngredient(ModContent.ItemType<PhantomicArtifact>());
            recipe.AddIngredient(ModContent.ItemType<Affliction>());

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.Register();
        }
        public class PhantomicMines : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BloodflareHeadSummon>().UpdateArmorSet(player);
            }
        }
        public class BloodflareArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();

            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<BloodflareHeadMelee>().UpdateArmorSet(player);
                ModContent.GetInstance<BloodflareHeadRanged>().UpdateArmorSet(player);
                ModContent.GetInstance<BloodflareHeadMagic>().UpdateArmorSet(player);
                ModContent.GetInstance<BloodflareHeadRogue>().UpdateArmorSet(player);
                ModContent.GetInstance<BloodflareRitualistMask>().UpdateArmorSet(player);
                ModContent.GetInstance<BloodflareSirenSkull>().UpdateArmorSet(player);
                ModContent.GetInstance<BloodflareHeadBard>().UpdateArmorSet(player);
                ModContent.GetInstance<BloodflareHeadHealer>().UpdateArmorSet(player);
            }
        }
        public class AfflictionEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().affliction = true;
                if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0 && Main.LocalPlayer.team == player.team && player.team != 0)
                {
                    Main.LocalPlayer.AddBuff(ModContent.BuffType<Afflicted>(), 20);
                }
            }
        }
        public class PhantomicEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().phantomicArtifact = true;
            }
        }
        public class BloodflareEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                player.Calamity().bloodflareCore = true;
            }
        }
        public class CruelSigilEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<ExaltationForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodflareEnchant>();
            public override void PostUpdateEquips(Player player)
            {
                ThoriumPlayer thoriumPlayer = player.GetThoriumPlayer();
                player.GetDamage(ThoriumDamageBase<BardDamage>.Instance) += 0.15f;
                player.GetAttackSpeed(ThoriumDamageBase<BardDamage>.Instance) += 0.08f;
                thoriumPlayer.inspirationRegenBonus += 0.08f;
                thoriumPlayer.bardResourceMax2 += 3;
            }
        }
    }
}
