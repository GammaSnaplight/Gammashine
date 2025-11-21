using System;

using UnityEngine;

namespace Gammashine.Bindfolds.Unity.Editor
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class VAutonaming : PropertyAttribute
    {
        public VAutonaming() { }
    }
}
