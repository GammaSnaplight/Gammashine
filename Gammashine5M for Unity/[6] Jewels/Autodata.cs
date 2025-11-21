using Gammashine.Controllables;

using Snaplight.Folds;

using System;
using System.Collections.Generic;

namespace Snaplight.Folds
{
    [Serializable]
    public struct AutoFold<T, K>
    {
        public T Variable;
        public K Controllable;
        public bool IsIgnore;
    }
}

namespace Snaplight.Jewels
{
    [Serializable]
    public class Autodata<TVariable, KControllable>
        where KControllable : Enum
    {
        // Special
        public static int InitialElementCount;

        // Serializable
        public bool IsChangeover { get; private set; }

        // Variable
        private readonly EqualityComparer<KControllable> _comparer = EqualityComparer<KControllable>.Default;

        private readonly List<AutoFold<TVariable, KControllable>> _data = new(InitialElementCount);

        private TVariable _initial;
        private TVariable _meantime;
        private TVariable _capture = default;

        private KControllable _mirrorControllable;

        private MonophaseControllable _initialPhase;

        private int _index;

        public Autodata<TVariable, KControllable> Collection(TVariable variable, KControllable controllable, bool isFilter = false)
        {
            //---
            foreach (AutoFold<TVariable, KControllable> data in _data) if (_comparer.Equals(controllable, data.Controllable)) return this;

            //---
            if (_index >= _data.Count)
            {
                _data.Add(new AutoFold<TVariable, KControllable>());
            }

            //---
            AutoFold<TVariable, KControllable> mirror = _data[_index];
            mirror.Variable = variable;
            mirror.Controllable = controllable;
            mirror.IsIgnore = isFilter;
            _data[_index] = mirror;

            //---
            _index++;

            return this;
        }

        public Autodata<TVariable, KControllable> Initial(TVariable variable)
        {
            if (_initialPhase == MonophaseControllable.Finishes) return this;

            _initial = variable;
            _initialPhase = MonophaseControllable.Activeness;

            return this;
        }

        public Autodata<TVariable, KControllable> Meantime(TVariable variable)
        {
            _meantime = variable;

            return this;
        }

        public TVariable Default() => _initial;

        public KControllable This() => _mirrorControllable;

        public TVariable Automation(KControllable controllable)
        {
            //---
            if (_comparer.Equals(controllable, _mirrorControllable)) return _capture;

            //---
            if (_initialPhase == MonophaseControllable.Activeness)
            {
                _initialPhase = MonophaseControllable.Finishes;
                return _initial;
            }

            //---
            foreach (AutoFold<TVariable, KControllable> data in _data)
            {
                if (_comparer.Equals(controllable, data.Controllable))
                {
                    if (data.IsIgnore)
                    {
                        if (_capture == null) return _initial;
                        else return _capture;
                    }

                    IsChangeover = !_comparer.Equals(controllable, _mirrorControllable);
                    _mirrorControllable = controllable;

                    _capture = data.Variable;
                    return data.Variable;
                }
            }

            //---
            return _meantime;
        }

        public KControllable ReverseAutomation(TVariable variable)
        {
            //---
            foreach (AutoFold<TVariable, KControllable> data in _data)
            {
                if (EqualityComparer<TVariable>.Default.Equals(variable, data.Variable))
                {
                    return data.Controllable;
                }
            }

            //---
            return _mirrorControllable;
        }
    }
}
