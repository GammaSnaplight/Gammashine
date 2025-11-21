using System;
using System.Collections.Generic;

namespace Gammashine
{
    public interface ISupervisionTypemodel<T, K>
        where T : Enum
        where K : Enum
    { }

    public interface ISupervision<T, K> : ISupervisionTypemodel<T, K>
        where T : Enum
        where K : Enum
    {
        public List<(T, K)> Enumerations { get; set; }
    }

    public interface ISupervisionGammashine<T, K> : ISupervisionTypemodel<T, K>, IModulable, IDestroyable, IEnterable<SupervisionGammashineFold<T, K>>
        where T : Enum
        where K : Enum
    {
        public ICollection<SupervisionGammashineFold<T, K>> Enumerations { get; set; }
    }
}
