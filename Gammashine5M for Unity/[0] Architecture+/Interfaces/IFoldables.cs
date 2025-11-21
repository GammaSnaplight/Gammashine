namespace Gammashine
{
    public interface IFoldables<T> : IRawable<T>
    {
        public T Fold { get; set; }
    }
}
