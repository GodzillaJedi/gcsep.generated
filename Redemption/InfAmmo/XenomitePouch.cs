using Fargowiltas.Items.Ammos;
using gcsep.Core;
using Redemption.Items.Weapons.PreHM.Ammo;
using Terraria.ModLoader;

namespace gcsep.Redemption.InfAmmo
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class XenomitePouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<XenomiteBullet>();

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
}
