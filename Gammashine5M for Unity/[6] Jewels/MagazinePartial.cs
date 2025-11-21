using System;

namespace Snaplight.Jewels
{
    public partial class Magazine<T>
    {
        public T RecalculationChecksafe()
        {
            if (_cartridges.Count < 2) throw new Exception("Magazine cannot contain less than 2 values.");

            return Recalculation();
        }

        public T ReverseChecksafe()
        {
            if (_cartridges.Count < 2) throw new Exception("Magazine cannot contain less than 2 values.");
            if (Previous == null) throw new Exception("The methods Recalculation() or Reverse() were not called to restore this value to the previous value.");

            return Reverse();
        }
    }
}
