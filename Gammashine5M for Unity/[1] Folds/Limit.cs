using System;

using UnityEngine;

namespace Snaplight.Folds
{
    [Serializable]
    public struct Limit
    {
        // Serializable
        public float Preload;
        public float Minimal;
        public float Limitation;

        // Hide
        [HideInInspector] public float Current;

        public void Initialize()
            => Current = Preload;
    }
}