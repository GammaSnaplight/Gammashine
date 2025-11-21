using System;

namespace Gammashine.Jewels
{
    public partial class Supervision<T, K> : ISupervision<T, K>
    {
        public void Filter<O>(T controllable, O other) where O : Enum
        {
            _enumerationFilters.Add((controllable, other));
        }

        public bool FilterCheckout<O>(O other) where O : Enum
        {
            foreach ((T, Enum) filter in _enumerationFilters) if (_comparer.Equals(filter.Item2, other)) return true;
            return false;
        }

        public T FilterReturn<O>(O other) where O : Enum
        {
            foreach ((T, Enum) filter in _enumerationFilters) if (_comparer.Equals(filter.Item2, other)) return filter.Item1;
            throw new NullReferenceException();
        }
    }
}
