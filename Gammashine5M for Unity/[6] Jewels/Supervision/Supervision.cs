using System;
using System.Collections.Generic;

namespace Gammashine.Jewels
{
    public partial class Supervision<T, K> : ISupervision<T, K>
        where T : Enum
        where K : Enum
    {
        // ISupervisionTypemodel
        public List<(T, K)> Enumerations { get; set; } = new();

        // Variable
        private List<(T, Enum)> _enumerationFilters = new();

        private EqualityComparer<Enum> _comparer = EqualityComparer<Enum>.Default;

        private T _memoryControllable;

        public Supervision<T, K> Collection(T controllable, K mirror)
        {
            if (_comparer.Equals(_memoryControllable, controllable)) return this;

            Enumerations.Add((controllable, mirror));

            _memoryControllable = controllable;

            return this;
        }

        public bool Checkout(T controllable, K mirror)
        {
            return Enumerations.Contains((controllable, mirror));
        }

        public void Pairwise(bool isCriterion, T controllable, K mirror, T controllableOther, K mirrorOther)
        {
            if (isCriterion)
            {
                Collection(controllable, mirror);
                Elimination(controllableOther, mirrorOther);
            }
            else
            {
                Elimination(controllable, mirror);
                Collection(controllableOther, mirrorOther);
            }
        }

        public Supervision<T, K> Elimination(T controllable, K mirror)
        {
            Enumerations.Remove((controllable, mirror));

            if (_comparer.Equals(_memoryControllable, controllable)) _memoryControllable = default;

            return this;
        }

        public void Destruction()
        {
            Enumerations.Clear();
        }
    }
}
