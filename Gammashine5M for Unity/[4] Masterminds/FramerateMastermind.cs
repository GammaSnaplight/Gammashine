using Gammashine;

using UnityEngine;

namespace Gammashine.Manifolds
{
    public class FramerateMastermind : MonoBehaviour, IMasterable<IManifoldable<IModulable>>
    {
        public FramerateManifold FramerateManifold = new();
        public IManifoldable<IModulable> Manifold { get => FramerateManifold; private set => FramerateManifold = (FramerateManifold)value; }

        public void Collection()
        {
            Manifold.Modules.Add(FramerateManifold.FramerateModule);
            Manifold.Modules.Add(FramerateManifold.PerformanceModule);

            FramerateManifold.FramerateModule.Collection();
            FramerateManifold.PerformanceModule.Collection();
        }
        public void Playback()
        {
            FramerateManifold.PerformanceModule.FramerateBind = FramerateManifold.FramerateModule.FramerateInformation;
        }

        public void Elimination()
        {
           
        }
    }
}
