using Gammashine.Controllables;
using Gammashine.Bindfolds.Unity.Editor;

using Snaplight.Controllable;

using System;

using UnityEngine;

namespace Snaplight.Folds
{
    [Serializable]
    public class AutotimerFold : IRawable<AutotimerFold>
    {
        // Serializable
        [VMe("Limitation")]
        [Min(0)] public float Limit;
        [HideInInspector, Min(0)] public float Timer;

        // Serializable: Controllable
        [VMe("Options")]
        public TimedataControllable Controllable;
        public OvertimeTypemodel OvertimeTypemodel;
        public CountdownTypemodel CountdownTypemodel;
    }
}