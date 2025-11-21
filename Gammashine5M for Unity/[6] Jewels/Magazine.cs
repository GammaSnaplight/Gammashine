using Gammashine.Bindfolds.Unity.Editor;
using Gammashine.Stationary.Mathematics;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace Snaplight.Jewels
{
    [Serializable]
    /// <summary>
    /// 6_Jewels: Magazine является специальной коллекцией, которая перемещается по данным поочередно в обе стороны безопасно предоставляя данные.
    /// Полезная связка подобной коллекции связана с использованием UI, где надо переключатся между значениями.
    /// </summary>
    /// <typeparam name="T"> Обобщенный тип без ограничений </typeparam>
    public partial class Magazine<T>
    {
        // Serializable
        [VMe("Cartridges")]
        [SerializeField] private List<T> _cartridges = new();

        [VMe("Options")]
        [SerializeField] private int _initialIndex;

        // Serializable : Property
        [HideInInspector] public T Current { get; private set; }
        [HideInInspector] public T Previous { get; private set; }

        [HideInInspector] public int Index => _currentIndex;

        // Variable
        private int _currentIndex;

        public Magazine()
        {
            Current = _cartridges[_currentIndex];
        }

        public void Initial(int initialIndex)
            => _initialIndex = initialIndex;

        public void Collection(params T[] cartridges) 
            => _cartridges.AddRange(cartridges);

        public T Recalculation()
        {
            //---
            Previous = _cartridges[_currentIndex];

            //---
            _currentIndex = Mathlight.Magazine(++_currentIndex, 0, _cartridges.Count - 1);
            Current = _cartridges[_currentIndex];

            //---
            return Current;
        }

        public T Reverse()
        {
            //---
            Previous = _cartridges[_currentIndex];

            //---
            _currentIndex = Mathlight.Magazine(--_currentIndex, 0, _cartridges.Count - 1);
            Current = _cartridges[_currentIndex];

            //---
            return Current;
        }
    }
}
