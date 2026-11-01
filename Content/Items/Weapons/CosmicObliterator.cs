using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Items.Weapons
{
    public class CosmicObliterator : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 950; 
            Item.DamageType = DamageClass.Magic; 
            Item.width = 58;
            Item.height = 58;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Terraria.Item.buyPrice(gold: 50);
            Item.rare = ItemRarityID.Purple;

            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.PlayerSuperCosmicBeam>();
            Item.shootSpeed = 1f;
        }
    }
}
