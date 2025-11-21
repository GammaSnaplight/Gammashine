using Gammashine.Bindfolds.Unity.Editor;

using UnityEngine;

namespace Snaplight.Folds.Scriptable
{
    [CreateAssetMenu(fileName = "MultipurposeRotationScriptable", menuName = "Snaplight/Folds/Scriptable/Cameravision/MultipurposeRotationSettings")]
    public class MultipurposeCameravisionRotationScriptable : ScriptableObject
    {
        [VMe("Options")]
        public int RotationSmoothness;

        public float SensitivityX;
        public float SensitivityY;

        public int RotationYLimitation;
        public int RotationNegativeYLimitation;

        [VMe("Special")]
        public bool IsInvert;
        public bool IsSmoothness;
    }
}
