using System.Collections.Generic;
using Terraria.ModLoader;

namespace gcsep.Core
{
    internal abstract class CrossmodHandler : ILoadable
    {
        protected Mod Mod { get; private set; }

        protected Mod Crossmod
        {
            get
            {
                if (!ModLoader.TryGetMod(ModName, out var result))
                {
                    return null;
                }

                return result;
            }
        }

        internal bool IsModLoaded => Crossmod != null;

        protected abstract string ModName { get; }

        internal virtual void OnModLoad()
        {
        }

        internal virtual void SetupContent()
        {
        }

        internal virtual void PostSetupContent()
        {
        }

        internal virtual void PostSetupEverything()
        {
        }

        void ILoadable.Load(Mod mod)
        {
            Mod = mod;
            CrogcsepodSystem._handlers.Add(this);
        }

        void ILoadable.Unload()
        {
        }
    }

    public sealed class CrogcsepodSystem : ModSystem
    {
        internal static readonly List<CrossmodHandler> _handlers = new List<CrossmodHandler>();

        public override void OnModLoad()
        {
            foreach (CrossmodHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.OnModLoad();
                }
            }
        }

        public override void SetupContent()
        {
            foreach (CrossmodHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.SetupContent();
                }
            }
        }

        public override void PostSetupContent()
        {
            foreach (CrossmodHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.PostSetupContent();
                }
            }
        }

        public override void PostAddRecipes()
        {
            foreach (CrossmodHandler handler in _handlers)
            {
                if (handler.IsModLoaded)
                {
                    handler.PostSetupEverything();
                }
            }
        }

        public override void Unload()
        {
            _handlers.Clear();
        }
    }
}