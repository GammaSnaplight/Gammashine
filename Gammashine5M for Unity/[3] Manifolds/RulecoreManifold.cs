using Gammashine.Modules;

using System;
using System.Collections.Generic;

namespace Gammashine.Manifolds
{
    [Serializable]
    public class RulecoreManifold : IManifoldable<IModulable>
    {
        // IManifoldable
        public IList<IModulable> Modules { get; set; } = new List<IModulable>();

        // Modules
        public RulecorePlaybackModule RulecorePlayback = new();
    }
}
