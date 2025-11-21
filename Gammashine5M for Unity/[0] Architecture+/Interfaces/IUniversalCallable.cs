using System;

namespace Gammashine
{
    public interface IUniversalCallable<T, K> : IUniversalIdentifiable<K>
    {
        public event Action<T> Collected;
        public event Action<T> Eliminated;
    }
}
