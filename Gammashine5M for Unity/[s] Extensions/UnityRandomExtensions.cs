using Gammashine.Stationary.Mathematics;

using System.Collections.Generic;

using UnityEngine;

namespace Gammashine.Extensions
{
    public static class UnityRandomExtensions
    {
        public static float RangeQuaternion
            => Mathlight.MinMax(Random.Range(0, 361), 0, 360);

        public static int RangeElements<T>(ICollection<T> item)
            => Random.Range(0, item.Count);

        public static int RangeAnd(int a, int b) 
            => Random.Range(0, 101) <= 50 ? a : b;

        public static float RangeAnd(float a, float b)
            => Random.Range(0, 101) <= 50 ? a : b;

        public static bool Percent(int chance)
            => Random.Range(0, 101) <= chance;

        public static int Range(int max)
            => Random.Range(0, max + 1);

        public static float Range(float max)
            => Random.Range(0, max + 1);
    }
}
