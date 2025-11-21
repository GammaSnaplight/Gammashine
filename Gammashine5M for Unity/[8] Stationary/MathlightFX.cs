using System;

namespace byGammaSnaplight.Gammashine.Mathematics
{
    public static partial class Mathlight
    {
        public static float SlowdownStart(float t, float percent, float limit) 
            => limit == 0 ? t : percent * percent;

        public static float SlowdownLate(float t, float percent, float limit) 
            => limit == 0 ? t : -1 * (percent * (percent - 2));

        public static float SlowdownEdges(float t, float percent, float limit) 
            => limit == 0 ? t : percent < 0.5f ? 2 * percent * percent : -1 + ((4 - (2 * percent)) * percent);

        public static float SharpStart(float t, float percent, float limit) 
            => limit == 0 ? t : percent * percent * percent;

        public static float SharpLate(float t, float percent, float limit) 
            => limit == 0 ? t : 1 - MathF.Pow(1 - percent, 3);

        public static float SharpEdges(float t, float percent, float limit) 
            => limit == 0 ? t : percent < 0.5f ? 4 * percent * percent * percent : 1 - MathF.Pow(-2 * percent + 2, 3) / 2;

        public static float AcuteStart(float t, float percent, float limit) 
            => limit == 0 ? t : MathF.Pow(percent, 4);

        public static float AcuteLate(float t, float percent, float limit) 
            => limit == 0 ? t : 1 - MathF.Pow(1 - percent, 4);

        public static float AcuteEdges(float t, float percent, float limit) 
            => limit == 0 ? t : percent < 0.5f ? 4 * MathF.Pow(2 * percent, 4) : 1 - MathF.Pow(-2 * percent + 2, 4) / 2;

        public static float LightfastStart(float t, float percent, float limit) 
            => limit == 0 ? t : MathF.Pow(percent, 4);

        public static float LightfastLate(float t, float percent, float limit)
            => limit == 0 ? t : 1 - MathF.Pow(1 - percent, 4);

        public static float LightfastEdges(float t, float percent, float limit) 
            => limit == 0 ? t : percent < 0.5f ? 8 * MathF.Pow(percent, 4) : 1 - MathF.Pow(-2 * percent + 2, 4) / 2;

        public static float ElasticStart(float t, float percent, float limit) 
            => limit == 0 ? t : MathF.Pow(2, 10 * percent - 10) * MathF.Sin((percent * 10 - 10.75f) * ((2 * MathF.PI) / 3));

        public static float ElasticLate(float t, float percent, float limit) 
            => limit == 0 ? t : 1 - MathF.Pow(2, -10 * percent) * MathF.Sin((percent * 10 - 0.75f) * ((2 * MathF.PI) / 3));

        public static float ElasticEdges(float t, float percent, float limit) 
            => limit == 0 ? t : percent < 0.5f ? -(MathF.Pow(2, 20 * percent - 10) * MathF.Sin((20 * percent - 11.125f) * ((2 * MathF.PI) / 4.5f))) / 2 : (MathF.Pow(2, -20 * percent + 10) * MathF.Sin((20 * percent - 11.125f) * ((2 * MathF.PI) / 4.5f))) / 2 + 1;

        public static float ExponentialStart(float t, float percent, float limit) 
            => limit == 0 ? t : MathF.Pow(2, 10 * percent - 10);

        public static float ExponentialLate(float t, float percent, float limit) 
            => limit == 0 ? t : 1 - MathF.Pow(2, -10 * percent);

        public static float ExponentialEdges(float t, float percent, float limit) 
            => limit == 0 ? t : percent < 0.5f ? MathF.Pow(2, 20 * percent - 10) / 2 : (2 - MathF.Pow(2, -20 * percent + 10)) / 2;

        public static float CircularStart(float t, float percent, float limit) 
            => limit == 0 ? t : 1 - MathF.Sqrt(1 - MathF.Pow(percent, 2));

        public static float CircularLate(float t, float percent, float limit) 
            => limit == 0 ? t : MathF.Sqrt(1 - MathF.Pow(percent - 1, 2));

        public static float CircularEdges(float t, float percent, float limit)
            => limit == 0 ? t : percent < 0.5f ? (1 - MathF.Sqrt(1 - MathF.Pow(2 * percent, 2))) / 2 : (MathF.Sqrt(1 - MathF.Pow(-2 * percent + 2, 2)) + 1) / 2;

        public static float Bounce(float t)
        {
            if (t < 1 / 2.75f) return 7.5625f * t * t;
            else if (t < 2 / 2.75f) return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
            else if (t < 2.5 / 2.75) return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
            else return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
        }

        public static float BounceStart(float t, float percent, float limit)
            => limit == 0 ? t : 1 - Bounce(1 - percent);

        public static float BounceLate(float t, float percent, float limit)
            => limit == 0 ? t : Bounce(percent);

        public static float BounceEdges(float t, float percent, float limit)
            => limit == 0 ? t : percent < 0.5f ? (1 - Bounce(1 - (2 * percent))) / 2 : (1 + Bounce((2 * percent) - 1)) / 2;
    }
}
