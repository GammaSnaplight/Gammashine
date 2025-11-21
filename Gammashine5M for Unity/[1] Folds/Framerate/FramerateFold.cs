using System;

using UnityEngine;

namespace Snaplight.Folds
{
    public class FramerateFold
    {
        // Serializable
        public int FramerateAverageLimitation;

        // Hide
        [HideInInspector] public byte FPS;
        [HideInInspector] public byte FPSMinimal;
        [HideInInspector] public byte FPSMaximum;
        [HideInInspector] public byte FPSAverage;

        [HideInInspector] public byte[] FPSArray;

        [HideInInspector] public int IndexArray;
    }
}
