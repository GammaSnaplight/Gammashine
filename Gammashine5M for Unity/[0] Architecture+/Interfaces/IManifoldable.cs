using System.Collections.Generic;

namespace Gammashine
{ 
    public interface IManifoldable<T>
        where T : IModulable
    {
        public IList<T> Modules { get; set; }
    }
}
