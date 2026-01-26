global using FargowiltasSouls.Core.ModPlayers;
global using FargowiltasSouls.Core.Toggler;
global using LumUtils = Luminance.Common.Utilities.Utilities;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.MusicBoxes;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Core.Globals;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using gcsep.Content.Items.Consumables;
using gcsep.Content.NPCs.MutantEX;
using gcsep.Content.Sky;
using gcsep.Content.UI;
using gcsep.gunrightsmod;
using gcsep.Items;
using gcsep.Redemption;
using gcsep.SoA;
using gcsep.SpiritMod;
using gcsep.Systems;
using gcsep.Thorium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Chat;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static gcsep.Thorium.Enchantments.CyberPunkEnchant;
using gcsep.CrossMod.CraftingStations;

namespace gcsep
{
    public partial class gcsep : Mod
    {
        // Swarms
        public static bool PostMLSwarmActive;
        public static bool HardmodeSwarmActive;
        public static bool SwarmNoHyperActive;
        public static int SwarmItemsUsed;
        internal static bool SwarmSetDefaults;
        public static bool SwarmActive;
        public static int SwarmKills;
        public static int SwarmTotal;
        public static int SwarmSpawned;

        internal static ModKeybind dotMount;

        public UserInterface _bossSummonUI;
        public BossSummonUI _bossSummonState;
        public bool _showBossSummonUI;

        internal static gcsep Instance;
        public static bool debug = GCSEConfig.Instance.DebugMode;

        private delegate void UIModDelegate(object instance, SpriteBatch spriteBatch);
        public static bool amiactive;
        public static readonly BindingFlags UniversalBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public static bool legit;
        public static int OS;
        public static int[] AllStationIDs { get; private set; }
        public static string userName = Environment.UserName;
        public static string filePath = "C:/Users/" + userName + "/Documents/My Games/Terraria/tModLoader/StarlightSouls";

        public override uint ExtraPlayerBuffSlots => 300u;

        public static int SwarmMinDamage
        {
            get
            {
                float num = ((!gcsep.HardmodeSwarmActive) ? ((float)(50 + 3 * gcsep.SwarmItemsUsed)) : ((float)(60 + 40 * gcsep.SwarmItemsUsed)));
                return (int)num;
            }
        }
        public enum PacketID : byte
        {
            SpawnFishronEX,
            HealthDataSync,
            RequestCyberOrbs
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte data = reader.ReadByte();
            if (Enum.IsDefined(typeof(PacketID), data))
            {
                switch ((PacketID)data)
                {
                    case PacketID.SpawnFishronEX:
                        HandleSpawnFishronEX(reader, whoAmI);
                        break;

                    case PacketID.HealthDataSync:
                        HandleHealthDataSync(reader, whoAmI);
                        break;
                    case PacketID.RequestCyberOrbs:
                        if (Main.netMode == NetmodeID.Server)
                        {
                            byte p = reader.ReadByte();
                            int multiplier = reader.ReadByte();
                            int n = NPC.NewNPC(NPC.GetBossSpawnSource(p), (int)Main.player[p].Center.X, (int)Main.player[p].Center.Y, ModContent.NPCType<CyberneticOrb>(), 0,
                                p, 0f, multiplier, 0);
                            if (n != Main.maxNPCs)
                            {
                                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                            }
                        }
                        break;
                }
            }
        }
        private void HandleSpawnFishronEX(BinaryReader reader, int whoAmI)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                byte target = reader.ReadByte();
                int x = reader.ReadInt32();
                int y = reader.ReadInt32();
                EModeGlobalNPC.spawnFishronEX = true;
                NPC.NewNPC(NPC.GetBossSpawnSource(target), x, y, NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, target);
                ChatHelper.BroadcastChatMessage(
                    NetworkText.FromKey("Announcement.HasAwoken",
                    Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DukeFishronEX.DisplayName")),
                    new Color(50, 100, 255));
            }
        }

        private void HandleHealthDataSync(BinaryReader reader, int whoAmI)
        {
            byte playerId = reader.ReadByte();
            Player player = Main.player[playerId];

            //if (player.TryGetModPlayer(out MonstrHealthPlayer modPlayer))
            //{
            //    modPlayer.OriginalMaxLife = reader.ReadInt32();
            //    modPlayer.HealthReduction = reader.ReadInt32();

            //    if (Main.netMode == NetmodeID.Server)
            //    {
            //        modPlayer.SyncData();
            //    }

            //    if (Main.netMode == NetmodeID.MultiplayerClient)
            //    {
            //        player.statLifeMax = modPlayer.OriginalMaxLife - modPlayer.HealthReduction;
            //        if (player.statLife > player.statLifeMax)
            //        {
            //            player.statLife = player.statLifeMax;
            //        }
            //    }
            //}
        }
        private void BossChecklistCompatibility()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                static bool AllPlayersAreDead() => Main.player.All(plr => !plr.active || plr.dead);

                void Add(string type, string bossName, float priority, List<int> npcIDs, Func<bool> downed, Func<bool> available, List<int> collectibles, List<int> spawnItems, bool hasKilledAllMessage, string portrait = null)
                {
                    bossChecklist.Call(
                        $"Log{type}",
                        this,
                        bossName,
                        priority,
                        downed,
                        npcIDs,
                        new Dictionary<string, object>()
                        {
                            { "spawnItems", spawnItems },
                            { "availability", available },
                            { "despawnMessage", hasKilledAllMessage ? new Func<NPC, LocalizedText>(npc =>
                                        AllPlayersAreDead() ? Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.KilledAllMessage") : Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.DespawnMessage")) :
                                    Language.GetText($"Mods.{Name}.NPCs.{bossName}.BossChecklistIntegration.DespawnMessage") },
                            {
                                "customPortrait",
                                portrait == null ? null : new Action<SpriteBatch, Rectangle, Color>((spriteBatch, rect, color) =>
                                {
                                    Texture2D tex = Assets.Request<Texture2D>(portrait, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                                    Rectangle sourceRect = tex.Bounds;
                                    float scale = Math.Min(1f, (float)rect.Width / sourceRect.Width);
                                    spriteBatch.Draw(tex, rect.Center.ToVector2(), sourceRect, color, 0f, sourceRect.Size() / 2, scale, SpriteEffects.None, 0);
                                })
                            }
                        }
                    );
                }

                Add("Boss",
                    "MutantEX",
                    float.MaxValue,
                    new List<int> { ModContent.NPCType<MutantEX>() },
                    () => WorldSaveSystem.downedMutantEX,
                    () => true,
                    new List<int> {
                        ModContent.ItemType<MutantMusicBox>(),
                        ModContent.ItemType<Sadism>(),
                        ModContent.ItemType<MutantTrophy>(),
                        ModContent.ItemType<SpawnSack>(),
                        //ModContent.ItemType<PhantasmalEnergy>()
                    },
                    new List<int> { ModContent.ItemType<MutantsForgeItem>() },
                    true
                );

                if (ModCompatibility.SacredTools.Loaded)
                {
                    ModCompatibility.SacredTools.Mod.TryFind<ModNPC>("Nihilus", out ModNPC Nihilus);
                    ModCompatibility.SacredTools.Mod.TryFind<ModItem>("EmberOfOmen", out ModItem Ember);
                    ModCompatibility.SacredTools.Mod.TryFind<ModItem>("NihilusObelisk", out ModItem Obelisk);

                    Add("Boss",
                    "Nihilus",
                    int.MaxValue - 100,
                    new List<int> { Nihilus.Type },
                    () => WorldSaveSystem.downedNihilus,
                    () => true,
                    new List<int> {
                        Ember.Type
                    },
                    new List<int> { Obelisk.Type },
                    true
                );
                }
            }
        }

        public override void Load()
        {
            //// Load the overlay texture
            //overlayTexture = ModContent.Request<Texture2D>("gcsep/Assets/ModIcon/LifeStar", AssetRequestMode.ImmediateLoad).Value;

            //// Get the UIModItem.Draw method using reflection
            //Type uiModItemType = typeof(ModLoader).Assembly.GetType("Terraria.ModLoader.UI.UIModItem");
            //MethodInfo drawMethod = uiModItemType.GetMethod("Draw", BindingFlags.Instance | BindingFlags.Public);

            //// Add the hook with a compatible delegate
            //MonoModHooks.Add(drawMethod, (Action<object, SpriteBatch>)DrawHook);

            _bossSummonUI = new UserInterface();
            ModIntergationSystem.BossChecklist.AdjustValues();

            Instance = this;
            OS = OSType();

            CaughtNPCItem.RegisterItems();

            if (ModLoader.TryGetMod("ThoriumMod", out Mod tor))
            {
                ThoriumCaughtNpcs.ThoriumRegisterItems();
            }
            if (ModLoader.TryGetMod("SacredTools", out Mod soa))
            {
                SoACaughtNpcs.SoARegisterItems();
            }
            if (ModLoader.TryGetMod("Redemption", out Mod red))
            {
                RedemptionCaughtNpcs.RedemptionRegisterItems();
            }
            if (ModLoader.TryGetMod("gunrightsmod", out Mod grm))
            {
                gunrightsmodCaughtNpcs.gunrightsmodRegisterItems();
            }
            if (ModLoader.TryGetMod("SpiritMod", out Mod spr))
            {
                SpiritModCaughtNpcs.SpiritModRegisterItems();
            }

            SkyManager.Instance["gcsep:MutantEX"] = new MutantEXSky();

            ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist);
        }

        public override void Unload()
        {
            //overlayTexture = null;
            _bossSummonUI = null;
            Instance = null;
        }

        public void ShowBossSummonUI()
        {
            if (_bossSummonState == null)
            {
                _bossSummonState = new BossSummonUI();
                _bossSummonUI.SetState(_bossSummonState);
            }
            _showBossSummonUI = true;
        }
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("Wikithis", out Mod wikithis) && !Main.dedServ)
            {
                wikithis.Call("AddModURL", this, "https://terrariamods.wiki.gg/wiki/Community_Souls_Expansion/{}");
            }
            BossChecklistCompatibility();
            if (ModCompatibility.Thorium.Loaded)
            {
                PostSetupContentThorium.PostSetupContent_Thorium();
            }
        }

        public int OSType()
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID platform = os.Platform;
            switch (platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    filePath = "C:/Users/" + userName + "/Documents/My Games/Terraria/tModLoader/StarlightSouls";
                    return 0;
                case PlatformID.Unix:
                    filePath = "/home/" + userName + "/.local/share/Terraria/tModLoader/StarlightSouls";
                    return 1;
                case PlatformID.MacOSX:
                    filePath = "/Users/" + userName + "/Library/Application Support/Terraria/tModLoader/StarlightSouls";
                    return 2;
                default:
                    filePath = Main.SavePath + "/ModLoader/StarlightSouls";
                    break;
            }
            return -1;
        }
    }
}
