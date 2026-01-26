using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using gcsep.Core;
using gcsep.Content.Items.Accessories;
using gcsep.Thorium.Toggles;
using Terraria;
using Terraria.ModLoader;

namespace gcsep.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumEternityComponents : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            int type = item.type;

            bool isColossus = type == ModContent.ItemType<ColossusSoul>();
            bool isDimension = type == ModContent.ItemType<DimensionSoul>();
            bool isEternity = type == ModContent.ItemType<EternitySoul>();
            bool isStargate = type == ModContent.ItemType<StargateSoul>();
            bool isSupersonic = type == ModContent.ItemType<SupersonicSoul>();

            bool isMajorSoul = isColossus || isDimension || isEternity || isStargate;

            // Helper: safely apply Thorium accessory
            void Apply(string internalName)
            {
                if (ModContent.TryFind(ModCompatibility.Thorium.Name, internalName, out ModItem modItem))
                    modItem.UpdateAccessory(player, hideVisual);
            }

            // Blast Shield (Colossus / Dimension / Eternity / Stargate)
            if (isMajorSoul && player.AddEffect<BlastShieldEffect>(item))
            {
                Apply("BlastShield");
            }

            // Omega Core (Supersonic / Dimension / Eternity / Stargate)
            if ((isSupersonic || isDimension || isEternity || isStargate) &&
                player.AddEffect<OmegaCoreEffect>(item))
            {
                Apply("TheOmegaCore");
            }
        }
    }
}