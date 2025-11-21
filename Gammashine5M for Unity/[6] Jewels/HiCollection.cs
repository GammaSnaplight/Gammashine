using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Snaplight.Origamma
{
    public struct HiCollection<T> : ICollection<T>
    {
        // ICollection
        public readonly int Count => _count;
        public readonly int Size => _sizes;

        public readonly bool IsReadOnly => false;

        // Variable
        private T[] _items;
        private int _count;
        private int _sizes;
        private int _sizeType;

        private bool _highPerformance;

        private const int _hiSize = 16;
        private const int _lowSize = 64;

        public HiCollection(int capacity, bool highPerformance)
        {
            _highPerformance = highPerformance;

            _sizeType = Marshal.SizeOf<T>();

            _items = new T[capacity];

            _sizes = capacity * _sizeType;

            _count = 0;

            if (_sizes >= Performance()) throw new Exception($"Collection memory limit exceeded");
        }

        public void Add(T item)
        {
            if (_count >= _items.Length) throw new Exception($"The limit of collection items has been exceeded");

            _items[_count] = item;
            _count++;

            _sizes += _sizeType;

            if (_sizes >= Performance()) throw new Exception($"Collection memory limit exceeded");
        }

        public void Clear()
        {
            for (int i = 0; i < _items.Length; i++) _items[i] = default;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++) if (_items[i].Equals(item)) return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Span<T> s1 = _items.AsSpan(0, _count);
            Span<T> s2 = array.AsSpan(arrayIndex, _count);
            s1.CopyTo(s2);
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i].Equals(item))
                {
                    _count--;

                    for (int j = i; j < _count; j++) _items[j] = _items[j + 1];

                    _items[_count] = default;
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++) yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();

        private int Performance() 
            => _highPerformance ? _hiSize : _lowSize;
    }
}
