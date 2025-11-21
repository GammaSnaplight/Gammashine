using System;

using UnityEngine;

namespace Snaplight.Extension
{
    public static partial class RigidBodyExtensions
    {
        [Serializable]
        public class RigidBodyControllerData
        {
            public float Accelerate;
            public float Decelerate;
            public float HorizontalLimitation;
            public float VerticalLimitation;
            public float Gravity;
        }
    }
}
