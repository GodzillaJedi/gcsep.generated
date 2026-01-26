using gcsep.Core;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Thorium.EternityAccessories
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DarkenedCloak : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return GCSEConfig.Instance.EmodeThorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = 6;
            //    Item.value = 100000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CSEThoriumPlayer>().DarkenedCloak = true;
        }
    }
}
