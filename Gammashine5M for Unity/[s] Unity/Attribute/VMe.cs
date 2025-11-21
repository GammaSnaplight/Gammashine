using System;

using UnityEngine;

namespace Gammashine.Bindfolds.Unity.Editor
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class VMeAttribute : PropertyAttribute 
    {
        public string Name;

        public VMeAttribute(string name)
        {
            Name = name;
        }
    }
}