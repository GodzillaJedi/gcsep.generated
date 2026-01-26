using CalamityEntropy.Content.Projectiles;
using CalamityMod.Projectiles.Rogue;
using gcsep.Core;
using Microsoft.Xna.Framework;
using NoxusBoss.Content.NPCs.Bosses.NamelessDeity.Projectiles;
using gcsep.Content.Buffs;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using gcsep.Content.Projectiles;

namespace gcsep
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name, ModCompatibility.Entropy.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name, ModCompatibility.Entropy.Name)]
    public class GCSEKeyTrigs : ModPlayer
    {
        private int LearningExampleKeybindHeldTimer;

        private int LearningExampleKeybindDoubleTapTimer;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (GCSEKeySys.CruserAttack.JustPressed && base.Player.GetModPlayer<GCSEPlayer>().equippedVoidFaquirEnchantment)
            {
                int buf = ModContent.BuffType<GojiPrice>();
                Vector2 position = Player.Center; // Start at the player's center
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - Player.Center) * 10f; // Aim at the cursor
                int type = ModContent.ProjectileType<CruiserShadow>(); // Replace with your projectile type
                int damage = 100; // Set damage
                float knockBack = 2f; // Set knockback

                Projectile.NewProjectile(Player.GetSource_FromThis(), position, velocity, type, damage, knockBack, Player.whoAmI);
            }
            if (GCSEKeySys.SupernovaAttack.JustPressed && base.Player.GetModPlayer<GCSEPlayer>().equippedGemTechEnchantment)
            {
                int buff = ModContent.BuffType<GojiPrice>();
                Vector2 position = Player.Center; // Start at the player's center
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - Player.Center) * 10f; // Aim at the cursor
                int type = ModContent.ProjectileType<SupernovaStealthBoom>(); // Replace with your projectile type
                int damage = 100; // Set damage
                float knockBack = 2f; // Set knockback

                Projectile.NewProjectile(Player.GetSource_FromThis(), position, velocity, type, damage, knockBack, Player.whoAmI);
            }
        }
    }
}
