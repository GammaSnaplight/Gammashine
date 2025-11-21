using System;
using System.Collections.Generic;

using Snaplight.Controllable;

namespace Gammashine.Jewels
{
    public class SupervisionGammashine<T> : ISupervisionGammashine<T, MirrorControllable>, IEquatable<SupervisionGammashineFold<T, MirrorControllable>>
        where T : Enum
    {
        // ISupervisionGammashine
        public ICollection<SupervisionGammashineFold<T, MirrorControllable>> Enumerations { get; set; } = new List<SupervisionGammashineFold<T, MirrorControllable>>();

        // Serializable

        // Variable
        private EqualityComparer<SupervisionGammashineFold<T, MirrorControllable>> _comparer = EqualityComparer<SupervisionGammashineFold<T, MirrorControllable>>.Default;

        private List<SupervisionGammashineFold<T, MirrorControllable>> _folds;

        public void Enterfold(SupervisionGammashineFold<T, MirrorControllable> fold)
            => _folds.Add(fold);

        public void Enterfolds(params SupervisionGammashineFold<T, MirrorControllable>[] folds)
        {
            foreach (SupervisionGammashineFold<T, MirrorControllable> fold in folds) _folds.Add(fold);
        }

        public void Collection()
        {
            if (_folds.Count == 0) return;

            foreach (SupervisionGammashineFold<T, MirrorControllable> fold in _folds)
            {
                _folds.Add(fold);
                _folds.Clear();
            }
        }

        public bool Equals(SupervisionGammashineFold<T, MirrorControllable> controllable)
        {
            _folds.Clear();

            foreach (SupervisionGammashineFold<T, MirrorControllable> enumeration in Enumerations)
            {
                if (_comparer.Equals(enumeration, controllable))
                {
                    return true;
                }
            }
            return false;
        }

        public void Elimination()
        {
            if (_folds.Count == 0) return;

            foreach (SupervisionGammashineFold<T, MirrorControllable> fold in _folds)
            {
                _folds.Remove(fold);
                _folds.Clear();
            }
        }

        public void Destruction() 
            => Enumerations.Clear();
    }
}
