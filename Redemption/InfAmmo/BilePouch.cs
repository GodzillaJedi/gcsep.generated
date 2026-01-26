using Fargowiltas.Items.Ammos;
using gcsep.Core;
using Redemption.Items.Weapons.HM.Ammo;
using Terraria.ModLoader;

namespace gcsep.Redemption.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class BilePouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<BileBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
}
