using FargowiltasSouls;
using gcsep.Core;
using Redemption.NPCs.Lab.Volt;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Redemption.MinibossSummons
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]

    public class GreenComputerChip : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 48;
            Item.rare = ItemRarityID.Purple;
            Item.maxStack = 20;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<ProtectorVolt>());
            return true;
        }
    }
}