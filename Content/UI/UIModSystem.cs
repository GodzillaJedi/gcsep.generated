using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace gcsep.Content.UI
{
    public class UIModSystem : ModSystem
    {
        public override void UpdateUI(GameTime gameTime)
        {
            if (gcsep.Instance._showBossSummonUI)
            {
                gcsep.Instance._bossSummonUI?.Update(gameTime);
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "gcsep: Boss Summon UI",
                    delegate
                    {
                        if (gcsep.Instance._showBossSummonUI)
                        {
                            gcsep.Instance._bossSummonUI.Update(new GameTime());
                            gcsep.Instance._bossSummonUI.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
