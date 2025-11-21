using UnityEngine;

namespace Gammashine.Extensions
{
    public static class VectorExtensions
    {
        public static void Zero(this ref Vector2 vector) 
            => vector = Vector2.zero;

        public static void Zero(this ref Vector3 vector) 
            => vector = Vector3.zero;

        public static Vector3 Convert2to3(this Vector2 vector2)
            => new(vector2.x, 0, vector2.y);
    }
}
