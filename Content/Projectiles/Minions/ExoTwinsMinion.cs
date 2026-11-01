using CalamityMod;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace gcsep.Content.Projectiles.Minions
{
    public class ExoTwinsMinion : ModProjectile
    {
        public const int PhaseVolley = 0;     
        public const int PhaseAssault = 1;    
        public const float VolleyDuration = 200f;
        public const float AssaultDuration = 130f;

        public ref float Phase => ref Projectile.ai[0];
        public ref float PhaseTimer => ref Projectile.ai[1];

        public int ArtemisIndex = -1;
        public int ApolloIndex = -1;

        public override string Texture => "CalamityMod/NPCs/ExoMechs/Artemis/Artemis";
        public NPC Target
        {
            get
            {
                int index = (int)Projectile.localAI[0] - 1;
                return index >= 0 && index < Main.maxNPCs && Main.npc[index].active ? Main.npc[index] : null;
            }
        }

        public int MinionDamage => Math.Max(1, Projectile.damage);

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 1;
            ProjectileID.Sets.MinionSacrificable[Type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 4000; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft = 18000;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.DamageType = DamageClass.Summon;
        }

        public override bool? CanDamage() => false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!player.active || player.dead || !player.GetModPlayer<GCSEPlayer>().TwinsSummon)
            {
                Projectile.Kill();
                return;
            }
            Projectile.timeLeft = 2;
            Projectile.Center = player.Center;
            Projectile.velocity = Vector2.Zero;

            NPC target = Projectile.Center.MinionHoming(2500f, player);
            Projectile.localAI[0] = target != null ? target.whoAmI + 1 : 0f;

            if (target != null)
            {
                PhaseTimer++;
                float duration = (int)Phase == PhaseVolley ? VolleyDuration : AssaultDuration;
                if (PhaseTimer >= duration)
                {
                    Phase = (int)Phase == PhaseVolley ? PhaseAssault : PhaseVolley;
                    PhaseTimer = 0f;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Phase = PhaseVolley;
                PhaseTimer = 0f;
            }

            FindCompanions();
            if (Main.netMode != NetmodeID.MultiplayerClient)
                EnsureCompanionsExist();
        }
        private void FindCompanions()
        {
            ArtemisIndex = -1;
            ApolloIndex = -1;
            int artemisType = ModContent.NPCType<ExoTwinsArtemis>();
            int apolloType = ModContent.NPCType<ExoTwinsApollo>();

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active || (npc.type != artemisType && npc.type != apolloType))
                    continue;
                if ((int)npc.ai[3] != Projectile.owner || (int)npc.ai[2] != Projectile.identity)
                    continue;

                if (npc.type == artemisType)
                    ArtemisIndex = i;
                else
                    ApolloIndex = i;
            }
        }

        private void EnsureCompanionsExist()
        {
            if (ArtemisIndex < 0)
                SpawnTwin(ModContent.NPCType<ExoTwinsArtemis>(), -160f);
            if (ApolloIndex < 0)
                SpawnTwin(ModContent.NPCType<ExoTwinsApollo>(), 160f);
        }

        private void SpawnTwin(int type, float xOffset)
        {
            int index = NPC.NewNPC(
                Projectile.GetSource_FromAI(),
                (int)(Projectile.Center.X + xOffset),
                (int)(Projectile.Center.Y - 140f),
                type,
                0,
                0f,
                0f,
                Projectile.identity,
                Projectile.owner);

            if (index >= 0 && index < Main.maxNPCs)
                Main.npc[index].netUpdate = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawTwin(ArtemisIndex, ModContent.NPCType<ExoTwinsArtemis>(), Artemis.GlowTexture.Value);
            DrawTwin(ApolloIndex, ModContent.NPCType<ExoTwinsApollo>(), Apollo.GlowTexture.Value);
            return false;
        }

        private void DrawTwin(int npcIndex, int npcType, Texture2D glow)
        {
            if (npcIndex < 0 || npcIndex >= Main.maxNPCs)
                return;
            NPC npc = Main.npc[npcIndex];
            if (!npc.active || !(npc.ModNPC is ExoTwinBase twin))
                return;

            Texture2D texture = TextureAssets.Npc[npcType].Value;
            int cellWidth = texture.Width / ExoTwinBase.SheetColumns;
            int cellHeight = texture.Height / ExoTwinBase.SheetRows;
            int index = twin.FrameIndex;
            Rectangle frame = new Rectangle(cellWidth * (index / ExoTwinBase.SheetRows), cellHeight * (index % ExoTwinBase.SheetRows), cellWidth, cellHeight);

            Vector2 position = npc.Center - Main.screenPosition;
            Vector2 origin = frame.Size() * 0.5f;
            float scale = 0.8f * Projectile.scale;
            Color light = Lighting.GetColor(npc.Center.ToTileCoordinates());

            Main.EntitySpriteDraw(texture, position, frame, light * Projectile.Opacity, npc.rotation, origin, scale, SpriteEffects.None, 0f);
            Main.EntitySpriteDraw(glow, position, frame, Color.White * Projectile.Opacity, npc.rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
    public abstract class ExoTwinBase : ModNPC
    {
        public const int SheetColumns = 10;
        public const int SheetRows = 9;

        protected const int IdleFirst = 0, IdleLast = 9;
        protected const int ChargeFirst = 10, ChargeLast = 19;
        protected const int AttackFirst = 20, AttackLast = 29;

        protected const float LaserDamageMult = 1f;
        protected const float PlasmaDamageMult = 1.3f;
        protected const float RocketDamageMult = 1.5f;

        public int FrameIndex;
        private int frameTimer;

        protected int OwnerIndex => (int)NPC.ai[3];
        protected int ParentIdentity => (int)NPC.ai[2];

        protected abstract Vector2 IdleOffset { get; }
        protected abstract Vector2 HoverOffset { get; }
        protected abstract void Behave(Player player, NPC target, ExoTwinsMinion minion);

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers { Hide = true };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.aiStyle = -1;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 1;
            NPC.life = 1;
            NPC.friendly = true;
            NPC.dontTakeDamage = true;
            NPC.chaseable = false;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.npcSlots = 0f;
            NPC.boss = false;
            NPC.netAlways = true;
        }
        public override bool CheckActive() => false;
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) => false;

        private bool TryGetMaster(out ExoTwinsMinion minion)
        {
            minion = null;
            int owner = OwnerIndex;
            if (owner < 0 || owner >= Main.maxPlayers)
                return false;

            int index = Projectile.GetByUUID(owner, ParentIdentity);
            if (index < 0 || index >= Main.maxProjectiles)
                return false;

            Projectile master = Main.projectile[index];
            if (!master.active || master.owner != owner || !(master.ModProjectile is ExoTwinsMinion m))
                return false;

            minion = m;
            return true;
        }

        public override void AI()
        {
            if (!TryGetMaster(out ExoTwinsMinion minion))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                return;
            }

            Player player = Main.player[OwnerIndex];

            if (Vector2.Distance(NPC.Center, player.Center) > 3500f)
                NPC.Center = player.Center + IdleOffset;

            NPC target = minion.Target;
            if (target == null)
            {
                MoveTo(player.Center + IdleOffset, 16f);
                NPC.rotation = StepAngle(NPC.rotation, NPC.velocity.X * 0.02f, 0.1f);
                Animate(IdleFirst, IdleLast);
                return;
            }

            Behave(player, target, minion);
        }
        protected void MoveTo(Vector2 destination, float maxSpeed)
        {
            Vector2 offset = destination - NPC.Center;
            float distance = offset.Length();
            Vector2 desired = distance > 1f ? offset / distance * Math.Min(maxSpeed, distance * 0.1f) : Vector2.Zero;
            NPC.velocity = Vector2.Lerp(NPC.velocity, desired, 0.1f);
        }
        protected void AimAt(Vector2 point)
        {
            float aim = (point - NPC.Center).ToRotation() + MathHelper.PiOver2;
            NPC.rotation = StepAngle(NPC.rotation, aim, 0.25f);
        }
        protected static float StepAngle(float current, float target, float maxChange)
        {
            float diff = MathHelper.WrapAngle(target - current);
            return MathHelper.WrapAngle(current + MathHelper.Clamp(diff, -maxChange, maxChange));
        }
        protected void Animate(int first, int last)
        {
            if (++frameTimer >= 6)
            {
                frameTimer = 0;
                FrameIndex++;
            }
            if (FrameIndex < first || FrameIndex > last)
                FrameIndex = first;
        }
        protected static bool Every(float timer, float interval, float start) => timer >= start && (timer - start) % interval == 0f;
        protected static void MakeFriendly(int index)
        {
            if (index < 0 || index >= Main.maxProjectiles)
                return;
            Projectile p = Main.projectile[index];
            p.hostile = false;
            p.friendly = true;
            p.DamageType = DamageClass.Summon;
        }
    }
    public class ExoTwinsArtemis : ExoTwinBase
    {
        private const float DashDuration = 45f;
        public override string Texture => "CalamityMod/NPCs/ExoMechs/Artemis/Artemis";
        protected override Vector2 IdleOffset => new Vector2(-160f, -140f);
        protected override Vector2 HoverOffset => new Vector2(-450f, -150f);

        protected override void Behave(Player player, NPC target, ExoTwinsMinion minion)
        {
            float t = minion.PhaseTimer;
            Vector2 hover = target.Center + HoverOffset;

            if ((int)minion.Phase == ExoTwinsMinion.PhaseVolley)
            {
                MoveTo(hover, 22f);
                AimAt(target.Center);
                Animate(AttackFirst, AttackLast);

                if (Every(t, 20f, 20f))
                    FireLaser(target, minion, 0f, true);
            }
            else
            {
                AimAt(target.Center);

                if (t < DashDuration)
                {
                    // Dash through the target
                    Animate(ChargeFirst, ChargeLast);
                    Vector2 dash = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    NPC.velocity = Vector2.Lerp(NPC.velocity, dash * 30f, 0.08f);
                }
                else
                {
                    Animate(AttackFirst, AttackLast);
                    MoveTo(hover, 22f);

                    if (t == DashDuration)
                    {
                        SoundEngine.PlaySound(Artemis.LaserShotgunSound, NPC.Center);
                        for (int i = -2; i <= 2; i++)
                            FireLaser(target, minion, i * 0.18f, false);
                    }
                }
            }

            Lighting.AddLight(NPC.Center, 1.5f, 0.3f, 0.2f);
        }

        private void FireLaser(NPC target, NPC unusedTargetAlias, float spread, bool playSound) { }

        private void FireLaser(NPC target, ExoTwinsMinion minion, float spread, bool playSound)
        {
            if (playSound)
                SoundEngine.PlaySound(Artemis.LaserShotgunSound, NPC.Center);
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            Vector2 toTarget = target.Center - NPC.Center;
            float distance = Math.Max(toTarget.Length(), 300f);
            Vector2 direction = toTarget.SafeNormalize(Vector2.UnitX).RotatedBy(spread);
            Vector2 destination = NPC.Center + direction * distance; // spread now actually fans out around the target

            int laser = Projectile.NewProjectile(
                NPC.GetSource_FromAI(),
                NPC.Center + direction * 70f,
                destination,
                ModContent.ProjectileType<ArtemisLaser>(),
                (int)(minion.MinionDamage * LaserDamageMult),
                1f,
                OwnerIndex,
                16f,
                NPC.whoAmI);
            MakeFriendly(laser);
        }
    }

    public class ExoTwinsApollo : ExoTwinBase
    {
        public override string Texture => "CalamityMod/NPCs/ExoMechs/Apollo/Apollo";

        protected override Vector2 IdleOffset => new Vector2(160f, -140f);
        protected override Vector2 HoverOffset => new Vector2(450f, -150f);

        protected override void Behave(Player player, NPC target, ExoTwinsMinion minion)
        {
            float t = minion.PhaseTimer;

            MoveTo(target.Center + HoverOffset, 22f);
            AimAt(target.Center);
            Animate(AttackFirst, AttackLast);

            if ((int)minion.Phase == ExoTwinsMinion.PhaseVolley)
            {
                if (Every(t, 30f, 30f))
                    FirePlasma(target, minion);
            }
            else
            {
                if (Every(t, 30f, 20f))
                    FireRocket(target, minion);
            }

            Lighting.AddLight(NPC.Center, 0.2f, 1.5f, 0.4f);
        }

        private void FirePlasma(NPC target, ExoTwinsMinion minion)
        {
            SoundEngine.PlaySound(CommonCalamitySounds.ExoPlasmaShootSound, NPC.Center);
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);

            int fireball = Projectile.NewProjectile(
                NPC.GetSource_FromAI(),
                NPC.Center + direction * 70f,
                direction * 13f,
                ModContent.ProjectileType<ApolloFireball>(),
                (int)(minion.MinionDamage * PlasmaDamageMult),
                2f,
                OwnerIndex,
                target.Center.X,
                target.Center.Y);
            MakeFriendly(fireball);
        }

        private void FireRocket(NPC target, ExoTwinsMinion minion)
        {
            SoundEngine.PlaySound(Apollo.MissileLaunchSound, NPC.Center);
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);

            int rocket = Projectile.NewProjectile(
                NPC.GetSource_FromAI(),
                NPC.Center + direction * 70f,
                direction * 16f,
                ModContent.ProjectileType<ApolloRocket>(),
                (int)(minion.MinionDamage * RocketDamageMult),
                2f,
                OwnerIndex,
                0f,
                target.Center.Y);
            MakeFriendly(rocket);
        }
    }

}