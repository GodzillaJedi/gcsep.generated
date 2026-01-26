using CalamityMod.Items.Armor.Aerospec;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using Microsoft.Xna.Framework;
using gcsep.Content.SoulToggles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.RangedItems;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Projectiles;
using ThoriumMod.Utilities;
using static gcsep.Calamity.Enchantments.AstralEnchant;

namespace gcsep.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class AssassinEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.Thorium;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 400000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<AssassinHelmArbalestEffect>(Item);
            player.AddEffect<AssassinHelmMarksmanEffect>(Item);
            player.AddEffect<AssassinEffect>(Item);
            if (player.AddEffect<DartPouchEffect>(Item))
            {
                ModContent.GetInstance<DartPouch>().UpdateAccessory(player, hideVisual);
            }
        }
        public class AssassinHelmArbalestEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AssassinEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MasterArbalestHood>().UpdateArmorSet(player);
            }
        }
        public class AssassinHelmMarksmanEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AssassinEnchant>();
            public override bool MutantsPresenceAffects => true;
            public override void PostUpdateEquips(Player player)
            {
                ModContent.GetInstance<MasterMarksmansScouter>().UpdateArmorSet(player);
            }
        }
        public class DartPouchEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AssassinEnchant>();
            public override bool MutantsPresenceAffects => true;
        }
        public class AssassinEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<AssassinEnchant>();
            public override bool ActiveSkill => true;

            public override void PostUpdateEquips(Player player)
            {
                if (Main.gameMenu) return;

                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                if (modPlayer.assassinCooldown > 0)
                    modPlayer.assassinCooldown--;
            }

            public override void ActiveSkillJustPressed(Player player, bool stunned)
            {
                var modPlayer = player.GetModPlayer<CSEThoriumPlayer>();
                if (modPlayer.assassinCooldown > 0) return;

                Vector2 mousePosition = Main.MouseWorld;
                NPC targetNpc = null;
                float searchRadius = 80f;
                float minDistance = float.MaxValue;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.chaseable && !npc.friendly && npc.life > 0 && !npc.dontTakeDamage)
                    {
                        float distance = Vector2.Distance(npc.Center, mousePosition);
                        if (distance <= searchRadius && distance < minDistance)
                        {
                            minDistance = distance;
                            targetNpc = npc;
                        }
                    }
                }

                if (targetNpc != null)
                {
                    Vector2 teleportPosition = targetNpc.Center;
                    player.Teleport(teleportPosition, TeleportationStyleID.TeleportationPotion);
                    player.immuneTime += 20;

                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, teleportPosition.X, teleportPosition.Y, TeleportationStyleID.TeleportationPotion);
                    }

                    modPlayer.tripleDamageNextHit = true;
                    modPlayer.assassinCooldown = 600; // 10 seconds at 60 FPS
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<AssassinsWalkers>());
            recipe.AddIngredient(ModContent.ItemType<AssassinsGuard>());
            recipe.AddIngredient(ModContent.ItemType<MasterArbalestHood>());
            recipe.AddIngredient(ModContent.ItemType<MasterMarksmansScouter>());
            recipe.AddIngredient(ModContent.ItemType<DartPouch>());
            recipe.AddIngredient(ModContent.ItemType<TheBlackBow>());
            recipe.AddIngredient(ModContent.ItemType<WyrmDecimator>());

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
