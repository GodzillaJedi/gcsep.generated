using gcsep.Calamity.Addons.Clamity;
using gcsep.Redemption.Renewals;
using gcsep.SoA.Renewals;
using gcsep.SpiritMod.Renewals;
using gcsep.Spooky.Renewals;

namespace gcsep.Core.RenewalConversions
{
    public static class gcsepConvertToPurity
    {
        public static void ConvertAllToPurity(int i, int j)
        {
            if (ModCompatibility.SpiritMod.Loaded)
            {
                SpiritToPurityConversion.SpiritConvert(i, j, 4);
            }

            if (ModCompatibility.SacredTools.Loaded)
            {
                SacredToolsConversion.FlariumConvert(i, j, 4);
                SacredToolsConversion.ShrineConvert(i, j, 4);
            }

            //if (ModCompatibility.Calamity.Loaded)
            //{
            //    CalamityConversion.AstralConvert(i, j, 4);
            //}   

            if (ModCompatibility.Clamity.Loaded)
            {
                ClamityConversion.FrozenHellConvert(i, j, 4);
            }

            if (ModCompatibility.Redemption.Loaded)
            {
                RedemptionConversion.IrradiatedConvert(i, j, 4);
            }

            if (ModCompatibility.Spooky.Loaded)
            {
                SpookyConversion.SpookyConvert(i, j, 4);
            }
        }
    }
}