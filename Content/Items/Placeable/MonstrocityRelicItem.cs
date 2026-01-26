using gcsep.Content.Tiles;
using Terraria.ModLoader;

namespace gcsep.Content.Items.Placeable
{
    public class MonstrocityRelicItem : FargowiltasSouls.Content.Items.Placables.Relics.BaseRelic
    {
        protected override int TileType => ModContent.TileType<MonstrocityRelicTile>();
    }
}
