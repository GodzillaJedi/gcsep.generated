using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Accessories.Bookmarks;
using ContinentOfJourney.Items.Accessories.MeleeExpansion;
using ContinentOfJourney.Items.Accessories.SummonerRings;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler.Content;
using gcsep.Core;
using gcsep.Content.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Homeward
{
    [ExtendsFromMod(ModCompatibility.Homeward.Name)]
    [JITWhenModsEnabled(ModCompatibility.Homeward.Name)]
    public class HwjEternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            GCSEPlayer modPlayer = player.GetModPlayer<GCSEPlayer>();

            bool hasHomeward = ModCompatibility.Homeward.Loaded;
            bool hasCalamity = ModCompatibility.Calamity.Loaded;

            int type = item.type;

            // Soul identity flags
            bool isConjurist = type == ModContent.ItemType<ConjuristsSoul>();
            bool isSupersonic = type == ModContent.ItemType<SupersonicSoul>();
            bool isDimension = type == ModContent.ItemType<DimensionSoul>();
            bool isArchWizard = type == ModContent.ItemType<ArchWizardsSoul>();
            bool isSniper = type == ModContent.ItemType<SnipersSoul>();
            bool isUniverse = type == ModContent.ItemType<UniverseSoul>();
            bool isEternity = type == ModContent.ItemType<EternitySoul>();
            bool isStargate = type == ModContent.ItemType<StargateSoul>();

            bool isAnyMajorSoul =
                isUniverse || isEternity || isStargate;

            // ---------------------------------------------------------
            // 1. Homeward‑based soul effects
            // ---------------------------------------------------------

            if (hasHomeward)
            {
                // Conjurist / Universe / Eternity / Stargate
                if (isConjurist || isAnyMajorSoul)
                    player.AddEffect<DivineNecklaceEffect>(item);

                // Supersonic / Dimension / Eternity / Stargate
                if (isSupersonic || isDimension || isAnyMajorSoul)
                {
                    player.AddEffect<ArrowCaseEffect>(item);
                    player.AddEffect<EdgewalkerEffect>(item);
                }

                // ArchWizard / Universe / Eternity / Stargate
                if (isArchWizard || isAnyMajorSoul)
                {
                    player.AddEffect<RejuvenatedCrossEffect>(item);
                    player.AddEffect<EruditeBookmarkEffect>(item);
                }

                // Sniper / Universe / Eternity / Stargate
                if (isSniper || isAnyMajorSoul)
                {
                    player.AddEffect<WalnutOnFireEffect>(item);
                    player.AddEffect<StarQuiverEffect>(item);
                    player.AddEffect<TheBatterEffect>(item);
                }

                // Always‑on Homeward effects
                player.AddEffect<OneGiantLeapEffect>(item);
                player.AddEffect<CommandersGauntletEffect>(item);
                player.AddEffect<PhilosophersStoneEffect>(item);
                player.AddEffect<GodlyTouchEffect>(item);
                player.AddEffect<BerserkerGloveEffect>(item);

                // Master Shield (Homeward)
                if (ModContent.TryFind(ModCompatibility.Homeward.Name, "MasterShield", out ModItem masterShield))
                    masterShield.UpdateAccessory(player, true);
            }

            // ---------------------------------------------------------
            // 2. Calamity compatibility
            // ---------------------------------------------------------

            if (hasCalamity && hasHomeward)
            {
                if (ModContent.TryFind(ModCompatibility.Calamity.Name, "ElementalQuiver", out ModItem elementalQuiver))
                {
                    if (type == elementalQuiver.Type)
                        player.AddEffect<StarQuiverEffect>(item);
                }
            }

            // ---------------------------------------------------------
            // 3. Specific Homeward item check
            // ---------------------------------------------------------

            if (hasHomeward)
            {
                if (ModContent.TryFind(ModCompatibility.Homeward.Name, "CommandersGaunlet", out ModItem gauntlet))
                {
                    if (type == gauntlet.Type)
                        player.AddEffect<BerserkerGloveEffect>(item);
                }
            }
        }
        public class DivineNecklaceEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineNecklace>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("DivineNecklace").UpdateAccessory(player, true);
            }
        }
        public class ArrowCaseEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
            public override int ToggleItemType => ModContent.ItemType<ArrowCase>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("ArrowCase").UpdateAccessory(player, true);
            }
        }
        public class EdgewalkerEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SupersonicHeader>();
            public override int ToggleItemType => ModContent.ItemType<Edgewalker>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("Edgewalker").UpdateAccessory(player, true);
            }
        }
        public class EruditeBookmarkEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<EruditeBookmark>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("EruditeBookmark").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class RejuvenatedCrossEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<RejuvenatedCross>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("RejuvenatedCross").UpdateAccessory(player, true);
            }
        }
        public class OneGiantLeapEffect : AccessoryEffect
        {
            public override int ToggleItemType => ModContent.ItemType<OneGiantLeap>();
            public override Header ToggleHeader => Header.GetHeader<ColossusHeader>();
            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("OneGiantLeap").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class PhilosophersStoneEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<PhilosophersStone>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("PhilosophersStone").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class CommandersGauntletEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<CommandersGaunlet>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("CommandersGaunlet").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class GodlyTouchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<DivineTouch>();

            public override void PostUpdateEquips(Player player)
            {
                //no free 15% stat boosts
                ModCompatibility.Homeward.Mod.Find<ModItem>("DivineEmblem").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class WalnutOnFireEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<WalnutOnFire>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("WalnutOnFire").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class StarQuiverEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<StarQuiver>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("StarQuiver").UpdateAccessory(player, true);
            }
        }
        [ExtendsFromMod(ModCompatibility.Homeward.Name)]
        public class TheBatterEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<UniverseHeader>();
            public override int ToggleItemType => ModContent.ItemType<TheBatter>();

            public override void PostUpdateEquips(Player player)
            {
                ModCompatibility.Homeward.Mod.Find<ModItem>("TheBatter").UpdateAccessory(player, true);
            }
        }
        public class BerserkerGloveEffect : AccessoryEffect
        {
            public override Header ToggleHeader => null;
            public override int ToggleItemType => ItemID.BerserkerGlove;
            public override void PostUpdateEquips(Player player)
            {
                player.statDefense += 8;
                player.kbGlove = true;
                player.meleeScaleGlove = true;
                player.autoReuseGlove = true;
            }
        }
    }
}
