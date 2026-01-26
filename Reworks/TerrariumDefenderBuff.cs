using gcsep.Core;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.Terrarium;

namespace gcsep.Reworks
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class TerrariumDefenderBuff : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<TerrariumDefender>())
            {
                item.defense = 8;
            }
        }
    }
}