using Snaplight.Controllable;
using Snaplight.Folds;

using System;

namespace Gammashine
{
    public interface IChangeable<T>
        where T : Enum
    {
        public void Changeover(T controllable);
    }

    public interface IAutotimerModulable : IMultipurposeModulable<AutotimerFold>, IZeroable, IShuttable, IChangeable<AutotimerChangeover>
    {
        public AutotimerFold Fold { get; set; }
    }
}