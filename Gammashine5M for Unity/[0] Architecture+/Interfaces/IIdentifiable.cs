namespace Gammashine
{
    public interface IIdentifiable<T>
    {
        public T Identifier { get; }
    }

    public interface IUniversalIdentifiable<T> : IIdentifiable<T>
    {

    }
}
