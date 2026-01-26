using Fargowiltas.Items.Renewals;
using Fargowiltas.Projectiles;
using gcsep.Core;
using Microsoft.Xna.Framework;
using Redemption.Items.Usable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace gcsep.Redemption.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class WastelandRenewal : BaseRenewalItem
    {
        public WastelandRenewal()
            : base("Wasteland Renewal", "Turn large radius into wasteland", ModContent.ItemType<BleachedSolution>())
        {
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(source.Item), position, velocity, ModContent.ProjectileType<WastelandNukeProj>(), 0, 0f, Main.myPlayer);
            return false;
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class WastelandNukeProj : RenewalBaseProj
    {
        public override string Texture => "gcsep/Redemption/InfAmmo/WastelandRenewal";
        public WastelandNukeProj()
            : base("WastelandRenewal", 145, 0, supreme: false)
        {
        }
    }
}