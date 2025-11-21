using System.Runtime.CompilerServices;

using UnityEngine;

namespace byGammaSnaplight.Gammashine.Mathematics
{
    public static partial class Mathlight
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Interpolation(float a, float b, float amount)
            => (a * (1.0f - amount)) + (b * amount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Interpolation(Vector2 a, Vector2 b, float amount)
            => (a * (1.0f - amount)) + (b * amount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Interpolation(Vector3 a, Vector3 b, float amount)
            => (a * (1.0f - amount)) + (b * amount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Showtime(float t)
            => Interpolation(0, 1, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Fadeout(float t)
            => Interpolation(1, 0, t);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InterpolateLimitation(float a, float b, float amount)
            => MinMax(Interpolation(a, b, amount), a, b);
    }
}
