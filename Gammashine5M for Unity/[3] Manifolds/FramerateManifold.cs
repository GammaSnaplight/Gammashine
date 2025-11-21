using Gammashine;
using Gammashine.Modules;

using System;
using System.Collections.Generic;

namespace Gammashine.Manifolds
{
    [Serializable]
    public class FramerateManifold : IManifoldable<IModulable>
    {
        // IManifoldable
        public IList<IModulable> Modules { get; set; } = new List<IModulable>();

        // Modules
        public FramerateModule FramerateModule = new();
        public FrameratePerformanceModule PerformanceModule = new();   
    }
}
