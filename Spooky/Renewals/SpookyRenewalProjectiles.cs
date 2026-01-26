using Fargowiltas.Projectiles;
using gcsep.Core;
using gcsep.Core.RenewalConversions;
using Spooky.Content.Generation;
using Spooky.Content.Items.Cemetery.Misc;
using Spooky.Content.Items.SpookyBiome.Misc;
using System;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Spooky.Renewals
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewalProj : RenewalBaseProj
    {
        public override string Texture => "gcsep/Spooky/Renewals/SpookyRenewal";

        public SpookyRenewalProj() : base("SpookyRenewal", ModContent.ProjectileType<SpookySolutionProj>(), 1, false)
        {
        }

        public override void OnKill(int timeLeft)
        {
            int radius = 150;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                    {
                        gcsepConvertToPurity.ConvertAllToPurity(i, j);
                        TileConversionMethods.ConvertPurityIntoSpooky(i, j);
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewalSupremeProj : RenewalBaseProj
    {
        public override string Texture => "gcsep/Spooky/Renewals/SpookyRenewalSupreme";
        public SpookyRenewalSupremeProj() : base("SpookyRenewalSupreme", ModContent.ProjectileType<SpookySolutionProj>(), 1, true)
        {
        }
        public override void OnKill(int timeLeft)
        {
            for (int x = -Main.maxTilesX; x < Main.maxTilesX; x++)
            {
                for (int y = -Main.maxTilesY; y < Main.maxTilesY; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    gcsepConvertToPurity.ConvertAllToPurity(i, j);
                    TileConversionMethods.ConvertPurityIntoSpooky(i, j);
                }
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewalProj : RenewalBaseProj
    {
        public override string Texture => "gcsep/Spooky/Renewals/SwampyRenewal";
        public SwampyRenewalProj() : base("SwampyRenewal", ModContent.ProjectileType<CemeterySolutionProj>(), 4, false)
        {
        }

        public override void OnKill(int timeLeft)
        {
            int radius = 150;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                    {
                        gcsepConvertToPurity.ConvertAllToPurity(i, j);
                        TileConversionMethods.ConvertPurityIntoCemetery(i, j);
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewalSupremeProj : RenewalBaseProj
    {
        public override string Texture => "gcsep/Spooky/Renewals/SwampyRenewalSupreme";
        public SwampyRenewalSupremeProj() : base("SwampyRenewalSupreme", ModContent.ProjectileType<CemeterySolutionProj>(), 4, true)
        {
        }

        public override void OnKill(int timeLeft)
        {
            for (int x = -Main.maxTilesX; x < Main.maxTilesX; x++)
            {
                for (int y = -Main.maxTilesY; y < Main.maxTilesY; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    gcsepConvertToPurity.ConvertAllToPurity(i, j);
                    TileConversionMethods.ConvertPurityIntoCemetery(i, j);
                }
            }
        }
    }
}