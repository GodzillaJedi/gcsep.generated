using Fargowiltas.Items.Ammos;
using gcsep.Core;
using Spooky.Content.Items.SpiderCave.OldHunter;
using Spooky.Content.Items.SpookyBiome;
using Spooky.Content.Items.SpookyHell;
using Terraria.ModLoader;

namespace gcsep.Spooky.InfiniteAmmos.Misc
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class EndlessMucusPouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SnotBullet>();
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class EndlessRustedPouch : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<RustedBullet>();
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class EndlessSnotRocketBox : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<SnotRocket>();
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class MossyPebbleJar : BaseAmmo
    {
        public override int AmmunitionItem => ModContent.ItemType<MossyPebble>();
    }
}