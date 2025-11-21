using System;

namespace Gammashine
{
    public interface IControllable<T>
        where T : Enum
    {
        public T Controllable { get; set; }
    }
}
