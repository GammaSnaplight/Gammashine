using System;

using UnityEngine;

namespace Snaplight.Folds
{
    /// <summary>
    /// 2_Fold - Задокументированная структура в Gammashine описывающая все состояния такого действия как - скорость.
    /// Укладывается ровно в 16 байт.
    /// </summary>
    [Serializable]
    public struct Velocity
    {
        // Serializable
        public float Acceleration;
        public float Deceleration;
        public float Limitation;

        // Hide
        [HideInInspector] public float Current;
    }
}